   M S B A M L   `   `   `  ����K  GQuanLyQuanTapHoa, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullM+clr-namespace:QuanLyQuanTapHoa.UserControlsQuanLyQuanTapHoa.UserControls  5      4    .QuanLyQuanTapHoa.UserControls.WarehouseControl   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      =local+clr-namespace:QuanLyQuanTapHoa.UserControls     5      c _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5      h dMicrosoft.Expression.Interactions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bei9http://schemas.microsoft.com/expression/2010/interactions  5      _ [MaterialDesignThemes.Wpf, Version=4.2.1.2, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  6S   -   5          >�Name$  ImportCT��5   7      WarehouseVM � DataContext# [  5
      .  (System.Windows.Interactivity.Interaction   Triggers 5      D  >Microsoft.Expression.Interactivity.Core.PropertyChangedTrigger  5   
      Binding 6$   �� *
Visibility+ �� ElementName$ ImportCT��.  (System.Windows.Interactivity.TriggerBase   Actions. 5      6  0System.Windows.Interactivity.InvokeCommandAction     Command 6$   �� *LoadCommand+   CommandParameter 6D   �� $ ImportCT��5      .��5      �  � ColumnDefinitions 5   
   �� 5      $��2* �6   6,   .+�5   
   �� 
�������	 �� CornerRadius$	 	20 0 0 20��6#   6@   � 5   
   $��0��6   
 � RowDefinitions
 5      �� 5      $
��auto �6    60   �� 5      $
��auto �6    60   �� 5      $
��auto �6    60   .+�5      � $��0��6   $	��Top=�6    $��30��68    5      �� 5      6)   .+�5       � $��0��6   $
��Left=�6'   $	��250��6B   .+�5!      ��  �� Orientation$ 
Horizontal=�6!   $
��auto��6:   $
��auto��6G   $��Center=�6U   $��Center=�6p   .+�5"      �� $��24(�6$   $��Light"�62   $��Danh mục ��6F   6Y   �� 5#      $��24(�6$   $
��Bold"�62   $
��auto��6D   $	��Kho��6Q   5$      5%      5&      � 5'      $��1��6   $��Center=�6    $��50��6;    � 	Resources 5(       �� 
TargetType �� Property �� Value �� 
TargetType � Cursor��%  '��       �� 5)      " ��6   .�5*      ��  � �� 5+      �� 5,   "   " ��62   .R�5-   &   �� ���#6F89E9$	 10��6B   $
��Left=�6T   $	��Top=�6o   ._�5.   *   � .+�5/   .   �� $��5q�6?   $
 Hand��6J   50   +   51   '   52   #   54      55      56       57      �� 58      $
��auto �6'   �� 59      $
��auto �6'   �� 5:      $
��auto �6'   �� 5;      $
��auto �6'   .+�5=      �� $��0��6   $��
Nhập kho��6O   $��Center=�6b   
�������$
��45 0q�5>   ,   ��5=   )   �� *OpenImportProduct+��5>   ;   �� $ 
importList���� 5?      $��1��6   $��Xem chi tiết nhập kho��5@      
�������$��Center=�5A      ��5?   )   �� *OpenImportDetailWDCommand+5A      �� 5B      $��2��6   $
��45 0q�6)   $$��Sản phẩm sắp hết hạn��5C      
�������$��Center=�5D      ��5B   7   �� *OpendExpiredProductCommand+5D      � 5E      $��3��6   $��Right=�6'   .+�5F      �� 
�������$	 15��60   $��40 10 100 10q�6B   ._�5G      �� $ 
Horizontal=�6%   $��Center=�6>   $��Center=�6Y   .+�5H      '  !MaterialDesignThemes.Wpf.PackIcon     Kind$ Magnify=�66   $��25��6E   $��30��6Q   ���#747373
������ 6q   �� 5I      -   5         ��Name$ 	txbSearch��5I   &   
������ $��25��6R   $	��150��6^   $��0q�6j   $��0 0 10 0q�6   $
��Left=�5J   *   $��Center=�6L   $��Right=�5K   *   )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ Tìm kiếm��6F   $��14(�6p   .��6~    5L   "   /  )System.Windows.Interactivity.EventTrigger  5M   &      	EventName$ TextChanged��65   . 5N   *      6@   �� *SearchCommand+ 6b   �� $ ImportCT��5O   '   .��5P   :   5Q      5R      5S      5T      5U      � 5V      $��2��6   $��Bottom=�6    $	��450��6;   .+�5W      �� $
��20 0q�6   .+�5X      �  5Y      �� 5Z      $��0.75* �6/   6?   �� 5[      $��3* �6/   6<   �� 5\      $
��1.5* �6/   6>   �� 5]      61   �� 5^      61   �� 5_      $
��1.5* �6/   6>   �� 5`      61    5b      ��%Q  '��       '�� �     �� 5c      " ��6$   .�5d   "   ��  � �� �#6F89E96Q   �� 5e   "    � ��$ 1q�6D   6P   �� 5f   "    � ��
 �����6P   5g      �� 5h      " ��6$   .�5i   "   ��  � ��$	 0 2q�6;   6I   �� 5j   "    � ��$ Center=�6H   6Y   �� 5k   "    � ��
 �����6O   5l      5m      .+�5n      � $��0��6   .+�5o      �� $	 10 0 0 0��6%   ._�5p   "   �� $��Mã số��6,   6;   5q      5r      � 5s      $��1��6   .+�5t      �� ._�5u   "   �� $��Tên sản phẩm��6,   6B   5v      5w      � 5x      $��2��6   .+�5y      �� ._�5z   "   �� $��Số lượng trong kho��6,   6H   5{      5|      � 5}      $��3��6   .+�5~      �� ._�5   "   �� $��
Đơn vị��6,   6<   5�      5�      � 5�      $��4��6   .+�5�      �� ._�5�   "   �� $��Loại��6,   6:   5�      5�      � 5�      $��5��6   .+�5�      �� ._�5�   "   �� $��Giá nhập��6,   6>   5�      5�      � 5�      $��6��6   .+�5�      �� $	 0 10 0 0��6%   ._�5�   "   �� $�� ��6,   66   5�      5�      5�      � 5�      .+�5�      �� $	��430��6'   
�������$
��Auto=�6G   $��2��6j   .��5�      �� -   5         ��Name$ 
importList��5�   +   $y�Hidden=�6?   ��5�   "   �� 5�   &   .R�5�   *   �� 5�   '   5�      5�      � 5�      -   5         �Name$ progressBar��5�   !   $��80��66   $��80��6A    % !MaterialDesignCircularProgressBar#��[ 5�   !    � Value$ 0Q�5�   !    � IsIndeterminate .  J� RenderTransformOrigin$ 0.722,-1.304&�6B   $��400,175,371,175q�6g   5�      5�      5�      5�      5�      5�      