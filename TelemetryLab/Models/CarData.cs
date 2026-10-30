namespace TelemetryLab.Models;

public class CarData
{
    public CarData(int position, double rpm, double speed, int gear, double throttle, double brake, double clutch, double steering, double fuel, string frontTyreCompound, string rearTyreCompound)
    {
        Position = position;
        Rpm = rpm;
        Speed = speed;
        Gear = gear;
        Throttle = throttle;
        Brake = brake;
        Clutch = clutch;
        Steering = steering;
        Fuel = fuel;
        FrontTyreCompound = frontTyreCompound;
        RearTyreCompound = rearTyreCompound;
    }

        public int Position { get; set; }
        public double Rpm { get; set; }
        public double Speed { get; set; }
        public int Gear { get; set; }
        public double Throttle { get; set; }
        public double Brake { get; set; }
        public double Clutch { get; set; }
        public double Steering { get; set; }
        public double Fuel { get; set; }
        public string FrontTyreCompound { get; set; }
        public string RearTyreCompound { get; set; }

    
}

// position, rpm, speed, gear, throttle brake clutch, fuel, tire wear, tire temp, tire pressure