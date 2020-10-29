using System;
using System.Threading;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;
using FlexBuffers;
using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.UI;

public class Cube_MQTT : MonoBehaviour
{
    public Text MagHeading;
    public Text GroundSpeed;
    public Text RadioAltitude;
    public Text VerticalSpeed;
    String IP;
    
    MqttFactory factory;
    IMqttClient mqttClient;
    IMqttClientOptions options;
    AutoResetEvent semaphore;
    TimeSpan receiveTimeout;
    byte[] data;

    public WindDirection windDirection;
    public EngineTorqueHud EngineTorqueHud;
    public PropRPMHUD PropRPMHUD;
    public VerticalSpeedHUD VerticalSpeedHud;
    public FlightPathVector FPV;
    public AttitudeHeadingHUD AttitudeHeadingHud;
    public AirspeedHUD AirspeedHud;
    public AltitudeHUD AltitudeHud;
    public HeadingHUD HeadingHud;
    public AttitudeHUD AttitudeHud;

    void Start()
    {
        String PersistentPath = Application.persistentDataPath;
        DirectoryInfo dir = new DirectoryInfo(Application.persistentDataPath);
        FileInfo[] info = dir.GetFiles("*.*");
        int processed = 0;
        foreach (FileInfo f in info)
        {
            if (processed == 0)
            {
                String FilePath = f.ToString();
                FilePath = FilePath.Substring(PersistentPath.Length + 1);
                //Debug.Log(FilePath);
                processed++;
                IP = FilePath;
            }
        }
        Debug.Log(IP);
        Debug.Log(PersistentPath);
        factory = new MqttFactory();
        mqttClient = factory.CreateMqttClient();
        options = new MqttClientOptionsBuilder()
                .WithClientId("")
                .WithTcpServer(IP, 1883)
                .WithCleanSession()
                .Build();

        mqttClient.ConnectAsync(options, CancellationToken.None);

        semaphore = new AutoResetEvent(false);
        receiveTimeout = TimeSpan.FromSeconds(0.2f);

        mqttClient.UseConnectedHandler(async e =>
        {
            Debug.Log("### CONNECTED WITH SERVER ###");

            // Subscribe to a topic
            await mqttClient.SubscribeAsync(new TopicFilterBuilder().WithTopic("XP-S76-Debug").Build());

            Debug.Log("### SUBSCRIBED ###");
        });

        mqttClient.UseApplicationMessageReceivedHandler(e =>
        {
            data = e.ApplicationMessage.Payload;
            semaphore.Set();
        });

    }

    // Update is called once per frame
    void Update()
    {
        semaphore.WaitOne((int)receiveTimeout.TotalMilliseconds, true);

        if (data != null)
        {
            var flx = FlxValue.FromBytes(data).AsMap;
            //sim / flightmodel / position / indicated_airspeed
            //Debug.Log("Byte message recieved.");
            //Debug.Log(flx.ToJson);

            //var latitude = flx["latitude"].AsDouble;
            //var longitude = flx["longitude"].AsDouble;

            var windspeed = flx["windspeed"].AsDouble;
            var windheading = flx["windheading"].AsDouble;
            var windpitch = flx["windpitch"].AsDouble;
            var windroll = flx["windroll"].AsDouble;

            var PropellerRPM = flx["PROPRPM"][0].AsDouble;
            var EngineTorque = flx["ENGNTRQ"][0].AsDouble;
            var altitude = flx["altitude"].AsDouble;
            var airspeed = flx["airspeed"].AsDouble;
            var verticalspeed = flx["verticalspeed"].AsDouble;
            var heading = flx["heading"].AsDouble;
            var pitch = flx["pitch"].AsDouble;
            var roll = flx["roll"].AsDouble;
            //var EnginePercent = flx["ENGN1"][0].AsDouble;
            //var EnginePercent1 = flx["ENGN1"][1].AsDouble;

            var relativepitch = flx["relativepitch"].AsDouble;
            var relativeheading = flx["relativeheading"].AsDouble;

            var groundspeed = flx["groundspeed"].AsDouble;
            var radioaltitude = flx["radioaltitude"].AsDouble;

            AltitudeHud.UpdateAltitude((float)altitude);
            AirspeedHud.UpdateAirspeed((float)airspeed);
            VerticalSpeedHud.UpdateVerticalSpeed((float)verticalspeed);
            HeadingHud.UpdateHeading((float)heading);
            AttitudeHud.UpdatePitch((float)pitch);
            AttitudeHud.UpdateRoll((float)roll);
            AttitudeHeadingHud.UpdateAttitudeHeading((float)heading);
            FPV.UpdateFPV((float)relativepitch, (float)relativeheading);
            EngineTorqueHud.UpdateEngineTorque((float)EngineTorque);
            PropRPMHUD.UpdatePropRPM((float)PropellerRPM);
            //EngineHud.UpdateEngine((float)EnginePercent);
            //EngineHud1.UpdateEngine((float)EnginePercent1);

            MagHeading.text = ((int)heading).ToString();
            GroundSpeed.text = ((int)(groundspeed* 1.94384f)).ToString(); //Meters per second to knots per second
            RadioAltitude.text = ((int)radioaltitude).ToString();
            int verticalSpeed50rounded = (int)verticalspeed / 50 * 50;//Estimate to nearest 50.
            VerticalSpeed.text = (verticalSpeed50rounded).ToString();
            windDirection.UpdateWind((float)windheading, (float)heading, (float)windspeed);

            //Debug.Log((float)PropellerRPM);
            //Debug.Log("Wind heading is " + (float)windheading);
            //Debug.Log("Wind pitch is " + (float)windpitch);
            //Debug.Log("Wind roll is " + (float)windroll);
            //Debug.Log("Torque 1 is:" + (float)EngineTorque);//Max ~22,000
            //Debug.Log("Torque 2 is:" + (float)EngineTorque1);
            //Debug.Log("RPM 1 is:" + (float)PropellerRPM);//Max ~500
            //Debug.Log("RPM 2 is:" + (float)PropellerRPM1);

            //Debug.Log("Latitude is:" + (float)latitude);
            //Debug.Log("Longitude is:" + (float)longitude);

            //FuelFlowGauge1.UpdateFlowRate((float)fuel_flow1);
            //FuelFlowGauge2.UpdateFlowRate((float)fuel_flow2);
            //AltitudeGauge.UpdateAltitude((float)altitude);
            //AirspeedGauge.UpdateAirspeed((float)airspeed);
            //VspeedGauge.UpdateVSpeed((float)vspeed);
            //ittGauge.UpdateItt((float)itt);

        }
        else
        {
            Debug.Log("No message recieved.");
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
