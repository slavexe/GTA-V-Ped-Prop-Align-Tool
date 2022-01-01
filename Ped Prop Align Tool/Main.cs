using GTA;
using GTA.Native;
using GTA.UI;
using LemonUI;
using LemonUI.Menus;
using System.Windows.Forms;

namespace Ped_Prop_Align_Tool
{
    public partial class Main : Script
    {
        bool loadModel = false;
        bool loadProp = false;
        bool softPinning = false;
        bool collision = false;
        bool isPed = false;
        bool fixedRot = true;
        bool propLoaded = false;
        bool xInvert = false;
        bool yInvert = false;
        bool zInvert = false;
        string propInput = "";
        string boneInput = "";
        string vertexInput = "";
        string clipboardText = "";
        int boneIndex = 90;
        int vertex = 2;
        float propXpos = 0f;
        float propYpos = 0f;
        float propZpos = 0f;
        float propXrot = 0f;
        float propYrot = 0f;
        float propZrot = 0f;

        Model propModel;
        Prop? prop;

        ObjectPool mainPool = new ObjectPool();
        NativeMenu mainMenu = new NativeMenu("Ped Prop Align Tool", "Aligns Peds and Props")
        {
            Width = 500,
        };
        NativeMenu adjustmentMenu = new NativeMenu("Ped Prop Align Tool", "Adjust Position and Rotation")
        {
            Width = 500,
        };
        NativeItem propItem = new NativeItem("Input Prop Name");
        NativeItem boneItem = new NativeItem("Input Bone Index");
        NativeItem vertexItem = new NativeItem("Input Vertex");
        NativeCheckboxItem softPinningItem = new NativeCheckboxItem("Enable Soft Pinning")
        {
            Checked = false,
        };
        NativeCheckboxItem collisionItem = new NativeCheckboxItem("Enable Collision")
        {
            Checked = false,
        };
        NativeCheckboxItem isPedItem = new NativeCheckboxItem("Enable Is Ped")
        {
            Checked = false,
        };
        NativeCheckboxItem fixedRotItem = new NativeCheckboxItem("Enable Fixed Rotation")
        {
            Checked = true,
        };
        NativeItem addItem = new NativeItem("Add Prop");
        NativeItem removeItem = new NativeItem("Remove Prop");
        NativeCheckboxItem xinvertItem = new NativeCheckboxItem("Invert X Position")
        {
            Checked = false,
        };
        NativeSliderItem xPosSlider = new NativeSliderItem("X Position")
        {
            Maximum = 9,
            Value = 0,
        };
        NativeSliderItem xPosSliderTe = new NativeSliderItem("X Position Tenth")
        {
            Maximum = 9,
            Value = 0,
        };
        NativeSliderItem xPosSliderHu = new NativeSliderItem("X Position Hundredth")
        {
            Maximum = 9,
            Value = 0,
        };
        NativeSliderItem xPosSliderTo = new NativeSliderItem("X Position Thousandth")
        {
            Maximum = 9,
            Value = 0,
        };
        NativeSeparatorItem xySeperator = new NativeSeparatorItem();
        NativeCheckboxItem yinvertItem = new NativeCheckboxItem("Invert Y Position")
        {
            Checked = false,
        };
        NativeSliderItem yPosSlider = new NativeSliderItem("Y Position")
        {
            Maximum = 9,
            Value = 0,
        };
        NativeSliderItem yPosSliderTe = new NativeSliderItem("Y Position Tenth")
        {
            Maximum = 9,
            Value = 0,
        };
        NativeSliderItem yPosSliderHu = new NativeSliderItem("Y Position Hundredth")
        {
            Maximum = 9,
            Value = 0,
        };
        NativeSliderItem yPosSliderTo = new NativeSliderItem("Y Position Thousandth")
        {
            Maximum = 9,
            Value = 0,
        };
        NativeSeparatorItem yzSeperator = new NativeSeparatorItem();
        NativeCheckboxItem zinvertItem = new NativeCheckboxItem("Invert Z Position")
        {
            Checked = false,
        };
        NativeSliderItem zPosSlider = new NativeSliderItem("Z Position")
        {
            Maximum = 9,
            Value = 0,
        };
        NativeSliderItem zPosSliderTe = new NativeSliderItem("Z Position Tenth")
        {
            Maximum = 9,
            Value = 0,
        };
        NativeSliderItem zPosSliderHu = new NativeSliderItem("Z Position Hundredth")
        {
            Maximum = 9,
            Value = 0,
        };
        NativeSliderItem zPosSliderTo = new NativeSliderItem("Z Position Thousandth")
        {
            Maximum = 9,
            Value = 0,
        };
        NativeSeparatorItem zxrSeperator = new NativeSeparatorItem();
        NativeSliderItem xRotSlider = new NativeSliderItem("X Rotation")
        {
            Maximum = 360,
            Value = 0,
        };
        NativeSliderItem xRotSliderTe = new NativeSliderItem("X Rotation Tenth")
        {
            Maximum = 9,
            Value = 0,
        };
        NativeSliderItem xRotSliderHu = new NativeSliderItem("X Rotation Hundredth")
        {
            Maximum = 9,
            Value = 0,
        };
        NativeSliderItem xRotSliderTo = new NativeSliderItem("X Rotation Thousandth")
        {
            Maximum = 9,
            Value = 0,
        };
        NativeSeparatorItem xyrSeperator = new NativeSeparatorItem();
        NativeSliderItem yRotSlider = new NativeSliderItem("Y Rotation")
        {
            Maximum = 360,
            Value = 0,
        };
        NativeSliderItem yRotSliderTe = new NativeSliderItem("Y Rotation Tenth")
        {
            Maximum = 9,
            Value = 0,
        };
        NativeSliderItem yRotSliderHu = new NativeSliderItem("Y Rotation Hundredth")
        {
            Maximum = 9,
            Value = 0,
        };
        NativeSliderItem yRotSliderTo = new NativeSliderItem("Y Rotation Thousandth")
        {
            Maximum = 9,
            Value = 0,
        };
        NativeSeparatorItem yzrSeperator = new NativeSeparatorItem();
        NativeSliderItem zRotSlider = new NativeSliderItem("Z Rotation")
        {
            Maximum = 360,
            Value = 0,
        };
        NativeSliderItem zRotSliderTe = new NativeSliderItem("Z Rotation Tenth")
        {
            Maximum = 9,
            Value = 0,
        };
        NativeSliderItem zRotSliderHu = new NativeSliderItem("Z Rotation Hundredth")
        {
            Maximum = 9,
            Value = 0,
        };
        NativeSliderItem zRotSliderTo = new NativeSliderItem("Z Rotation Thousandth")
        {
            Maximum = 9,
            Value = 0,
        };
        public Main()
        {
            Tick += MainTick;
            KeyDown += OnKeyDown;

            propItem.Activated += InputProp;
            boneItem.Activated += InputBone;
            vertexItem.Activated += InputVertex;
            addItem.Activated += InputAdd;
            removeItem.Activated += InputRemove;

            softPinningItem.CheckboxChanged += InputSoftPinning;
            collisionItem.CheckboxChanged += InputCollision;
            isPedItem.CheckboxChanged += InputIsPed;
            fixedRotItem.CheckboxChanged += InputFixedRot;

            xinvertItem.CheckboxChanged += InputXInvert;
            yinvertItem.CheckboxChanged += InputYInvert;
            zinvertItem.CheckboxChanged += InputZInvert;

            xPosSlider.ValueChanged += InputXPosSlider;
            xPosSliderTe.ValueChanged += InputXPosTeSlider;
            xPosSliderHu.ValueChanged += InputXPosHuSlider;
            xPosSliderTo.ValueChanged += InputXPosToSlider;
            yPosSlider.ValueChanged += InputYPosSlider;
            yPosSliderTe.ValueChanged += InputYPosTeSlider;
            yPosSliderHu.ValueChanged += InputYPosHuSlider;
            yPosSliderTo.ValueChanged += InputYPosToSlider;
            zPosSlider.ValueChanged += InputZPosSlider;
            zPosSliderTe.ValueChanged += InputZPosTeSlider;
            zPosSliderHu.ValueChanged += InputZPosHuSlider;
            zPosSliderTo.ValueChanged += InputZPosToSlider;
            xRotSlider.ValueChanged += InputXRotSlider;
            xRotSliderTe.ValueChanged += InputXRotTeSlider;
            xRotSliderHu.ValueChanged += InputXRotHuSlider;
            xRotSliderTo.ValueChanged += InputXRotToSlider;
            yRotSlider.ValueChanged += InputYRotSlider;
            yRotSliderTe.ValueChanged += InputYRotTeSlider;
            yRotSliderHu.ValueChanged += InputYRotHuSlider;
            yRotSliderTo.ValueChanged += InputYRotToSlider;
            zRotSlider.ValueChanged += InputZRotSlider;
            zRotSliderTe.ValueChanged += InputZRotTeSlider;
            zRotSliderHu.ValueChanged += InputZRotHuSlider;
            zRotSliderTo.ValueChanged += InputZRotToSlider;

            mainPool.Add(mainMenu);
            mainPool.Add(adjustmentMenu);

            mainMenu.AddSubMenu(adjustmentMenu);
            mainMenu.Add(propItem);
            mainMenu.Add(boneItem);
            mainMenu.Add(vertexItem);
            mainMenu.Add(softPinningItem);
            mainMenu.Add(collisionItem);
            mainMenu.Add(isPedItem);
            mainMenu.Add(fixedRotItem);
            mainMenu.Add(addItem);
            mainMenu.Add(removeItem);

            adjustmentMenu.Add(xinvertItem);
            adjustmentMenu.Add(xPosSlider);
            adjustmentMenu.Add(xPosSliderTe);
            adjustmentMenu.Add(xPosSliderHu);
            adjustmentMenu.Add(xPosSliderTo);
            adjustmentMenu.Add(xySeperator);
            adjustmentMenu.Add(yinvertItem);
            adjustmentMenu.Add(yPosSlider);
            adjustmentMenu.Add(yPosSliderTe);
            adjustmentMenu.Add(yPosSliderHu);
            adjustmentMenu.Add(yPosSliderTo);
            adjustmentMenu.Add(yzSeperator);
            adjustmentMenu.Add(zinvertItem);
            adjustmentMenu.Add(zPosSlider);
            adjustmentMenu.Add(zPosSliderTe);
            adjustmentMenu.Add(zPosSliderHu);
            adjustmentMenu.Add(zPosSliderTo);
            adjustmentMenu.Add(zxrSeperator);
            adjustmentMenu.Add(xRotSlider);
            adjustmentMenu.Add(xRotSliderTe);
            adjustmentMenu.Add(xRotSliderHu);
            adjustmentMenu.Add(xRotSliderTo);
            adjustmentMenu.Add(xyrSeperator);
            adjustmentMenu.Add(yRotSlider);
            adjustmentMenu.Add(yRotSliderTe);
            adjustmentMenu.Add(yRotSliderHu);
            adjustmentMenu.Add(yRotSliderTo);
            adjustmentMenu.Add(yzrSeperator);
            adjustmentMenu.Add(zRotSlider);
            adjustmentMenu.Add(zRotSliderTe);
            adjustmentMenu.Add(zRotSliderHu);
            adjustmentMenu.Add(zRotSliderTo);

            xPosSlider.Title = "X Position: " + xPosSlider.Value;
            xPosSliderTe.Title = "X Position Tenth: " + xPosSliderTe.Value;
            xPosSliderHu.Title = "X Position Hundredth: " + xPosSliderHu.Value;
            xPosSliderTo.Title = "X Position Thousandth: " + xPosSliderTo.Value;
            yPosSlider.Title = "Y Position: " + yPosSlider.Value;
            yPosSliderTe.Title = "Y Position Tenth: " + yPosSliderTe.Value;
            yPosSliderHu.Title = "Y Position Hundredth: " + yPosSliderHu.Value;
            yPosSliderTo.Title = "Y Position Thousandth: " + yPosSliderTo.Value;
            zPosSlider.Title = "Z Position: " + zPosSlider.Value;
            zPosSliderTe.Title = "Z Position Tenth: " + zPosSliderTe.Value;
            zPosSliderHu.Title = "Z Position Hundredth: " + zPosSliderHu.Value;
            zPosSliderTo.Title = "Z Position Thousandth: " + zPosSliderTo.Value;
            xRotSlider.Title = "X Rotation: " + xRotSlider.Value;
            xRotSliderTe.Title = "X Rotation Tenth: " + xRotSliderTe.Value;
            xRotSliderHu.Title = "X Rotation Hundredth: " + xRotSliderHu.Value;
            xRotSliderTo.Title = "X Rotation Thousandth: " + xRotSliderTo.Value;
            yRotSlider.Title = "Y Rotation: " + yRotSlider.Value;
            yRotSliderTe.Title = "Y Rotation Tenth: " + yRotSliderTe.Value;
            yRotSliderHu.Title = "Y Rotation Hundredth: " + yRotSliderHu.Value;
            yRotSliderTo.Title = "Y Rotation Thousandth: " + yRotSliderTo.Value;
            zRotSlider.Title = "Z Rotation: " + zRotSlider.Value;
            zRotSliderTe.Title = "Z Rotation Tenth: " + zRotSliderTe.Value;
            zRotSliderHu.Title = "Z Rotation Hundredth: " + zRotSliderHu.Value;
            zRotSliderTo.Title = "Z Rotation Thousandth: " + zRotSliderTo.Value;

            clipboardText = "X Pos: " + propXpos + ", Prop Y Pos: " + propYpos + ", Prop Z Pos: " + propZpos + ", Prop X Rot: " + propXrot + ", Prop Y Rot: " + propYrot + ", Prop Z Rot: " + propZrot;
        }
        private void MainTick(object sender, EventArgs e)
        {
            mainPool.Process();
            if (loadModel)
            {
                propModel = new Model(propInput);
                propModel.Request(5000);
                if (propModel.IsValid && propModel.IsInCdImage)
                {
                    while (!propModel.IsLoaded) Wait(50);
                    Notification.Show("~g~Successfully loaded prop model");
                } else
                {
                    Notification.Show("~r~Could not load prop model. Check spelling and validity of model.");
                }
                loadModel = false;
            }
            if (loadProp)
            {
                if (propModel.IsLoaded)
                {
                    prop = World.CreateProp(propModel, Game.Player.Character.Position, false, false);
                    propLoaded = true;
                    loadProp = false;
                    updateProp();
                } else
                {
                    if (loadModel)
                    {
                        while (!propModel.IsLoaded) Wait(50);
                        prop = World.CreateProp(propModel, Game.Player.Character.Position, false, false);
                        propLoaded = true;
                        loadProp = false;
                        updateProp();
                    } else
                    {
                        if (propInput != "")
                        {
                            loadModel = true;
                            while (!propModel.IsLoaded) Wait(50);
                            prop = World.CreateProp(propModel, Game.Player.Character.Position, false, false);
                            propLoaded = true;
                            loadProp = false;
                            updateProp();
                        } else
                        {
                            Notification.Show("~o~Please choose a prop before attempting to load it");
                            loadProp = false;
                        }
                    }
                }
            }
        }
        private void InputProp(object sender, EventArgs e)
        {
            propInput = Game.GetUserInput();
            if (propInput.Contains("\""))
            {
                for (int i = 0; i < propInput.Length; i++)
                {
                    if (propInput[i] == '"')
                    {
                        propInput.Remove(i);
                    }
                }
            }
            Notification.Show("Prop ~g~" + propInput + " ~s~Selected");
            loadModel = true;
            updateProp();
        }
        private void InputBone(object sender, EventArgs e)
        {
            boneInput = Game.GetUserInput();
            if (int.TryParse(boneInput, out boneIndex))
            {
                Notification.Show("Bone Index ~g~" + boneIndex + " ~s~Selected");
            } else
            {
                Notification.Show("~r~Only integer values allowed");
            }
            updateProp();
        }
        private void InputVertex(object sender, EventArgs e)
        {
            vertexInput = Game.GetUserInput();
            if (int.TryParse(vertexInput, out vertex))
            {
                Notification.Show("Vertex ~g~" + vertex + " ~s~Selected");
            } else
            {
                Notification.Show("~r~Only integer values allowed");
            }
            updateProp();
        }
        private void InputSoftPinning(object sender, EventArgs e)
        {
            softPinning = softPinningItem.Checked;
        }
        private void InputCollision(object sender, EventArgs e)
        {
            collision = collisionItem.Checked;
        }
        private void InputIsPed(object sender, EventArgs e)
        {
            isPed = isPedItem.Checked;
        }
        private void InputFixedRot(object sender, EventArgs e)
        {
            fixedRot = fixedRotItem.Checked;
        }
        private void InputXInvert(object sender, EventArgs e)
        {
            xInvert = !xInvert;
            updateProp();
            updateNames();
        }
        private void InputYInvert(object sender, EventArgs e)
        {
            yInvert = !yInvert;
            updateProp();
            updateNames();
        }
        private void InputZInvert(object sender, EventArgs e)
        {
            zInvert = !zInvert;
            updateProp();
            updateNames();
        }
        private void InputAdd(object sender, EventArgs e)
        {
            if (prop == null)
            {
                loadProp = true;
            } else if (prop != null && !propLoaded)
            {
                loadProp = true;
            }
            else
            {
                Notification.Show("~o~Prop already loaded");
            }
        }
        private void InputRemove(object sender, EventArgs e)
        {
            if (prop != null)
            {
                prop.Detach();
                prop.Delete();
                propLoaded = false;
            } else
            {
                Notification.Show("~o~Prop not loaded, nothing to delete");
            }
        }
        private void InputXPosSlider(object sender, EventArgs e)
        {
            updateProp();
            updateNames();
        }
        private void InputYPosSlider(object sender, EventArgs e)
        {
            updateProp();
            updateNames();
        }
        private void InputZPosSlider(object sender, EventArgs e)
        {
            updateProp();
            updateNames();
        }
        private void InputXPosTeSlider(object sender, EventArgs e)
        {
            updateProp();
            updateNames();
        }
        private void InputYPosTeSlider(object sender, EventArgs e)
        {
            updateProp();
            updateNames();
        }
        private void InputZPosTeSlider(object sender, EventArgs e)
        {
            updateProp();
            updateNames();
        }
        private void InputXPosHuSlider(object sender, EventArgs e)
        {
            updateProp();
            updateNames();
        }
        private void InputYPosHuSlider(object sender, EventArgs e)
        {
            updateProp();
            updateNames();
        }
        private void InputZPosHuSlider(object sender, EventArgs e)
        {
            updateProp();
            updateNames();
        }
        private void InputXPosToSlider(object sender, EventArgs e)
        {
            updateProp();
            updateNames();
        }
        private void InputYPosToSlider(object sender, EventArgs e)
        {
            updateProp();
            updateNames();
        }
        private void InputZPosToSlider(object sender, EventArgs e)
        {
            updateProp();
            updateNames();
        }
        private void InputXRotSlider(object sender, EventArgs e)
        {
            updateProp();
            xRotSlider.Title = "X Rotation: " + xRotSlider.Value;
        }
        private void InputYRotSlider(object sender, EventArgs e)
        {
            updateProp();
            yRotSlider.Title = "Y Rotation: " + yRotSlider.Value;
        }
        private void InputZRotSlider(object sender, EventArgs e)
        {
            updateProp();
            zRotSlider.Title = "Z Rotation: " + zRotSlider.Value;
        }
        private void InputXRotTeSlider(object sender, EventArgs e)
        {
            updateProp();
            xRotSliderTe.Title = "X Rotation Tenth: " + xRotSliderTe.Value;
        }
        private void InputYRotTeSlider(object sender, EventArgs e)
        {
            updateProp();
            yRotSliderTe.Title = "Y Rotation Tenth: " + yRotSliderTe.Value;
        }
        private void InputZRotTeSlider(object sender, EventArgs e)
        {
            updateProp();
            zRotSliderTe.Title = "Z Rotation Tenth: " + zRotSliderTe.Value;
        }
        private void InputXRotHuSlider(object sender, EventArgs e)
        {
            updateProp();
            xRotSliderHu.Title = "X Rotation Hundredth: " + xRotSliderHu.Value;
        }
        private void InputYRotHuSlider(object sender, EventArgs e)
        {
            updateProp();
            yRotSliderHu.Title = "Y Rotation Hundredth: " + yRotSliderHu.Value;
        }
        private void InputZRotHuSlider(object sender, EventArgs e)
        {
            updateProp();
            zRotSliderHu.Title = "Z Rotation Hundredth: " + zRotSliderHu.Value;
        }
        private void InputXRotToSlider(object sender, EventArgs e)
        {
            updateProp();
            xRotSliderTo.Title = "X Rotation Thousandth: " + xRotSliderTo.Value;
        }
        private void InputYRotToSlider(object sender, EventArgs e)
        {
            updateProp();
            yRotSliderTo.Title = "Y Rotation Thousandth: " + yRotSliderTo.Value;
        }
        private void InputZRotToSlider(object sender, EventArgs e)
        {
            updateProp();
            zRotSliderTo.Title = "Z Rotation Thousandth: " + zRotSliderTo.Value;
        }
        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Subtract)
            {
                if (!mainMenu.Visible && !adjustmentMenu.Visible)
                {
                    mainMenu.Visible = true;
                }
            }
            if (e.KeyCode == Keys.NumPad0)
            {
                if (clipboardText != null)
                {
                    Clipboard.setText(clipboardText);
                    Notification.Show("~g~Position and Rotation copied to clipboard!");
                } else
                {
                    Notification.Show("~r~Clipboard text does not exist");
                }
            }
        }
        void updateNames()
        {
            if (xInvert)
            {
                xPosSlider.Title = "X Position: " + -xPosSlider.Value;
                xPosSliderTe.Title = "X Position Tenth: " + -xPosSliderTe.Value;
                xPosSliderHu.Title = "X Position Hundredth: " + -xPosSliderHu.Value;
                xPosSliderTo.Title = "X Position Thousandth: " + -xPosSliderTo.Value;
            } else
            {
                xPosSlider.Title = "X Position: " + xPosSlider.Value;
                xPosSliderTe.Title = "X Position Tenth: " + xPosSliderTe.Value;
                xPosSliderHu.Title = "X Position Hundredth: " + xPosSliderHu.Value;
                xPosSliderTo.Title = "X Position Thousandth: " + xPosSliderTo.Value;
            }
            if (yInvert)
            {
                yPosSlider.Title = "Y Position: " + -yPosSlider.Value;
                yPosSliderTe.Title = "Y Position Tenth: " + -yPosSliderTe.Value;
                yPosSliderHu.Title = "Y Position Hundredth: " + -yPosSliderHu.Value;
                yPosSliderTo.Title = "Y Position Thousandth: " + -yPosSliderTo.Value;
            } else
            {
                yPosSlider.Title = "Y Position: " + yPosSlider.Value;
                yPosSliderTe.Title = "Y Position Tenth: " + yPosSliderTe.Value;
                yPosSliderHu.Title = "Y Position Hundredth: " + yPosSliderHu.Value;
                yPosSliderTo.Title = "Y Position Thousandth: " + yPosSliderTo.Value;
            }
            if (zInvert)
            {
                zPosSlider.Title = "Z Position: " + -zPosSlider.Value;
                zPosSliderTe.Title = "Z Position Tenth: " + -zPosSliderTe.Value;
                zPosSliderHu.Title = "Z Position Hundredth: " + -zPosSliderHu.Value;
                zPosSliderTo.Title = "Z Position Thousandth: " + -zPosSliderTo.Value;
            } else
            {
                zPosSlider.Title = "Z Position: " + zPosSlider.Value;
                zPosSliderTe.Title = "Z Position Tenth: " + zPosSliderTe.Value;
                zPosSliderHu.Title = "Z Position Hundredth: " + zPosSliderHu.Value;
                zPosSliderTo.Title = "Z Position Thousandth: " + zPosSliderTo.Value;
            }
        }
        void updateProp()
        {
            propXpos = xPosSlider.Value + (xPosSliderTe.Value * 0.1f) + (xPosSliderHu.Value * 0.01f) + (xPosSliderTo.Value * 0.001f);
            propYpos = yPosSlider.Value + (yPosSliderTe.Value * 0.1f) + (yPosSliderHu.Value * 0.01f) + (yPosSliderTo.Value * 0.001f);
            propZpos = zPosSlider.Value + (zPosSliderTe.Value * 0.1f) + (zPosSliderHu.Value * 0.01f) + (zPosSliderTo.Value * 0.001f);
            propXrot = xRotSlider.Value + (xRotSliderTe.Value * 0.1f) + (xRotSliderHu.Value * 0.01f) + (xRotSliderTo.Value * 0.001f);
            propYrot = yRotSlider.Value + (yRotSliderTe.Value * 0.1f) + (yRotSliderHu.Value * 0.01f) + (yRotSliderTo.Value * 0.001f);
            propZrot = zRotSlider.Value + (zRotSliderTe.Value * 0.1f) + (zRotSliderHu.Value * 0.01f) + (zRotSliderTo.Value * 0.001f);
            if (xInvert)
            {
                propXpos = -propXpos;
            }
            if (yInvert)
            {
                propYpos = -propYpos;
            }
            if (zInvert)
            {
                propZpos = -propZpos;
            }
            clipboardText = "X Pos: " + propXpos + ", Prop Y Pos: " + propYpos + ", Prop Z Pos: " + propZpos + ", Prop X Rot: " + propXrot + ", Prop Y Rot: " + propYrot + ", Prop Z Rot: " + propZrot;
            if (propLoaded)
            {
                Function.Call(Hash.ATTACH_ENTITY_TO_ENTITY, prop, Game.Player.Character, boneIndex, propXpos, propYpos, propZpos, propXrot, propYrot, propZrot, 0, softPinning, collision, isPed, vertex, fixedRot);
            }
        }
    }
}