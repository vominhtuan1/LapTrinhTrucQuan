   M S B A M L   `   `   `  ����K  GQuanLyQuanTapHoa, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullM+clr-namespace:QuanLyQuanTapHoa.UserControlsQuanLyQuanTapHoa.UserControls  5      7    1QuanLyQuanTapHoa.UserControls.ImportProductDetail   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      =local+clr-namespace:QuanLyQuanTapHoa.UserControls     5      _ [MaterialDesignThemes.Wpf, Version=4.2.1.2, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      -   5          >�Name$  ImportDetailCT��5   X      WarehouseVM � DataContext# [  5   H   $��25��5   3   .��5L      �  � ColumnDefinitions 5M   
   �� 5N      $��0.75* �6   6/   �� 5O      $��3* �6   6,   �� 5P      $
��1.5* �6   6.   �� 5Q      6!   �� 5R      6!   �� 5S      $
��1.5* �6   6.   �� 5T      6!    � 	Resources 5V   
    �� 
TargetType �� Property �� Value��%(  '��       '�� �     �� 5W      " ��6   .�5X      ��  � ��
 �����6?   �� 5Y       � ��$ 1q�64   6@   �� 5Z       � �� �#ccc6?   5[      �� 5\      " ��6   .�5]      ��  � ��$	 0 2q�6+   69   �� 5^       � ��$ Center=�68   6I   5_      5`      .+�5a   
   � $��0��6   .+�5b      �� ._�5c      �� -   5         ��Name$ txbID��5c      $��id��6+   67   5d      5e      � 5f   
   $��1��6   .+�5g      �� ._�5h      �� -   5        $ txbProductName��5h      $��ten san pham��64   6J   5i      5j      � 5k   
   $��2��6   .+�5l      �� ._�5m      �� -   5        $ txbQuantity��5m      $��so luong��62   6D   5n      5o      � 5p   
   $��3��6   .+�5q      �� ._�5r      �� -   5        $ txbUnit��5r      $��don vi��6-   6=   5s      5t      � 5u   
   $��4��6   .+�5v      �� ._�5w      �� -   5        $ txbCategory��5w      $
��loai��61   6?   5x      5y      � 5z   
   $��5��6   .+�5{      �� ._�5|      �� -   5        $ txbPrice��5|      $��gia nhap��6/   6A   5}      5~      � 5   
   $��6��6   .+�5�      �� ._�5�      ��  �� Orientation$ 
Horizontal=�6   $��Center=�66   .+�5�      �� 
������ $��25��66   $��0q�6B   $��0q�6V   ��5�      �� 	 �� RelativeSource	 �� *FindAncestor+
 �� AncestorType"
 �� �� Path$&  DataContext.DeleteProductCommand���5�      ��  �� ElementName$ ImportDetailCT��.��5�      '  !MaterialDesignThemes.Wpf.PackIcon  $��25��62   $��25��6>   
���  ��   Kind$ Delete=�6Z   5�      5�      5�      5�      5�      5�      