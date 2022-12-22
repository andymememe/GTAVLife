using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using GTA;
using GTA.Native;
using GTAVLife.GameData;

namespace GTAVLife.Helper
{
    public class MPFreemodeModel
    {
        public int Hair { get; set; }
        public int Upper { get; set; }
        public int Lower { get; set; }
        public int Hands { get; set; }
        public int Shoes { get; set; }
        public int Accessory { get; set; }
        public int ShirtOverlay { get; set; }
        public int UpperT { get; set; }
        public int LowerT { get; set; }
        public int HandsT { get; set; }
        public int ShoesT { get; set; }
        public int AccessoryT { get; set; }
        public int ShirtOverlayT { get; set; }
        public int Model { get; set; }
        public int Wrist { get; set; }
        public int WristT { get; set; }
        public int Watch { get; set; }
        public int WatchT { get; set; }
        public int S1 { get; set; }
        public int S2 { get; set; }
        public int S3 { get; set; }
        public int SK1 { get; set; }
        public int SK2 { get; set; }
        public int SK3 { get; set; }
        public double F1 { get; set; }
        public double F2 { get; set; }
        public double F3 { get; set; }
        public int HairColor { get; set; }
        public int Eyebrows { get; set; }
        public int EyebrowsColor { get; set; }
        public int Lipstick { get; set; }
        public int LipstickColor { get; set; }
        public int EyesColor { get; set; }
        public double Shape0 { get; set; }
        public double Shape1 { get; set; }
        public double Shape2 { get; set; }
        public double Shape3 { get; set; }
        public double Shape4 { get; set; }
        public double Shape5 { get; set; }
        public double Shape6 { get; set; }
        public double Shape7 { get; set; }
        public double Shape8 { get; set; }
        public double Shape9 { get; set; }
        public double Shape10 { get; set; }
        public double Shape11 { get; set; }
        public double Shape12 { get; set; }
        public double Shape13 { get; set; }
        public double Shape14 { get; set; }
        public double Shape15 { get; set; }
        public double Shape16 { get; set; }
        public double Shape17 { get; set; }
        public double Shape18 { get; set; }
        public string Voice { get; set; }
    }

    public class MPFreemodeModels : SimpleSingletonBase<MPFreemodeModels>
    {
        public Dictionary<string, MPFreemodeModel> Models => models;
        private Dictionary<string, MPFreemodeModel> models;

        public void LoadModels(string path)
        {
            string content = File.ReadAllText(path);
            this.models = JsonConvert.DeserializeObject<Dictionary<string, MPFreemodeModel>>(content);
        }
    }

    public class SkinChanger
    {
        public static void Change(string name)
        {
            MPFreemodeModel model;
            if (MPFreemodeModels.Instance.Models.TryGetValue(name, out model))
            {
                if (PlayerInfo.Player.ChangeModel(new Model(model.Model)))
                {
                    setParentShape(model);
                    setShape(model);
                    setHair(model);
                    setFacialFeature(model);
                    setClothes(model);

                    PlayerInfo.Character.Voice = model.Voice;
                    disablePain();
                }
            }
        }

        private static void setParentShape(MPFreemodeModel model)
        {
            Function.Call(
                Hash.SET_PED_HEAD_BLEND_DATA,
                PlayerInfo.Character,
                model.S1, model.S2, model.S3, // Shape
                model.SK1, model.SK2, model.SK3, // Skin
                model.F1, model.F2, model.F3, // Mix
                false
            );
        }

        private static void setShape(MPFreemodeModel model)
        {
            Function.Call(Hash._SET_PED_FACE_FEATURE, PlayerInfo.Character, 0, model.Shape0);
            Function.Call(Hash._SET_PED_FACE_FEATURE, PlayerInfo.Character, 1, model.Shape1);
            Function.Call(Hash._SET_PED_FACE_FEATURE, PlayerInfo.Character, 2, model.Shape2);
            Function.Call(Hash._SET_PED_FACE_FEATURE, PlayerInfo.Character, 3, model.Shape3);
            Function.Call(Hash._SET_PED_FACE_FEATURE, PlayerInfo.Character, 4, model.Shape4);
            Function.Call(Hash._SET_PED_FACE_FEATURE, PlayerInfo.Character, 5, model.Shape5);
            Function.Call(Hash._SET_PED_FACE_FEATURE, PlayerInfo.Character, 6, model.Shape6);
            Function.Call(Hash._SET_PED_FACE_FEATURE, PlayerInfo.Character, 7, model.Shape7);
            Function.Call(Hash._SET_PED_FACE_FEATURE, PlayerInfo.Character, 8, model.Shape8);
            Function.Call(Hash._SET_PED_FACE_FEATURE, PlayerInfo.Character, 9, model.Shape9);
            Function.Call(Hash._SET_PED_FACE_FEATURE, PlayerInfo.Character, 10, model.Shape10);
            Function.Call(Hash._SET_PED_FACE_FEATURE, PlayerInfo.Character, 11, model.Shape11);
            Function.Call(Hash._SET_PED_FACE_FEATURE, PlayerInfo.Character, 12, model.Shape12);
            Function.Call(Hash._SET_PED_FACE_FEATURE, PlayerInfo.Character, 13, model.Shape13);
            Function.Call(Hash._SET_PED_FACE_FEATURE, PlayerInfo.Character, 14, model.Shape14);
            Function.Call(Hash._SET_PED_FACE_FEATURE, PlayerInfo.Character, 15, model.Shape15);
            Function.Call(Hash._SET_PED_FACE_FEATURE, PlayerInfo.Character, 16, model.Shape16);
            Function.Call(Hash._SET_PED_FACE_FEATURE, PlayerInfo.Character, 17, model.Shape17);
            Function.Call(Hash._SET_PED_FACE_FEATURE, PlayerInfo.Character, 18, model.Shape18);
        }

        private static void setHair(MPFreemodeModel model)
        {
            Function.Call(Hash.SET_PED_COMPONENT_VARIATION, PlayerInfo.Character, 2, model.Hair-1, 0, 0);
            Function.Call(Hash._SET_PED_HAIR_COLOR, PlayerInfo.Character, model.HairColor, 0);
        }

        private static void setFacialFeature(MPFreemodeModel model)
        {
            // Eyes color
            Function.Call(Hash._SET_PED_EYE_COLOR, PlayerInfo.Character, model.EyesColor);

            // Eyebrow & Lipstick style
            Function.Call(Hash.SET_PED_HEAD_OVERLAY, PlayerInfo.Character, 2, model.Eyebrows, 1.0);
            Function.Call(Hash.SET_PED_HEAD_OVERLAY, PlayerInfo.Character, 8, model.Lipstick, 1.0);

            // Eyebrow & Lipstick color
            Function.Call(Hash._SET_PED_HEAD_OVERLAY_COLOR, PlayerInfo.Character, 2, 1, model.EyebrowsColor, model.EyebrowsColor);
            Function.Call(Hash._SET_PED_HEAD_OVERLAY_COLOR, PlayerInfo.Character, 8, 2, model.LipstickColor, model.LipstickColor);
        }

        private static void setClothes(MPFreemodeModel model)
        {
            // Clothes
            Function.Call(Hash.SET_PED_COMPONENT_VARIATION, PlayerInfo.Character, 3, model.Upper-1, model.UpperT-1, 0);
            Function.Call(Hash.SET_PED_COMPONENT_VARIATION, PlayerInfo.Character, 4, model.Lower-1, model.LowerT-1, 0);
            Function.Call(Hash.SET_PED_COMPONENT_VARIATION, PlayerInfo.Character, 6, model.Shoes-1, model.ShoesT-1, 0);
            Function.Call(Hash.SET_PED_COMPONENT_VARIATION, PlayerInfo.Character, 8, model.Accessory-1, model.AccessoryT-1, 0);
            Function.Call(Hash.SET_PED_COMPONENT_VARIATION, PlayerInfo.Character, 11, model.ShirtOverlay-1, model.ShirtOverlayT-1, 0);

            // Watch
            if (model.Watch > 0)
            {
                Function.Call(Hash.SET_PED_PROP_INDEX, PlayerInfo.Character, 6, model.Watch-1, model.WatchT-1, true);
            }

            // Wrist
            if (model.Wrist > 0)
            {
                Function.Call(Hash.SET_PED_PROP_INDEX, PlayerInfo.Character, 7, model.Wrist-1, model.WristT-1, true);
            }
        }

        private static void disablePain()
        {
            Function.Call(Hash.DISABLE_PED_PAIN_AUDIO, PlayerInfo.Character, true);
        }
    }
}