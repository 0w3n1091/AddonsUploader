   M S B A M L   `   `   `  ����I  EAddonsUploader, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:AddonsUploaderAddonsUploader  5   	       AddonsUploader.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   .localclr-namespace:AddonsUploader     5   	     � Title$)  #World of Warcraft InterfaceUploader��5   	   $	��450��65   $	��800��6B   .��5	      � .+�5
   
   �� -   5         ��Name$ 
PathButton��5
      $��Choose Directory��6%   $
��Left=�6@   $��
10,377,0,0q�6[   $	��Top=�6o   $	��111��6�    J� RenderTransformOrigin$ -0.295,1.577&�6�   $��23��6�   �� 5   
   -   5         ��Name$ 	PathLabel��5      $
��Left=�6%   $��23��6@    �� TextWrapping$
 Wrap=�6L   $�� ��6`   $	��Top=�6h   $	��349��6�   $ 1.507,17.162&�6�   $��126,377,0,0q�6�    �� 
IsReadOnly . 6�   �� 5   
   -   5         ��Name$ InterfaceCheck��5      $��	Interface��6+   $
��Left=�6?   $��506,374,0,0q�6Z   $	��Top=�6o   �� 5   
   -   5        $ WTFCheck��5      $	��WTF��6%   $
��Left=�63   $��506,394,0,0q�6N   $	��Top=�6c   $ 0.591,-0.333&�6{   �� 5   
   -   5        $ ZipInterface��5      $��Zip Interface��6'   $
��Left=�6?   $��663,377,0,0q�6Z   $	��Top=�6o   $	��119��6�   $��32��6�   6�   �� 5   
   -   5        $ LoginButton��5      $��Google Drive Login��6'   $
��Left=�6D   $��
663,10,0,0q�6_   $	��Top=�6s   $	��119��6�   $��41��6�   6�   �� 5   
   -   5        $ UIUpload��5      $��Upload��6$   $
��Left=�65   $��663,330,0,0q�6P   $	��Top=�6e   $	��119��6}   $��32��6�   6�   �� 5   
   -   5        $ InterfaceList��5      $��Load Interface List��6*   $
��Left=�6H   $��663,279,0,0q�6c   $	��Top=�6x   $	��119��6�   $��32��6�   6�   &   System.Windows.Controls.DataGrid  5   
   -	   5          Name$ InterfaceData��5      $
��Left=�6,   $	��327��6G   $��	10,10,0,0q�6T   $	��Top=�6g   $	��640��6    �� SelectedIndex$ 0��6�   	   CanUserAddRows	 .  
   
IsReadOnly
 .    SelectionMode$ Single=�6�   $   HorizontalScrollBarVisibility$ Hidden=�6�      AutoGenerateColumns .     CanUserResizeColumns .     Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$
 Name��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$	 160 63   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6@   �� *Name+  5      $ ID��6%   $	 240 61    6=   �� *ID+  5      $
 Size��6%   $ 80 63    6>   �� *Size+4  .System.Windows.Controls.DataGridTemplateColumn  5         CellTemplate 5      �� 5      .R�5      �� $ RestoreButton��6%   -
   5        $��Restore Interface��5   X   $	��150��6t   ��6�   ��  �� Path$ ID�5      5      5      5       �� 5!   
   -   5         ��Name$ OnlineStatus��5!      $��Offline��6&   $
��Left=�68   $	��Top=�6S   $��
672,51,0,0q�6k   $��96��6   $ 0.252,0.497&�6�   $��Center=�6�   � 5"   
   -   5         �Name$ ProgressBar��5"      $
��Left=�6+   $��20��6F   $��
10,342,0,0q�6R   $	��Top=�6f   $	��640��6~    � Minimum$ 0Q�6�    � Maximum$	 100Q�6�   5#      5$      