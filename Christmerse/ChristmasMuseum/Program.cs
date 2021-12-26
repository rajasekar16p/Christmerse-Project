using StereoKit;
using System;

namespace ChristmasMuseum
{
    class Program
    {
        static Mesh floorMesh;
        static Material floorMat;
        static int buttonSwitchCount = -1;
        //static Pose windowPoseButton = new Pose(-1, 0, 0, Quat.LookDir(1, 0, -0.5f));
        static Pose windowPoseButton = new Pose(-1, 0, 0, Quat.LookDir(1, 0, 1f));

        static Model room = Model.FromFile("circular_showroom/scene.gltf");
       
        
        static Model ruler = Model.FromFile("ruler/scene.gltf");
        static Model tree = Model.FromFile("xmas/tree/scene.gltf");
        static Model neon = Model.FromFile("xmas/neon/scene.gltf");
        static Model xmasscene = Model.FromFile("xmas/xmasscene/scene.gltf");
        static Model nutcracker = Model.FromFile("xmas/nutcracker/scene.gltf");
        static Model chair = Model.FromFile("xmas/chair/scene.gltf");
        static Model reindeer = Model.FromFile("xmas/reindeer/scene.gltf");
        static Model santa = Model.FromFile("xmas/santa_claus/scene.gltf");
        static Model sleigh = Model.FromFile("xmas/sleigh/scene.gltf");
        static Model snowglb = Model.FromFile("xmas/snow_globe/scene.gltf");
        static Model stars = Model.FromFile("xmas/stars/scene.gltf");
        static Model sky = Model.FromFile("xmas/sky/scene.gltf");



        static void Main(string[] args)
        {
            // Initialize StereoKit
            SKSettings settings = new SKSettings
            {
                appName = "Christmerse",
                assetsFolder = "Assets",
            };
            if (!SK.Initialize(settings))
                Environment.Exit(1);


            Matrix floorTransform = Matrix.TS(0, -1.5f, 0, new Vec3(30, 0.1f, 30));
            Material floorMaterial = new Material(Shader.FromFile("floor.hlsl"));
            floorMaterial.Transparency = Transparency.Blend;

            InitializeFloor();
            InitMaterials();

            // You can look at the model's animations:
            //foreach (Anim anim in neon.Anims)
           //     Log.Info($"Animation: {anim.Name} {anim.Duration}s");

            // You can play an animation like this
            neon.PlayAnim("Animation", AnimMode.Loop);


            // Core application loop
            while (SK.Step(() =>
            {
                //if (SK.System.displayType == Display.Opaque)
                //    Default.MeshCube.Draw(floorMaterial, floorTransform);

                //S floorMesh?.Draw(floorMat, Matrix.T(0, -1.5f, 0));

                //  ruler.Draw(Matrix.TRS(new Vec3(0, 2,-2), Quat.FromAngles(180, -90, -90), 0.00115f * 10));//1cm = 10cm
                //ruler.Draw(Matrix.TRS(new Vec3(0, 0, 0), Quat.FromAngles(90, 90, 90), 0.00115f));//1cm = 1cm
                //room.Draw(Matrix.TS(new Vec3(0, -1.5f, 0), 1f));//S



                ShowSwitchButtonUI();
                ShowModels();

            })) ;
            SK.Shutdown();
        }
        static void InitializeFloor()
        {
            // Add a floor if we're in VR
            floorMesh = Mesh.GeneratePlane(new Vec2(100, 100));
            floorMat = Default.Material.Copy();
            floorMat[MatParamName.DiffuseTex] = Tex.FromFile("pure-white-snow-surface.jpg");
            floorMat[MatParamName.TexScale] = 8;
        }

        static void InitMaterials()
        {
           // Material cuban_macaw_mat = Material.Unlit.Copy();
            //cuban_macaw_mat[MatParamName.DiffuseTex] = Tex.FromFile("cuban_macaw/textures/material_0_diffuse.png");
          //  cuban_macaw.SetMaterial(0, cuban_macaw_mat);
        }

        static void ShowSwitchButtonUI()
        {
            UI.WindowBegin("", ref windowPoseButton, new Vec2(30, 0) * U.cm, false ? UIWin.Normal : UIWin.Body);
            UI.ColorScheme = Color.White;
            UI.HSeparator();
            UI.Label("CHRISTMERSE");

            string btnNextText = "Next", btnPreviousText = "Previous";
            string xmasName = "";
            //string animalInfo = "";

            if (buttonSwitchCount == -1)
            {
                xmasName = "Merry Christmas and Happy New Year 2022";
                if (UI.Button("Start"))
                {
                    buttonSwitchCount = 0;
                }
            }
            else
            {
                if (UI.Button(btnPreviousText))
                {
                    if (buttonSwitchCount > 0)
                    {
                        buttonSwitchCount--;
                    }
                    else
                    {
                        buttonSwitchCount = 7;
                    }
                }

                UI.SameLine();

                if (UI.Button(btnNextText))
                {
                    if (buttonSwitchCount < 7)
                    {
                        buttonSwitchCount++;
                    }
                    else
                    {
                        buttonSwitchCount = 0;
                    }
                }
            }

            switch (buttonSwitchCount)
            {
                case 0:
                    xmasName = "Christmas Tree";
                    break;
                case 1:
                    xmasName = "Santa Claus";
                    break;
                case 2:
                    xmasName = "Santa's sleigh";
                    break;
                case 3:
                    xmasName = "Santa Chair";
                    break;
                case 4:
                    xmasName = "Rudolph Reindeer";
                    break;
                case 5:
                    xmasName = "Nutcracker";
                    break;
                case 6:
                    xmasName = "Snow Globe";
                    break;
                case 7:
                    xmasName = "Christmas Land";
                    break;
            }
            UI.HSeparator();
            UI.Label(xmasName);

            UI.WindowEnd();
        }

        static void ShowModels()
        {
            sky.Draw(Matrix.TS(new Vec3(0, -1.5f, 0), 0.07f));
            if (buttonSwitchCount != 7)
            {
                floorMesh?.Draw(floorMat, Matrix.T(0, -1.5f, 0));
            }
            switch (buttonSwitchCount)
            {
                case -1:
                    neon.Draw(Matrix.TS(new Vec3(0, -0.9f, -2), 1.4f));
                    break;
                case 0:
                    tree.Draw(Matrix.TS(new Vec3(0, -1.5f,-2), 1.5f));
                    break;
                case 1:
                    santa.Draw(Matrix.TS(new Vec3(0, 0.4f, -2), 0.011f));
                    break;
                case 2:
                    sleigh.Draw(Matrix.TS(new Vec3(0, -1f, -2.5f), 0.004f));
                    break;
                case 3:
                    chair.Draw(Matrix.TRS(new Vec3(0, -0.535f,-2), Quat.FromAngles(0, -90, 0), 2f));
                    break;
                case 4:
                    reindeer.Draw(Matrix.TRS(new Vec3(0, -1.5f, -2), Quat.FromAngles(0, -135, 0), 0.02f));
                    break;
                case 5:
                    nutcracker.Draw(Matrix.TRS(new Vec3(0, -1.5f, -2), Quat.FromAngles(0, -180, 0), 0.02f));
                    break;
                case 6:
                    snowglb.Draw(Matrix.TRS(new Vec3(0, -1.50f, -2), Quat.FromAngles(0, -180, 0), 0.2f));
                    break;
                case 7:
                    xmasscene.Draw(Matrix.TS(new Vec3(0, -1.5f, -4.5f), 1f));
                    break;
            }
        }
    }
}
