   M S B A M L   `   `   `  ����K  GQuanLyQuanTapHoa, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:QuanLyQuanTapHoaQuanLyQuanTapHoa  5   	   (    "QuanLyQuanTapHoa.EditProductWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0localclr-namespace:QuanLyQuanTapHoa     5   	   c _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5   	   _ [MaterialDesignThemes.Wpf, Version=4.2.1.2, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   -   5        $��EditProductWD��5	   	      	SettingVM  � DataContext#  [  5
   	    � WindowStartupLocation$ CenterScreen=�5   	     MaterialDesignBody#���  5   	   $��Regular"�5   	   $��13(�5   	   &   System.Windows.Media.TextOptions   TextFormattingMode$ Ideal=�5   	      TextRenderingMode$
 Auto=�5   	     MaterialDesignPaper#���  5   	     MaterialDesignFont#���  5   	    � Title$ EditProductWindow��5   	   $	��280��6#   $	��300��60   .��5      �  � RowDefinitions 5   
   �� 5      $
��auto �6   6,   �� 5      $
��auto �6   6,   �� 5      $
��auto �6   6,   �� 5      $
��auto �6   6,   �� 5      $
��auto �6   6,   �� 5      $
��auto �6   6,   .+�5   
   � $��0��6   .+�5      �� $��Sửa sản phẩm��6   $��20(�6,   $��Center=�6:   6Y   5       � 5!   
   $��1��6   $
��16 0q�6   .+�5"      �� -   5         ��Name$ id��5"      
�������69   �� 5#      -   5         ��Name$ txbProductName��5#   (   {�.  )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ Tên sản phẩm��6@    % !MaterialDesignFloatingHintTextBox#��[ 6n   5$      � 5%   
   $��2��6   $
��16 0q�6   	 � ColumnDefinitions	 5&      �� 5'      6%   �� 5(      6%   .+�5*      � $��0��6   $��0 0 10 0q�6#   .+�5+      �� -   5        $ txbSoLuongBayBan��5+      $ Số lượng bày bán��64   #��[ 6f   .��6�   .  (System.Windows.Interactivity.Interaction
   Triggers
 5,      /  )System.Windows.Interactivity.EventTrigger  5-         	EventName$ TextChanged��6)   .  (System.Windows.Interactivity.TriggerBase   Actions. 5.      6  0System.Windows.Interactivity.InvokeCommandAction     Command 64   �� *BringProductBackToWarehouse+   CommandParameter 6e   ��  �� ElementName$ EditProductWD��5/      .��50   .   51      52      � 53      $��1��6   $��10 0 0 0q�6#   .+�54      �� -   5        $ txbThemTuKho��54      $ Thêm từ kho��60   #��[ 6]   .��6�   
 55        56      $ TextChanged��6)   . 57         64   �� *AddProductFromWarehouse+ 6a   �� $ EditProductWD��58      .��59   .   5:      5;      5<      � 5=   
   $��3��6   $
��16 0q�6   	 5>      �� 5?      �� 5@      6%   .+�5B      � $��0��6   .+�5C      �� -   5        $ txbPrice��5C      $��0 0 5 0q�6,   $ 	Giá bán��6=   #��[ 6f   5D      � 5E      $��1��6   .+�5F      �� $	��VND��6   $��0 9 0 0q�6'   $��Center=�68   6U   5G      5H      � 5I   
   $��4��6   $
��16 0q�6   	 5J      �� 5K      6%   �� 5L      6%   .+�5N      � $��0��6   .+�5O      �� -   5        $��cbUnit��5O   -   {�.  $ Đơn vị tính��6;   $��72��6i   $��0 0 10 0q�6w    �� DisplayMemberPath$ TenDonViTinh��5P   >    & "MaterialDesignFloatingHintComboBox#��[ 5Q      ��5P      �� *
UnitList+��5R      �� 5S      .R�5T      $� 5U      5W      5X      � 5Y      $��1��6   .+�5Z      �� -   5        $��
cbCategory��5Z   -   {�.  $ Loại��6?   $��72��6f   $��10 0 0 0q�6t   $ TenLoai��5[   B   #��[ 5\      ��5[      �� *CategoryList+��5]      �� 5^      .R�5_      $� 5`      5b      5c      5d      � 5e   
   $��5��6   $��Center=�6    � 	Resources 5f       �� 
TargetType �� Property �� Value �� 
TargetType ��Name �� CornerRadius �� Padding � Cursor �� Triggers P� Property P� Value �� 
TargetName��%�  '��       �� 5g      " ��6   .�5h      ��  � �� 5i      �� 5j      " ��6.   .R�5k   "   �� $ ButtonBorder��6)   ���#F73B4F$ 10��6T   $��20q�6f   $
 20 5q�6r   ._�5l   &   �� $
 Hand��67   5m   #    5n   "   P� 5o   &    �@J�IsMouseOver . . �5p   *   �� $ ButtonBorder��6W    � �� �#ff98005q   '   5s      5u      5v      5w      .+�5x      �� $��Xác nhận��6s   
�������$��14(�6�   ��6   �� *EditProductCommand+��6<   �� $ EditProductWD��6�   5y      5z      5{      