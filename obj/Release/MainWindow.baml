   M S B A M L   `   `   `  ���� I  EAddonsUploader, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:AddonsUploaderAddonsUploader      AddonsUploader.MainWindow   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     .localclr-namespace:AddonsUploader       � Title$)  #World of Warcraft InterfaceUploader��$	��450��$	��800��.��� .+��� -    ��Name$ 
PathButton��$��Choose Directory��$
��Left=�$��
10,377,0,0q�$	��Top=�$	��111�� J� RenderTransformOrigin$ -0.295,1.577&�$��23���� -    ��Name$ 	PathLabel��$
��Left=�$��23�� �� TextWrapping$
 Wrap=�$�� ��$	��Top=�$	��349��$ 1.507,17.162&�$��126,377,0,0q� �� 
IsReadOnly . �� -    ��Name$ InterfaceCheck��$��	Interface��$
��Left=�$��506,374,0,0q�$	��Top=��� -   $ WTFCheck��$	��WTF��$
��Left=�$��506,394,0,0q�$	��Top=�$ 0.591,-0.333&��� -   $ ZipInterface��$��Zip Interface��$
��Left=�$��663,377,0,0q�$	��Top=�$	��119��$��32���� -   $ LoginButton��$��Google Drive Login��$
��Left=�$��
663,10,0,0q�$	��Top=�$	��119��$��41���� -   $ UIUpload��$��Upload��$
��Left=�$��663,330,0,0q�$	��Top=�$	��119��$��32���� -   $ InterfaceList��$��Load Interface List��$
��Left=�$��663,279,0,0q�$	��Top=�$	��119��$��32��&   System.Windows.Controls.DataGrid  -	     Name$ InterfaceData��$
��Left=�$	��327��$��	10,10,0,0q�$	��Top=�$	��640�� �� SelectedIndex$ 0��	   CanUserAddRows	 .  
   
IsReadOnly
 .    SelectionMode$ Single=�$   HorizontalScrollBarVisibility$ Hidden=�   AutoGenerateColumns .     CanUserResizeColumns .     Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$
 Name��   Width5  /System.Windows.Controls.DataGridLengthConverter$	 160 1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *Name+  $ ID��$	 240  �� *ID+  $
 Size��$ 80  �� *Size+4  .System.Windows.Controls.DataGridTemplateColumn     CellTemplate �� .R��� $ RestoreButton��-
   $��Restore Interface��$	��150������  �� Path$ ID��� -    ��Name$ OnlineStatus��$��Offline��$
��Left=�$	��Top=�$��
672,51,0,0q�$��96��$ 0.252,0.497&�$��Center=�� -    �Name$ ProgressBar��$
��Left=�$��20��$��
10,342,0,0q�$	��Top=�$	��640�� � Minimum$ 0Q� � Maximum$	 100Q�