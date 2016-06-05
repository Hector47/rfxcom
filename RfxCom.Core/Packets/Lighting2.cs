/*
 *	 RFXCom Core library Lighting2
 *	 Author: Hector Cachoulet
 *	
 *	 Licensed to Constellation under one or more contributor
 *	 license agreements. Constellation licenses this file to you under
 *	 the Apache License, Version 2.0 (the "License"); you may
 *	 not use this file except in compliance with the License.
 *	 You may obtain a copy of the License at
 *	
 *	 http://www.apache.org/licenses/LICENSE-2.0
 *	
 *	 Unless required by applicable law or agreed to in writing,
 *	 software distributed under the License is distributed on an
 *	 "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 *	 KIND, either express or implied. See the License for the
 *	 specific language governing permissions and limitations
 *	 under the License.
 */


namespace RfxCom.Packets
{
    using System.Collections.Generic;
    using Core;

    [RfxPacket(Type = RfxPacketType.Lighting2, Length = 11)]
    public class Lighting2 : RfxPacket
    {
        private static readonly Dictionary<byte, string> rfx_subtype_11 = new Dictionary<byte, string>()
        {
            [0x00] = "AC",
            [0x01] = "HomeEasy EU",
            [0x02] = "ANSLUT",
            [0x03] = "Kambrook RF3672",
        };

        public string Id { get; set; }
        public int UnitCode { get; set; }

        public Lighting2Command Command { get; set; }

        public int Level { get; set; } //0x0 to 0xF

        public int SignalLevel { get; set; }

        public override void Parse(byte[] packet)
        {
            base.Parse(packet);
            this.SubTypeName = rfx_subtype_11[(byte)this.SubType];
            this.Id = packet[4].ToString("X2") + packet[5].ToString("X2") + packet[6].ToString("X2") + packet[7].ToString("X2");//int.Parse(, System.Globalization.NumberStyles.HexNumber);
            this.UnitCode = packet[8];
            this.Command = (Lighting2Command)packet[9];
            this.Level = packet[10];
            this.SignalLevel = packet[11] >> 4;

        }

        public override string ToString()
        {
            return $"[Lighting2] ID={Id}-{UnitCode} Command:{Command} Level:{Level}  Signal:{SignalLevel}";
        }
    }
}
