   M S B A M L   `   `   `  ����K  GQuanLyQuanTapHoa, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullM+clr-namespace:QuanLyQuanTapHoa.UserControlsQuanLyQuanTapHoa.UserControls  5      8    2QuanLyQuanTapHoa.UserControls.ProductDetailControl   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      =local+clr-namespace:QuanLyQuanTapHoa.UserControls     5      _ [MaterialDesignThemes.Wpf, Version=4.2.1.2, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      -   5          >�Name$  ProductDetailCT��5      .��5      �  � ColumnDefinitions 5   
   �� 5      $
��0.5* �6   6.   �� 5      $
��2.5* �6   6.   �� 5      6!   �� 5      6!   �� 5      6!   �� 5      $
��1.5* �6   6.   �� 5      6!    � 	Resources 5   
    �� 
TargetType �� Property �� Value��%(  '��       '�� �     �� 5      " ��6   .�5      ��  � ��
 �����6?   �� 5       � ��$ 1q�64   6@   �� 5       � �� �#ccc6?   5      �� 5      " ��6   .�5      ��  � ��$	 0 2q�6+   69   �� 5       � ��$ Center=�68   6I   5      5      .+�5    
   � $��0��6   .+�5!      �� ._�5"      �� -   5         ��Name$ txbID��5"      ��6+   �� *	MaSanPham+6I   5#      5$      � 5%   
   $��1��6   .+�5&      �� ._�5'      �� -   5        $ txbProductName��5'      ��64   �� *
TenSanPham+6R   5(      5)      � 5*   
   $��2��6   .+�5+      �� ._�5,      �� -   5        $ txbQuantity��5,      ��61   �� *
SLBayBan+6M   5-      5.      � 5/   
   $��3��6   .+�50      �� ._�51      �� -   5        $ txbUnit��51      ��6-   �� *DonViTinh.TenDonViTinh+6W   52      53      � 54   
   $��4��6   .+�55      �� ._�56      �� -   5        $ txbCategory��56      ��61   �� *LoaiSanPham.TenLoai+6X   57      58      � 59   
   $��5��6   .+�5:      �� ._�5;      �� -   5        $ txbPrice��5;      ��6.   �� *GiaBan+6H   5<      5=      � 5>   
   $��6��6   .+�5?      �� ._�5@      ��  �� Orientation$ 
Horizontal=�6   $��Center=�66   .+�5A      �� 
������ $��25��66   $��0q�6B   $��0q�6V   $��0 0 20 0q�6b   ��5B      ��  �� RelativeSource �� *FindAncestor+	 �� AncestorType"	 ��
 �� Path$!
 DataContext.OpenEditProduct���5C      ��  �� ElementName$ ProductDetailCT��.��5D      '  !MaterialDesignThemes.Wpf.PackIcon  $��25��62   $��25��6>   
����  �   Kind$ PencilBoxOutline=�6[   5E      �� 5F      
������ $��25��67   $��0q�6C   $��0q�6W   ��5G      ��  �� *FindAncestor+"	 ��$&
  DataContext.DeleteProductCommand���5H      �� $ ProductDetailCT��.��5I        $��25��62   $��25��6>   
���  ��$ Delete=�6Z   5J      5K      5L      5M      5N      5O      