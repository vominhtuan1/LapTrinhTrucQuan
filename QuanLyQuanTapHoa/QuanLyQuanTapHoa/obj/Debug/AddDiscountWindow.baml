   M S B A M L   `   `   `  ����K  GQuanLyQuanTapHoa, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:QuanLyQuanTapHoaQuanLyQuanTapHoa  5   	   (    "QuanLyQuanTapHoa.AddDiscountWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0localclr-namespace:QuanLyQuanTapHoa     5   	   _ [MaterialDesignThemes.Wpf, Version=4.2.1.2, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   -   5        $��AddDiscountWD��5   	      
DiscountVM  � DataContext#  [  5	   	    � WindowStartupLocation$ CenterScreen=�5
   	     MaterialDesignBody#���  5   	   $��Regular"�5   	   $��13(�5   	   &   System.Windows.Media.TextOptions   TextFormattingMode$ Ideal=�5   	      TextRenderingMode$
 Auto=�5   	     MaterialDesignPaper#���  5   	     MaterialDesignFont#���  5   	    � Title$ Thêm mã giảm giá��5   	   $	��320��6"   $	��250��6/   .��5      �  � RowDefinitions 5   
   �� 5      $
��auto �6   6,   �� 5      $
��auto �6   6,   �� 5      $
��auto �6   6,   �� 5      $
��auto �6   6,   �� 5      $
��auto �6   6,   �� 5      $
��auto �6   6,   �� 5      $
��auto �6   6,   .+�5   
   � $��0��6   .+�5       �� $��Thêm Mã Giảm Giá��6   $��18(�60   $��Center=�6>   $	��165��6[   5!      � 5"   
   $��1��6   $
��16 0q�6   $��2��6*   .+�5#      �� -   5         ��Name$ 	tbCoupoun��5#      )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ Mã giảm giá��6)    % !MaterialDesignFloatingHintTextBox#��[ 6V   5$      � 5%   
   $��2��6   $
��16 0q�6   $��2��6*   .+�5&      (  "System.Windows.Controls.DatePicker  -   5          Name$ tbNgayBatDau��5&   H   $ Ngày bắt đầu��6   $
��Left=�6^   $��28��6y   $��0,9,0,0q�6�   $	��Top=�6�   $	��184��6�   5'      � 5(   
   $��3��6   $
��16 0q�6   $��2��6*   .+�5*        -   5        $ tbNgayKetThuc��5*   I   $ Ngày kết thúc��6   $
��Left=�6`   $��28��6{   $��0,9,0,0q�6�   $	��Top=�6�   $	��184��6�   5+      � 5,   
   $��4��6   $
��16 0q�6   $��2��6*   	 � ColumnDefinitions	 5-      �� 5.      6%   �� 5/      6%   .+�51      � $��0��6   $	��150��6#   .+�52      �� -   5        $ tbSoTienGiam��52      $
��Left=�60   $��0 0 10 0q�6K   $	��160��6]   $ Số tiền giảm��6i   #��[ 6�   53      � 54      $��1��6   $��30��6#   .+�55      �� $��Right=�6   $	��VND��68   $��0 0 0 5q�6C   $��Bottom=�6T   6q   56      57      � 58   
   $��5��6   $
��16 0q�6   $��2��6*   	 59      �� 5:      6%   �� 5;      6%   .+�5=      � $��0��6   $	��150��6#   .+�5>      �� -   5        $ tbDonHangTu��5>      $
��Left=�6/   $��	0,0,-10,0q�6J   $	��160��6]   $ Đơn hàng từ��6i   #��[ 6�   5?      � 5@      $��1��6   $��30��6#   .+�5A      �� $��Right=�6   $	��VND��68   $��0 0 0 5q�6C   $��Bottom=�6T   6q   5B      5C      � 5D   
   $��6��6   $��Center=�6   $��2��69   $
��68,0q�6M   
 � 	Resources
 5E       �� 
TargetType �� Property �� Value �� 
TargetType �� CornerRadius �� Padding � Cursor��%  '��       �� 5F      " ��6   .�5G      ��  � �� 5H      �� 5I      " ��6.   .R�5J   "   �� ���#F73B4F$ 10��6>   $
��0 20q�6P   $
 20 2q�6^   ._�5K   &   � .+�5L   *   �� $��5q�6;   $
 Hand��6F   5M   '   5N   #   5O      5Q      5R      5S      	 5T      �� 5U      $
��Auto �6#   �� 5V      $��0* �6#   .+�5X      �� $��Xác nhận��6   
�������$��14(�6;   ��5Y      �� *SubmitAddDiscount+��6;   ��  �� ElementName$ AddDiscountWD��5[      5\      5]      