   M S B A M L   `   `   `  ����I  EAzFileTransfer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:AzFileTransferAzFileTransfer  5   	       AzFileTransfer.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   .localclr-namespace:AzFileTransfer     5   	     � Title$  
MainWindow��5   	   $	��450��6   $	��800��6)   .��5	      �  � RowDefinitions 5
   
   �� 5      $��50 �6   �� 5       � ColumnDefinitions 5   
   �� 5      �� 5      .+�5   
   �� $��0��6   $��0��6   $��服务端文件列表��6-   $��Center=�6?   $��Center=�6Z   �� 5   
   $��1��6   $��0��6$   $
��Auto=�64   $
��Auto=�6Y   .��5      &   System.Windows.Controls.DataGrid  -   5        $��ServerFileList��5         SelectionMode$ Single=�6.      AutoGenerateColumns .     Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ 	文件名��6)   1  +System.Windows.Controls.DataGridBoundColumn   Binding 66   �� *Name+  5      $ 大小��6)    65   �� *Length+  5      $ 修改日期��6)    67   �� *LastWriteTime+5      5      �� 5   
   $��0��6   $��1��6"    �� Orientation$ 
Horizontal=�62   $��5q�6K   .+�5      �� -   5        $��选择上传��5      $��Center=�6%   6@   �� 5      -   5        $��确认上传��5      $��Center=�6%   6@   �� 5       -   5        $��下载��5       $��Center=�6#   6>   5!      �� 5"   
   $��1��6   $��1��6"   $��5q�62   .+�5#      �� $ 
Horizontal=�6   .+�5$      �� $��文件路径 : ��6   6-   �� 5%      -   5        $��tbFilePathInfo��5%      /-C:\Users\jovy-\source\FSFileDownLoad\test.rar6a   5&      �� 5'      $ 
Horizontal=�6   $��	0,10,0,10q�62   .+�5(      �� $��状态 :       ��6   61   �� 5)      -   5        $��tbFileStates��5)      
����  �6D   5*      �� 5+      $ 
Horizontal=�6   $	��0,2q�62   .+�5,      �� $��文件大小 :         ��6   65   �� 5-      -   5        $��
tbFileSize��5-      60   5.      �� 5/      $ 
Horizontal=�6   $	��0,2q�62   .+�50      �� $��已经完成 :         ��6   65   �� 51      -   5        $��	tbCurrent��51      6/   52      �� 53      $ 
Horizontal=�6   $	��0,2q�62   .+�54      �� $��传输速度 :         ��6   65   �� 55      -	   5        $��tbSpeed��55      6-   56      �� 57      $ 
Horizontal=�6   $	��0,2q�62   .+�58      �� $��剩余时间 :         ��6   65   �� 59      -
   5        $��tbTime��59      6,   5:      �� 5;      $ 
Horizontal=�6   $	��0,2q�62   .+�5<      �� $��能否续传 :         ��6   65   �� 5=      -   5        $��tbCanGO��5=      6-   5>      � 5?      -   5        $��ProBar��5?      	 � Value$	 0Q�6(   
 � Maximum$	
 100Q�62   $��15��6@   $	��0,5q�6L   �� 5@      .��5A      �� -   5        $��tbMesg��5A      $	��200��6*   69   5B      5C      5D      5E      