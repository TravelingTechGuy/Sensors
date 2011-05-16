using System;
using Microsoft.SPOT;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.NetduinoPlus;
using MicroLiquidCrystal;

namespace Sensors
{
    class LCD
    {
        private Lcd lcd;

        public LCD()
        {
            var lcdProvider = new GpioLcdTransferProvider(
                Pins.GPIO_PIN_D12,  // RS
                Pins.GPIO_NONE,     // RW
                Pins.GPIO_PIN_D11,  // enable
                Pins.GPIO_PIN_D9,   // d0
                Pins.GPIO_PIN_D8,   // d1
                Pins.GPIO_PIN_D7,   // d2
                Pins.GPIO_PIN_D6,   // d3
                Pins.GPIO_PIN_D5,   // d4
                Pins.GPIO_PIN_D4,   // d5
                Pins.GPIO_PIN_D3,   // d6
                Pins.GPIO_PIN_D2);  // d7

            // create the LCD interface
            lcd = new Lcd(lcdProvider);

            // set up the number of columns and rows:
            lcd.Begin(16, 2);
        }

        public void Write(string line)
        {
            lcd.Write(line);
        }

        public void SetCursorPosition(short column, short row)
        {
            lcd.SetCursorPosition(column, row);
        }

        public void Clear()
        {
            lcd.Clear();
        }
    }
}
