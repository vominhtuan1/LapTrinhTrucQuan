   M S B A M L   `   `   `  ����K  GQuanLyQuanTapHoa, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullM+clr-namespace:QuanLyQuanTapHoa.UserControlsQuanLyQuanTapHoa.UserControls  5      6    0QuanLyQuanTapHoa.UserControls.StaffDetailControl   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      =local+clr-namespace:QuanLyQuanTapHoa.UserControls     5      _ [MaterialDesignThemes.Wpf, Version=4.2.1.2, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  6H   -   5          >�Name$  StaffDetailCT��5
         StaffVM � DataContext# [  5      .��5      �  � ColumnDefinitions 5   
   �� 5      $
��0.5* �6   6.   �� 5      $
��1.5* �6   6.   �� 5      $
��0.5* �6   6.   �� 5      $��0.75* �6   6/   �� 5      6!   �� 5      $��1* �6   6,   �� 5      $��1* �6   6,   �� 5      $��1* �6   6,   �� 5      6!    � 	Resources 5   
    �� 
TargetType �� Property �� Value��%(  '��       '�� �     �� 5      " ��6   .�5      ��  � ��
 �����6?   �� 5       � ��$ 1q�64   6@   �� 5       � �� �#ccc6?   5      �� 5      " ��6   .�5      ��  � ��$	 0 2q�6+   69   �� 5       � ��$ Center=�68   6I   5       5!      .+�5"   
   � $��0��6   .+�5#      �� ._�5$      �� -   5         ��Name$ txbID��5$      6-   5%      5&      � 5'   
   $��1��6   .+�5(      �� ._�5)      �� -   5        $ txbStaffName��5)      65   5*      5+      � 5,   
   $��2��6   .+�5-      �� ._�5.      �� -   5        $ txbStaffSex��5.      64   5/      50      � 51   
   $��3��6   .+�52      �� ._�53      �� -   5        $ 
txbStaffBY��53      62   54      55      � 56   
   $��4��6   .+�57      �� ._�58      �� -   5        $ txbStaffPhone��58      65   59      5:      � 5;   
   $��5��6   .+�5<      �� ._�5=      �� -   5        $ txbStaffAccountName��5=      6;   5>      5?      � 5@   
   $��6��6   .+�5A      �� ._�5B      �� -   5        $ txbStaffRole��5B      65   5C      5D      � 5E   
   $��7��6   .+�5F      �� ._�5G      �� -	   5        $ txbStaffSalary��5G      66   5H      5I      � 5J   
   $��8��6   .+�5K      �� ._�5L      ��  �� Orientation$ 
Horizontal=�6   $��Center=�66   .+�5M      �� 
������ $��25��5O   6   $��0q�6B   $��0q�6V   $��0 0 20 0q�6b   ��5M      �� 	 �� RelativeSource	 �� *FindAncestor+
 �� AncestorType"
 �� �� Path$ DataContext.OpenEditStaff���5N      ��  �� ElementName$ StaffDetailCT��.��5P      '  !MaterialDesignThemes.Wpf.PackIcon  $��25��62   $��25��6>   
����  �   Kind$ PencilBoxOutline=�6[   5Q      �� 5R      
������ $��25��66   $��0q�6B   $��0q�6V   ��5S      �� 	 �� *FindAncestor+"
 ��$$ DataContext.DeleteStaffCommand���5T      �� $ StaffDetailCT��.��5U        $��25��62   $��25��6>   
���  ��$ Delete=�6Z   5V      5W      5X      5Y      5Z      5[      