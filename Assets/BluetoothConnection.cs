using System.Collections;
using System.Collections.Generic;
using UnityEngine;


#if NETFX_CORE
using Windows.Devices.Bluetooth;
using Windows.Devices.Enumeration;
using Windows.Devices.Bluetooth.GenericAttributeProfile; 
using Windows.Devices.Bluetooth.Advertisement;
using System;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Linq;

#endif

public class F8Sensor
{
	public F8Sensor() //Get empty F8Sensor Object that will be populated once sensor connects
	{

	}

	public ulong bluetoothAddress;

	public int stretch = 0;
	public bool isConnected = false;
	public Quaternion rotation = new Quaternion();
	public Vector3 acceleration = new Vector3(0f, 0f, 0f);
	public int id;
}

public class BluetoothConnection : MonoBehaviour
{
    public string sensorToConnectTo;
    string sensorOne = "f8Act-KC1723000047";
    string sensorTwo = "f8Act-KC1723000082";
    string currentDeviceString;
	public F8Sensor sensor = null;
	public GameObject transformReceiverGameObject;
    BTTransformReceiver transformReceiver;

    // These are used by the ValueChangedFunctions (not exported to unity!)
    private Quaternion rotation = new Quaternion();
    private Vector3 acceleration = new Vector3();

    // There appears to be a BLE device at the Media Lab that sends corrupt advertisements 
    // and causes the system to crash when accessing any of its fields. We check for its
    // BluetoothAddress and return before it can crash us. 
    private List<ulong> EVIL_DEVICES = new List<ulong>(new ulong[] { 22061633427570, 176913868405892 }); 

#if NETFX_CORE
    BluetoothLEAdvertisementWatcher watcher;

    Guid STRETCH_BT_SERVICE_UUID = new Guid("07c60200-faa6-11e6-bc64-92361f002671");
    Guid STRETCH_BT_UUID         = new Guid("07c60202-faa6-11e6-bc64-92361f002671");

    Guid IMU_BT_SERVICE_UUID  = new Guid("07c60300-faa6-11e6-bc64-92361f002671");
    Guid ROTATION_BT_UUID     = new Guid("07c60303-faa6-11e6-bc64-92361f002671");
    Guid ACCELERATION_BT_UUID = new Guid("07c60304-faa6-11e6-bc64-92361f002671");
    // Battery Service
    // Guid STRETCH_BT_SERVICE_UUID = new Guid("0x180F");
    // Guid STRETCH_BT_UUID = new Guid("0x2A19");
    BluetoothLEDevice bleDevice;

    // These need to be here so that they dont get disposed of automatically!
    GattDeviceService stretchService;
    GattCharacteristic stretchCharacteristic;
    GattDeviceService imuService;
    GattCharacteristic rotationCharacteristic;
    GattCharacteristic accelerationCharacteristic;


#endif

    // Use this for initialization
    void Start()
    {
        Debug.Log("Bluetooth Manager started");

		Component[] components = transformReceiverGameObject.GetComponents (typeof(BTTransformReceiver));
		for (int i = 0; i < components.Length; i++) {
			if (components [i] is BTTransformReceiver) {
				transformReceiver = components [i] as BTTransformReceiver;
			}
		}

        Debug.Log("End of BluetoothConnection Start()"); 
		 //transformReceiverGameObject.GetComponent<BTTransformReceiver> ();
#if NETFX_CORE
        //f8Sensors.setTotalSensors(numberOfSensorsToConnect);
        //f8Sensors = new F8SensorsManager(numberOfSensorsToConnect); 
#endif
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Dont run any of the following code in Unity Editor
#if NETFX_CORE

    private void Awake()
    {
        Debug.Log("About to start watcher"); 
        watcher = new BluetoothLEAdvertisementWatcher { ScanningMode = BluetoothLEScanningMode.Active };
        currentDeviceString = sensorOne;
        //watcher.SignalStrengthFilter.InRangeThresholdInDBm = -75;
        watcher.Received += Watcher_Received;
        watcher.Start();
        Debug.Log("BLE Watcher started"); 
    }

    private async void Watcher_Received(BluetoothLEAdvertisementWatcher sender, BluetoothLEAdvertisementReceivedEventArgs args)
    {
        // Debug.Log("Watcher Received!");
        //Debug.Log(args.Advertisement);
        //Debug.Log(args.Advertisement.ServiceUuids); //Crashes with evildevice

        //Debug.Log("Received Advertisement with strength: " + args.RawSignalStrengthInDBm);

        //var uuid = args.Advertisement.ServiceUuids; // Crashes with evildevice
        //Debug.Log("BLE got uuid"); 
        //var dataSections = args.Advertisement.DataSections; //Crashes  with evildevice
        // Debug.Log("BLE got Data Sections");

        //Debug.Log(args.AdvertisementType); //Crashes with evildevice
        //Debug.Log(args.Advertisement.GetHashCode());
        //Debug.Log(args.Advertisement.Flags);
        //Debug.Log(args.Advertisement.ToString()); 

        //Debug.Log(args.BluetoothAddress);  //Uncomment to find address of Evil Device 
        if (EVIL_DEVICES.Contains(args.BluetoothAddress))
        {
            Debug.Log("Evil Device found.... and blocked!");
            return; 
        }

        var deviceName = args.Advertisement.LocalName; //Crashes with evildevice
        //Debug.Log("BLE got device name: " + deviceName);

        if (!deviceName.Contains("f8")) return; //Not a figure8 device
        Debug.Log("device name: " + deviceName);
        Debug.Log("looking for: " + sensorToConnectTo);
        if (!deviceName.Contains(sensorToConnectTo)) return;
        Debug.Log("f8 device found: " + args.Advertisement.LocalName);

        if (args != null && args.Advertisement != null && args.Advertisement.LocalName.Length >0)
        {
           // Debug.Log("Device found: " + args.Advertisement.LocalName);
        }
        else return;

        ulong bluetoothAddress = args.BluetoothAddress;

        //if (f8Sensors.connectedSensors == f8Sensors.totalSensors) return; //All sensors have already connected
        //if (f8Sensors.isConnected(bluetoothAddress)) return; //Don't try to connect to the same device twice

        

        //if (!deviceName.Contains("f8")) return; //Not a figure8 device
        //Debug.Log("f8 device found: " + args.Advertisement.LocalName);
        //Debug.Log("BT Address: "  + args.BluetoothAddress);


        try
        {
            bleDevice = await BluetoothLEDevice.FromBluetoothAddressAsync(bluetoothAddress);
            watcher.Stop(); 
            // TODO Dont stop watching and restart watching after this sensor connects
            //Debug.Log("f8Sensors.connectedSensors: " + f8Sensors.connectedSensors);
            //if (f8Sensors.connectedSensors == f8Sensors.totalSensors -1) watcher.Stop(); //We are connecting the last sensor now, stop trying to connect more

            if (bleDevice != null)
            {
                Debug.Log("Trying to connect");
                // Setting the DevicePairingProtectionLevel to None tells the system to ignore bonding information and just look for a matching device.
                DevicePairingResult result = await bleDevice.DeviceInformation.Pairing.PairAsync(DevicePairingProtectionLevel.None);

                Debug.Log("result: " + result.Status.ToString());
                if (result.Status == DevicePairingResultStatus.Failed) Debug.Log("is the f8 sensor paird in the Hololens settings?"); 
                bleDevice.ConnectionStatusChanged += onBLEConnectionStatusChange;
                Debug.Log("Connection Status:" + bleDevice.ConnectionStatus);

                if (bleDevice.ConnectionStatus == BluetoothConnectionStatus.Connected)
                {
                    onBLEConnected(bleDevice); 
                }

                if (result.Status == DevicePairingResultStatus.AlreadyPaired && bleDevice.ConnectionStatus == BluetoothConnectionStatus.Disconnected)
                {
                    Debug.Log("Forcing connection");
                    onBLEConnected(bleDevice); 
                }

            }
        }
        catch (Exception ex)
        {
            Debug.Log(ex);
        }
    
    }

    private void onBLEConnectionStatusChange(BluetoothLEDevice bleDevice, object args)
    {
        Debug.Log("BLEConnection Status changed to: " + bleDevice.ConnectionStatus); 
        if (bleDevice.ConnectionStatus == BluetoothConnectionStatus.Connected )
        {
            onBLEConnected(bleDevice); 
        }
    }

    private async void onBLEConnected(BluetoothLEDevice bleDevice)
    {
        //int id = f8Sensors.addSensor(bleDevice);
        registerf8GattService();

        if (bleDevice.ConnectionStatus != BluetoothConnectionStatus.Connected)
        {
            Debug.Log("Not actually connected"); 
            return; 
        }
        if (sensor != null && sensor.isConnected && bleDevice.ConnectionStatus == BluetoothConnectionStatus.Connected)
        {
            Debug.Log("Sensor already connected"); 
            return;
        }
        Debug.Log("** Connected to Device");

        sensor = new F8Sensor();
	    sensor.bluetoothAddress = bleDevice.BluetoothAddress;
	    sensor.isConnected = bleDevice.ConnectionStatus == BluetoothConnectionStatus.Connected;
    }

    private bool gattServiceRegistered = false; 
    private async void registerf8GattService()
    {
        if (!gattServiceRegistered) gattServiceRegistered = true;
        else return; 
        Debug.Log("1");
        int id = 0;
        //Set up stretch
        stretchService = bleDevice.GetGattService(STRETCH_BT_SERVICE_UUID);
        stretchCharacteristic = stretchService.GetCharacteristics(STRETCH_BT_UUID)[0];
        stretchCharacteristic.ValueChanged += (c, a) => onStretchChange(c, a, id);
        // Set the notify enable flag - this tells the device to send us new data for this characteristic
        await stretchCharacteristic.WriteClientCharacteristicConfigurationDescriptorAsync(GattClientCharacteristicConfigurationDescriptorValue.Notify);

        Debug.Log("2");

        //Set up IMU rotation
        imuService = bleDevice.GetGattService(IMU_BT_SERVICE_UUID);
        rotationCharacteristic = imuService.GetCharacteristics(ROTATION_BT_UUID)[0];
        rotationCharacteristic.ValueChanged += (c, a) => onRotationChange(c, a, id);
        await rotationCharacteristic.WriteClientCharacteristicConfigurationDescriptorAsync(GattClientCharacteristicConfigurationDescriptorValue.Notify);

        Debug.Log("3");

        //Set up IMU acceleration
        //accelerationCharacteristic = imuService.GetCharacteristics(ACCELERATION_BT_UUID)[0];
        //accelerationCharacteristic.ValueChanged += (c,a) =>  onAccelerationChange(c,a,id);
        //await accelerationCharacteristic.WriteClientCharacteristicConfigurationDescriptorAsync(GattClientCharacteristicConfigurationDescriptorValue.Notify);
    }

    private void onStretchChange(GattCharacteristic stretchCharacteristic, GattValueChangedEventArgs args, int id)
    {
        //Debug.Log("onStretchChange called");
        byte[] stretch_data = args.CharacteristicValue.ToArray();

        //Debug.Log(PrintBytes(stretch_data));

        int stretch = stretch_data[1]; // int representing stretch from 0 to ~100

        //f8Sensors.updateSensorStretch(stretch, id); 
	//sensor.stretch = stretch;
	transformReceiver.updateStretch(stretch);
        //Debug.Log("stretch: " + stretch+ " sensor.stretch: "+ sensor.stretch);
        //Debug.Log("sensor.stretch: " + sensor.stretch);

    }

    private void onRotationChange(GattCharacteristic rotationCharacteristic, GattValueChangedEventArgs args, int id)
    {
        //float QUAT_CONVERSION = (float)9.31322574615478515625e-10;
        float QUAT_CONVERSION = 1; 

        //Debug.Log("onRotationChange called");
        byte[] rotation_data = args.CharacteristicValue.ToArray();

        // Split data acording to f8 ble spec sheet
        int w = (int)(BitConverter.ToInt32(rotation_data.Skip(0).Take(4).ToArray(), 0) * QUAT_CONVERSION);
        int x = (int)(BitConverter.ToInt32(rotation_data.Skip(4).Take(4).ToArray(), 0) * QUAT_CONVERSION);
        int y = (int)(BitConverter.ToInt32(rotation_data.Skip(8).Take(4).ToArray(), 0) * QUAT_CONVERSION);
        int z = (int)(BitConverter.ToInt32(rotation_data.Skip(12).Take(4).ToArray(), 0) * QUAT_CONVERSION);

        rotation.w = w; rotation.x = x; rotation.y = y; rotation.z = z;

        // Match Unity axis
        rotation = rotation * Quaternion.AngleAxis(90, new Vector3(1, 0, 0));
        rotation = rotation * Quaternion.AngleAxis(90, new Vector3(0, 0, 1));

        //Reorient IMU side
        Quaternion offset = Quaternion.AngleAxis(180, new Vector3(1, 0, 0));
        offset = offset * Quaternion.AngleAxis(180, new Vector3(0, 0, 1));
        offset = offset * Quaternion.AngleAxis(90, new Vector3(1, 0, 0));
        // Rotation offsets
        rotation = offset * rotation;

        //f8Sensors.updateSensorRotation(rotation, id); 
	//sensor.rotation = rotation;
	transformReceiver.updateRotation(rotation);
        //Debug.Log("rotation: " + rotation);
        //Debug.Log(PrintBytes(rotation_data)); 
    }

    private void onAccelerationChange(GattCharacteristic accelerationCharacteristic, GattValueChangedEventArgs args, int id)
    {
        byte[] acceleration_data = args.CharacteristicValue.ToArray();

        // Split data according to f8 ble spec
        float x = BitConverter.ToSingle(acceleration_data.Skip(0).Take(4).ToArray(), 0);
        float y = BitConverter.ToSingle(acceleration_data.Skip(4).Take(4).ToArray(), 0);
        float z = BitConverter.ToSingle(acceleration_data.Skip(8).Take(4).ToArray(), 0);
        
       
        acceleration.x = x; acceleration.y = y; acceleration.z = z;

        //f8Sensors.updateSensorAcceleration(acceleration, id); 
	sensor.acceleration = acceleration;
	transformReceiver.updateAcceleration(acceleration);
        //Debug.Log("acceleration: "+ acceleration);
        //Debug.Log("x:" + x + " y: " + y + " z: " + z); 
    }


    // Debugging Functions
    string PrintBytes(byte[] byteArray)
    {
        var sb = new StringBuilder("new byte[] { ");
        for (var i = 0; i < byteArray.Length; i++)
        {
            var b = byteArray[i];
            sb.Append(b);
            if (i < byteArray.Length - 1)
            {
                sb.Append(", ");
            }
        }
        sb.Append(" }");
        return sb.ToString();
    }
#endif

}