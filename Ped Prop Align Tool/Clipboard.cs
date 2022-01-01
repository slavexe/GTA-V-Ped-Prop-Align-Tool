namespace Ped_Prop_Align_Tool
{
    public static class Clipboard
    {
        public static void setText(string text)
        {
            Thread thread = new Thread((ThreadStart)(() => System.Windows.Forms.Clipboard.SetText(text)));
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            thread.Join();
        }
    }
}
