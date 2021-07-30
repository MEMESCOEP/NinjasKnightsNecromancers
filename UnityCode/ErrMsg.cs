 public MessageBoxButtons buttons = MessageBoxButtons.YesNo;
 public MessageBoxIcon icon = MessageBoxIcon.None;
 public MessageBoxDefaultButton defaultButton = MessageBoxDefaultButton.Button1;
 void Start()
 {
     MessageBox.Show(Callback, "Hello World!", "Hello", buttons, icon, defaultButton);
 }
 void Callback(DialogResult result)
 {
     Debug.Log(result.ToString());
 }
