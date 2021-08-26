using GameLib.Components;
using GameLib.Graphics;

using System;


namespace RunToTheStairs.WPF
{
    class ApperanceProvider : IApperanceProvider
    {
        private readonly string[] belts = new string[]
        {
            "BELT_leather_walk",
            "BELT_rope_walk"
        };
        private readonly string[] feets = new string[]
        {
            "FEET_plate_armor_shoes_walk",
            "FEET_shoes_brownwalk"
        };
        private readonly string[] hands = new string[]
        {
            "HANDS_plate_armor_gloves_walk",
        };
        private readonly string[] heads = new string[]
        {
            "HEAD_chain_armor_hood_walk",
            "HEAD_hair_blonde_walk",
            "HEAD_leather_armor_hat_walk",
            "HEAD_plate_armor_helmet_walk",
            "HEAD_robe_hood_walk",
        };
        private readonly string[] legs = new string[]
        {
            "LEGS_pants_greenish_walk",
            "LEGS_plate_armor_pants_walk",
            "LEGS_robe_skirt_walk",
        };
        private readonly string[] torsos = new string[]
        {
            "TORSO_chain_armor_jacket_purple_walk",
            "TORSO_chain_armor_torso_walk",
            "TORSO_leather_armor_bracers_walk",
            "TORSO_leather_armor_shirt_white_walk",
            "TORSO_leather_armor_shoulders_walk",
            "TORSO_leather_armor_torso_walk",
            "TORSO_plate_armor_arms_shoulders_walk",
            "TORSO_plate_armor_torso_walk",
            "TORSO_robe_shirt_brown_walk"
,       };
        private readonly string[] bodies = new string[]
        {
            "BODY_male_walk",
            "BODY_skeleton_walk"
,       };

        private Random random_ = new Random();

        public Apperance GetEntityApperance()
        {
            Apperance apperance = new Apperance();

            CreateRandomOrNone(apperance, bodies, false);
            CreateRandomOrNone(apperance, feets);
            CreateRandomOrNone(apperance, legs);
            CreateRandomOrNone(apperance, torsos);
            CreateRandomOrNone(apperance, belts);
            CreateRandomOrNone(apperance, heads);
            CreateRandomOrNone(apperance, hands);

            return apperance;
        }

        private void CreateRandomOrNone(Apperance apperance, string[] array,
            bool allowNone = true)
        {
            int index = random_.Next(array.Length + (allowNone ? 1 : 0));
            if (index != array.Length)
            {
                apperance.Sprites.Add(new SpriteDesc()
                {
                    Name = array[index],
                });
            }


        }
    }
}
