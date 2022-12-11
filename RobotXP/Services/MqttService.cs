using System.Text;
using MQTTnet;
using MQTTnet.Server;
using MQTTnet.Protocol;
using Newtonsoft.Json.Linq;
using RobotXP.Models;
using RobotXP.Repositories;

namespace RobotXP.Services {
    public class MqttService {
        private MqttService() { }

        public readonly static MqttService Instance = new();

        private readonly RobotXPRepository Repository = new();

        private IMqttServer? Server { get; set; }

        public void Build() {
            if (Server != null) { return; }
            var factory = new MqttFactory();
            Server = factory.CreateMqttServer();
        }

        public async Task Start() {
            if (Server == null) { return; }
            var options = new MqttServerOptionsBuilder()
                .WithDefaultEndpoint()
                .WithApplicationMessageInterceptor(OnNewMessage)
                .WithConnectionValidator(OnNewConnection)
                .Build();
            await Server.StartAsync(options);
            Console.WriteLine("STARTED");
        }

        public async Task Stop() {
            if (Server == null) { return; }
            await Server.StopAsync();
            Server.Dispose();
            Server = null;
        }

        private void OnNewConnection(MqttConnectionValidatorContext context) {
            // Console.WriteLine("CONNECTION");
            // Console.WriteLine(context.UserProperties);
            // Console.WriteLine(context.WillMessage);
            context.ReasonCode = MqttConnectReasonCode.Success;

        }

        private void OnNewMessage(MqttApplicationMessageInterceptorContext context) {
            var payload = Encoding.UTF8.GetString(
                context.ApplicationMessage.Payload,
                0,
                context.ApplicationMessage.Payload.Length
            );

            var json = JObject.Parse(payload);

            if (context.ApplicationMessage.Topic == "trajectory") {
                var trajectory = new TrajectoryModel {
                    StartedAt = DateTime.Parse(json["startedAt"]?.Value<string>() ?? ""),
                    EndedAt = DateTime.Parse(json["endedAt"]?.Value<string>() ?? "")
                };
                Repository.PostTrajectory(trajectory);
            } else if (context.ApplicationMessage.Topic == "robot_send") {
                var joints = new List<JointModel>();

                if (json["joints"] != null) {
                    foreach (var joint in json["joints"]!) {
                        var jointModel = new JointModel {
                            Current = joint["current"]?.Value<double>() ?? (double) 0.0,
                            Voltage = joint["voltage"]?.Value<double>() ?? (double) 0.0,
                            Number = joint["number"]?.Value<int>() ?? -1
                        };
                        joints.Add(jointModel);
                    }
                }

                var measure = new MeasureModel {
                    IsMoving = json["isMoving"]?.Value<bool>() ?? false,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    Joints = joints
                };

                Repository.PostMeasure(measure);
            }
        }
    }
}

