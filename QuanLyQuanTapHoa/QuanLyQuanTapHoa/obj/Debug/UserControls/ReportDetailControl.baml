   M S B A M L   `   `   `  ����K  GQuanLyQuanTapHoa, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullM+clr-namespace:QuanLyQuanTapHoa.UserControlsQuanLyQuanTapHoa.UserControls  5      7    1QuanLyQuanTapHoa.UserControls.ReportDetailControl   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      =local+clr-namespace:QuanLyQuanTapHoa.UserControls     5      _ [MaterialDesignThemes.Wpf, Version=4.2.1.2, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      -   5          >�Name$  ReprotDetailCT��5      $��25��5
      $	��750��5      .��5      �  � ColumnDefinitions 5   
   �� 5      $��0.45* �6   6/   �� 5      $��1.25* �6   6/   �� 5      $��0.65* �6   6/   �� 5      $
��0.5* �6   6.   �� 5      6!   �� 5      6!   �� 5      6!    � 	Resources 5   
    �� 
TargetType �� Property �� Value��%(  '��       '�� �     �� 5      " ��6   .�5      ��  � ��
 �����6?   �� 5       � ��$ 1q�64   6@   �� 5       � �� �#ccc6?   5      �� 5      " ��6   .�5      ��  � ��$	 0 2q�6+   69   �� 5       � ��$ Center=�68   6I   5      5       .+�5!   
   � $��0��6   .+�5"      �� ._�5#      �� -   5         ��Name$	 STT��5#      6+   5$      5%      � 5&   
   $��1��6   .+�5'      �� ._�5(      �� -   5        $ txbProductName��5(      $
��Wrap=�64   6J   5)      5*      � 5+   
   $��2��6   .+�5,      �� ._�5-      �� -   5        $ txtUnit��5-      6/   5.      5/      � 50   
   $��3��6   .+�51      �� ._�52      �� -   5        $ txbQuantity��52      63   53      54      � 55   
   $��4��6   .+�56      �� ._�57      �� -   5        $ 
txbRevenue��57      ��60   �� *LoaiSanPham.TenLoai+6W   58      59      � 5:   
   $��5��6   .+�5;      �� ._�5<      �� -   5        $ txbPrice��5<      ��6.   �� *GiaBan+6H   5=      5>      � 5?   
   $��6��6   .+�5@      �� ._�5A      �� -   5        $ txbProfitLoss��5A      ��63   �� *GiaBan+6M   5B      5C      5D      5E      