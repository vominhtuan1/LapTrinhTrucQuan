   M S B A M L   `   `   `  ����K  GQuanLyQuanTapHoa, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:QuanLyQuanTapHoaQuanLyQuanTapHoa  5   	   '    !QuanLyQuanTapHoa.AddProductWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0localclr-namespace:QuanLyQuanTapHoa     5   	   c _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5   	   _ [MaterialDesignThemes.Wpf, Version=4.2.1.2, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   -   5        $��AddProductWD��5	   	      	SettingVM  � DataContext#  [  5
   	    � WindowStartupLocation$ CenterScreen=�5   	     MaterialDesignBody#���  5   	   $��Regular"�5   	   $��13(�5   	   &   System.Windows.Media.TextOptions   TextFormattingMode$ Ideal=�5   	      TextRenderingMode$
 Auto=�5   	     MaterialDesignPaper#���  5   	     MaterialDesignFont#���  5   	    � Title$' !Thêm sản phẩm mới từ kho��5   	   $	��430��6*   $	��300��67   .��5      �  � RowDefinitions 5   
   �� 5      $
��auto �6   6,   �� 5      $	��100 �6   6,   �� 5      $
��auto �6   6,   �� 5      $
��auto �6   6,   �� 5      $
��auto �6   6,   �� 5      $
��auto �6   6,   �� 5      $
��auto �6   6,   �� 5      $
��auto �6   6,   �� 5      $
��auto �6   6,   .+�5!   
   � $��0��6   .+�5"      �� $'��!Thêm sản phẩm mới từ kho��6   $��19(�68   $��Center=�6F   6e   5#      � 5$   
   $��1��6   .+�5%      �� -   5         ��Name$ 
imgProduct��5%      $��/Images/1.jpg��6(   6A   5&      � 5'   
   $��2��6   $
��16 0q�6   .+�5(      �� -   5         ��Name$ 	cbProduct��5(      )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ Tên sản phẩm��6*   $��72��6X   	 �� DisplayMemberPath$	 
TenSanPham��5)   @    & "MaterialDesignFloatingHintComboBox#��[ 5*      ��5)      �� *SanPhamKhoList+��5+      �� 5,      .R�5-      $� 5.      .  (System.Windows.Interactivity.Interaction
   Triggers
 50      /  )System.Windows.Interactivity.EventTrigger  51         	EventName$ SelectionChanged��6%   .  (System.Windows.Interactivity.TriggerBase   Actions. 52      6  0System.Windows.Interactivity.InvokeCommandAction     Command 60   �� *SelectionChanged+   CommandParameter 6V   ��  �� ElementName$ AddProductWD��53      55      56      � 57   
   $��3��6   $
��16 0q�6   .+�58      �� -   5         ��Name$ txbSLBayBan��58      $ Số lượng bày bán��6+    % !MaterialDesignFloatingHintTextBox#��[ 6]   59      � 5:   
   $��4��6   $
��16 0q�6    � ColumnDefinitions 5;      �� 5<      $��7* �6#   �� 5=      $	��13* �6#   .+�5?      �� -   5        $ txbSLTrongKho��5?      $ Số lượng trong kho��6-   #��[ 6a   {�.  $��2��6�   5@      � 5A   
   $��5��6   $
��16 0q�6   .+�5B      �� -   5        $ 
txbGiaNhap��5B      $ Giá nhập��6*   #��[ 6T   {�.  6�   5C      � 5D   
   $��6��6   $
��16 0q�6    5E      �� 5F      6%   �� 5G      6%   .+�5I      � $��0��6   .+�5J      �� -   5        $ 	txbGiaBan��5J      $ 	Giá bán��6-   #��[ 6V   5K      � 5L      $��1��6   .+�5M      �� $	��VND��6   $��Bottom=�6'   $��5 0 0 5q�6B   6U   5N      5O      � 5P   
   $��7��6   $
��16 0q�6   .+�5Q      ��  �� Orientation$ 
Horizontal=�6   $��Center=�62   .+�5R      �� -   5        $ cbDonViTinh��5R      $ Đơn vị tính��60   $��72��6]   $	 TenDonViTinh��5S   :   {�.  #��[ 5T      ��5S      �� *
UnitList+��5U      �� 5V      .R�5W      $� 5X      5Z      �� 5[      -	   5        $ cbLoai��5[      $ Loại��6+   $��72��6Q   $��50 0 0 0q�6_   $	 TenLoai��5\   >   {�.  #��[ 5]      ��5\      �� *CategoryList+��5^      �� 5_      .R�5`      $� 5a      5c      5d      5e      � 5f   
   $��8��6   $��Center=�6    � 	Resources 5g       �� 
TargetType �� Property �� Value �� 
TargetType ��Name �� CornerRadius �� Padding � Cursor �� Triggers P� Property P� Value �� 
TargetName��%�  '��       �� 5h      " ��6   .�5i      ��  � �� 5j      �� 5k      " ��6.   .R�5l   "   �� $ ButtonBorder��6)   ���#F73B4F$ 10��6T   $��20q�6f   $
 20 5q�6r   ._�5m   &   �� $
 Hand��67   5n   #    5o   "   P� 5p   &    �@J�IsMouseOver . . �5q   *   �� $ ButtonBorder��6W    � �� �#ff98005r   '   5t      5v      5w      5x      .+�5y      �� $��Xác nhận��6}   
�������$��14(�6�   ��6   �� *SubmitAddProductFromWarehouse+��6G   �� $ AddProductWD��6�   5z      5{      5|      