   M S B A M L   `   `   `  ����K  GQuanLyQuanTapHoa, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:QuanLyQuanTapHoaQuanLyQuanTapHoa  5   	   M+clr-namespace:QuanLyQuanTapHoa.UserControlsQuanLyQuanTapHoa.UserControls  5   	   G(clr-namespace:QuanLyQuanTapHoa.ViewModelQuanLyQuanTapHoa.ViewModel  5   	   !    QuanLyQuanTapHoa.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   c _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5   	   *localclr-namespace:QuanLyQuanTapHoa  5   	   4uc+clr-namespace:QuanLyQuanTapHoa.UserControls  5   	   _ [MaterialDesignThemes.Wpf, Version=4.2.1.2, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   8	viewmodel(clr-namespace:QuanLyQuanTapHoa.ViewModel  5   	   -   5          �Name$  wdMain��5   	    � WindowStartupLocation$ CenterScreen=�5
   	    
  MainVM � DataContext# [  5   	     MaterialDesignBody#���  5   	   $��Regular"�5   	   $��13(�5   	   &   System.Windows.Media.TextOptions   TextFormattingMode$ Ideal=�5   	      TextRenderingMode$
 Auto=�5   	     MaterialDesignPaper#���  5   	     MaterialDesignFont#���  5   	    � Title$! Quản lý quán tạp hóa��5   	   $	��654��6&   $
��1008��63   .��5      � 
������� � ColumnDefinitions 5   
   �� 5      $
��auto �6   6.   �� 5      6!   .+�5   
   � -   5         �Name$ grdMenu��5      $��0��6     � 	Resources 5      	 �� 
TargetType
 �� Property �� Value �� 
TargetType ��Name �� CornerRadius � Cursor��%�   '��       �� 5      "	 ��6   .�5      �� 
 � �� 5       �� 5!      " ��6.   .R�5"   "   �� $ 	btnBorder��6)   $ 20��6<   
�������._�5#   &   �� $��5 0 5 0q�67   $
 Hand��5$   7   5%   #   5&      5(      5)      5*       � RowDefinitions 5+      �� 5,      6"   �� 5-      6"   �� 5.      6"   �� 5/      6"   �� 50      6"   �� 51      6"   �� 52      6"   �� 53      6"   .+�55      �� $��Images/LogoApp.png��6   $��55��60   $��55��6;   �� 56      -   5         ��Name$ btnHome��56      $��1��6&   
 J� Uid$ 1��63   .��57      � $��70��6   $��70��6#   .+�58      �� -   5        $ bdHome��58      ���#fe8f8f$ 20��6B   ._�59      �� $��Center=�6%   $��Center=�6@   .+�5:      '  !MaterialDesignThemes.Wpf.PackIcon  -   5          Name$ icHome��5:   6   $��Center=�6F      Kind$ HomeOutline=�6c   $��30��6v   $��30��6�   
�������6�   �� 5;      -   5         ��Name$ txtHome��5;   (   $
��Home��69   
�������6Z   5<      5=      5>      .  (System.Windows.Interactivity.Interaction   Triggers 5?      /  )System.Windows.Interactivity.EventTrigger  5@         	EventName$ Click��6%   .  (System.Windows.Interactivity.TriggerBase   Actions. 5A      6  0System.Windows.Interactivity.InvokeCommandAction     Command 60   �� *SwitchTabCommand+   CommandParameter 6U   ��  �� ElementName$ wdMain��5B        5C      $ 	MouseMove��6%   . 5D         60   �� *GetUidCommand+ 6R   �� $ btnHome��5E      5G      �� 5H      -   5        $ btnBill��5H      $��2��6&   $ 6��63   .��5I      � $��70��6   $��70��6#   .+�5J      �� -   5        $ bdBill��5J      
������ $ 20��6F   ._�5K      �� $��Center=�6%   $��Center=�6@   .+�5L        -	   5        $ icBill��5L   6   $��Center=�6F   $ Receipt=�6c   $��30��6r   $��30��6}   
���   �6�   �� 5M      -
   5        $ txtBill��5M   (   $��
Hóa đơn��69   
���   �6^   5N      ��5O      �� 5P      "	 ��6$    �� Triggers 5Q   "   P� 5R   &    P� Property �@J�IsMouseOver P� Value . . �5S   *   �� 
 � ��
 �  ��6G   5T   '   5V      5X      5Y       5Z        5[      $ Click��6%   . 5\         60   �� *SwitchTabCommand+ 6U   �� $ wdMain��5]        5^      $ 	MouseMove��6%   . 5_         60   �� *GetUidCommand+ 6R   �� $ btnBill��5`      5b      �� 5c      -   5        $ 
btnSetting��5c      $��3��6)   $ 5��66   .��5d      � $��70��6   $��70��6#   .+�5e      �� -   5        $ 	bdSetting��5e      
�������$ 20��6C   ._�5f      �� $��Center=�6%   $��Center=�6@   .+�5g        -   5        $ 	icSetting��5g   6   $��Center=�6I   $ Settings=�6f   $��30��6v   $��30��6�   
���   �6�   �� 5h      -   5        $ 
txtSetting��5h   (   $��Cài đặt��6<   
���   �6a   5i      5j      5k       5l        5m      $ Click��6%   . 5n         60   �� *SwitchTabCommand+ 6U   �� $ wdMain��5o        5p      $ 	MouseMove��6%   . 5q         60   �� *GetUidCommand+ 6R   �� $ 
btnSetting��5r      5t      �� 5u      -   5        $ btnStaff��5u      $��4��6'   $ 4��64   .��5v      � $��70��6   $��70��6#   .+�5w      �� -   5        $ bdStaff��5w      
�������$ 20��6B   ._�5x      �� $��Center=�6%   $��Center=�6@   .+�5y        -   5        $ icStaff��5y   6   $��Center=�6G   $ AccountMultipleOutline=�6d   $��30��6�   $��30��6�   
���   �6�   �� 5z      -   5        $ txtStaff��5z   (   $��Nhân viên��6:   
���   �6a   5{      5|      5}       5~        5      $ Click��6%   . 5�         60   �� *SwitchTabCommand+ 6U   �� $ wdMain��5�        5�      $ 	MouseMove��6%   . 5�         60   �� *GetUidCommand+ 6R   �� $ btnStaff��5�      5�      �� 5�      -   5        $ 	btnReport��5�      $��5��6(   $ 2��65   .��5�      � $��70��6   $��70��6"   .+�5�      �� -   5        $ bdReport��5�      
�������$ 20��6B   ._�5�      �� $��Center=�6%   $��Center=�6@   .+�5�        -   5        $ icReport��5�   6   $��Center=�6H   $ ChartPie=�6e   $��30��6u   $��30��6�   
���   �6�   �� 5�      -   5        $ 	txtReport��5�   (   $��	Báo cáo��6;   
���   �6`   5�      5�      5�       5�        5�      $ Click��6%   . 5�         60   �� *SwitchTabCommand+ 6U   �� $ wdMain��5�        5�      $ 	MouseMove��6%   . 5�         60   �� *GetUidCommand+ 6R   �� $ 	btnReport��5�      5�      �� 5�      -   5        $ btnWarehouse��5�      $��6��6+   $ 3��68   .��5�      � $��70��6   $��70��6#   .+�5�      �� -   5        $ bdWareHouse��5�      
�������$ 20��6F   ._�5�      �� $��Center=�6%   $��Center=�6@   .+�5�        -   5        $ icWareHouse��5�   6   $��Center=�6K   $ PackageVariantClosed=�6h   $��30��6�   $��30��6�   
���   �6�   �� 5�      -   5        $ txtWareHouse��5�   (   $	��Kho��6>   
���   �$��Center=�6\   6{   5�      5�      5�       5�        5�      $ Click��6%   . 5�         60   �� *SwitchTabCommand+ 6U   �� $ wdMain��5�        5�      $ 	MouseMove��6%   . 5�         60   �� *GetUidCommand+ 6R   �� $ btnWarehouse��5�      5�      �� 5�      -   5        $ btnDiscount��5�      $��7��6*   $ 7��67   .��5�      � $��70��6   $��70��6#   .+�5�      �� -   5        $ 
bdDiscount��5�      
�������$ 20��6D   ._�5�      �� $��Center=�6%   $��Center=�6@   .+�5�        -   5        $ 
icDiscount��5�   6   $��Center=�6J   $ TicketConfirmation=�6g   $��30��6�   $��30��6�   
���   �6�   �� 5�      -   5        $ txtDiscount��5�   (   $��Giảm giá��6=   
���   �6c   5�      5�      5�       5�        5�      $ Click��6%   . 5�         60   �� *SwitchTabCommand+ 6U   �� $ wdMain��5�        5�      $ 	MouseMove��6%   . 5�         60   �� *GetUidCommand+ 6R   �� $ btnDiscount��5�      5�      5�      � 5�   
   $��1��6   .+�5�      /   )QuanLyQuanTapHoa.UserControls.HomeControl  -   5          Name$
 Home��5�      $y�Visible=�6+   6B   4   .QuanLyQuanTapHoa.UserControls.WarehouseControl  5�      -    5           Name$  	Warehouse��5�   "   $y�Hidden=�65   6K   0	   *QuanLyQuanTapHoa.UserControls.StaffControl	  5�      -!   5        ! 	 Name$! Staff��5�      $y�Hidden=�6-   6C   2
   ,QuanLyQuanTapHoa.UserControls.SettingControl
  5�      -"   5        " 
 Name$" Setting��5�       $y�Hidden=�61   6G   /   )QuanLyQuanTapHoa.UserControls.BillControl  5�      -#   5        #  Name$
# Bill��5�      $y�Hidden=�6+   6A   3   -QuanLyQuanTapHoa.UserControls.DiscountControl  5�      -$   5        $  Name$$ Discount��5�   !   $y�Hidden=�63   6I   1   +QuanLyQuanTapHoa.UserControls.ReportControl  5�      -%   5        %  Name$% Report��5�      $y�Hidden=�6/   6E   5�      5�      5�      