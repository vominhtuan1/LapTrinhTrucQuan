   M S B A M L   `   `   `  ����  QuanLyQuanTapHoa2    ,QuanLyQuanTapHoa.UserControls.ProductControl   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      -   5        $��ProducCT��5       
  HomeVM  � DataContext#  [  5      $��10q�5	      $	��170��6   $	��100��6'   .��5
      �  � 	Resources 5   
    �� 
TargetType �� Property �� Value �� 
TargetType �� CornerRadius � Cursor��%  '��       �� 5      " ��6   .�5      ��  � �� 5      �� 5      " ��6*   .R�5      �� ���#F73B4F$ 10��6:   $	��0 5q�6L   ._�5   "   � .+�5   &   �� $��5q�67   $
 Hand��6B   $��
Chọn mua��6P   5   #   5      5      5      5      5      .+�5   
   �� $ 15��6    �� Padding$ 5q�6#   $��1q�6/   ���#89C5C0	 J� Opacity$	 2Q�6Y   
�������._�5      � 
 � RowDefinitions
 5      �� 5      6&   �� 5      6&   �� 5      $
��auto �6$   64   .+�5!      � $��0��6   .+�5"      �� -   5         ��Name$ 
ProductImg��5"      $��/Images/1.jpg��62   5#      � 5$      $��1��6   .+�5%      �� $��Center=�6!   .+�5&      �� -   5         ��Name$ ProductName��5&   $   $��
TenSanPham��69   $��Center=�6K   $��0 0 0 5q�6h   $
��Wrap=�5'   $    �� TextAlignment$ Center=�68   6Q   � 5(      $��Center=�6   .+�5)      ��  �� Orientation$ 
Horizontal=�6)   .+�5*   "   �� -   5        $ ProductPrice��5*   -   $��GiaBan��6C   
���  ��6Q   �� 5+   "   $
�� VND��6-   
���  ��6L   5,      5-      5.      5/      � 50      $��2��6   $��Bottom=�6$   .+�51      �� -   5        $ id��51       $��Id��6,   
������ 6R   �� 52      $��Center=�6   
�������$
 Hand��6N   ��53      �� *AddToCartCommand+��6C   ��  �� ElementName$ ProducCT��6w   54      55      56      57      58      