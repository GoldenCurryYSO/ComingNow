   M S B A M L   `   `   `  ����D  @ComingNow, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:ComingNow	ComingNow  5   	   O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5   	       ComingNow.MediaWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:ComingNow     5   	   6sys&clr-namespace:System;assembly=mscorlib     5   	     � Title$  MediaWindow��5	   	   $	��600��6   $	��600��6*   
���   � �� BitmapScalingMode$
 Fant=�5
   	    � 	Resources 5         ScrollBarSize  ScrollBarRepeatButtonSize  ScrollBarMarginSize  ScrollBarColorBrush �� Color  ScrollBarBorderBrush  ScrollBarTrackBrush  ScrollBarHilightBrush  ScrollBarPressedBrush  ScrollBarDisabledBrush 	 ScrollBarRepeatButtonStyle �� 
TargetType �� Property �� Value �� 
TargetType ��Name	 �� CornerRadius
 �� RelativeSource �� Triggers P� Property P� Value �� 
TargetName 
 ScrollBarTrackStyle  ScrollBarThumbStyle  CustomScrollBarStyle �� Triggers u� Left
 u� Top u� Right u� Bottom � RowDefinitions ]�Name ]� DecreaseRepeatButton ]� Thumb ]� IncreaseRepeatButton � ColumnDefinitions  CustomScrollViewerStyle ��Name �� Orientation �� VerticalOffset � Value �� ScrollableHeight � Maximum  �� ViewportHeight! �� ViewportSize*" �� #ComputedVerticalScrollBarVisibility# �� HorizontalOffset$ �� ScrollableWidth% �� ViewportWidth,& �� %ComputedHorizontalScrollBarVisibility��%  &	        &	      &	 2     &	 K     &	 n     &	 �     &	 �     &	 �     &	 �     &		     7  7  7  7  7  &	
     &	 �    7  7  &	 E    7   7  7  7  7  7  7  7 	 7  7 
 7  7 
 7 	 7  7   7  7  7  7  7  7  7 	 7  7 
 7  7 
 7 	 7  &	 �    7  7  7  7  7  7  '�� �    X�5   
   8Q�6.   X�5   
   8Q�6:   X�5   
   1Q�64   �� 5   
   $ 	#66ffffff��66   �� 5   
   $ 	#66ffffff��67   �� 5   
   $ 	#33ffffff��66   �� 5   
   $ 	#ccffffff��68   �� 5   
   $ 	#99ffffff��68   �� 5    
   $ 	#44ffffff��69   �� 5#   
   " ��63   .�5$      ��  � �� 5%      �� 5&      " ��6&   .R�5'      �� $ Border��6!   $��0q�61   $	 0��6<   8��  8�� $��1q�6�   ._�5(      F� $��Center=�6#   $��Center=�6@   8�� ��6�   �� *	Content+
 �� *TemplatedParent+5)       5*      P� 5+       � �� . . �5,   "   �� $ Border��6)   8   � ��6S   5-      P� 5.       � {� .  . �5/   "   �� 8   � ��6?   50      52      54      55      �� 58   
   " ��6,   .�59      ��  � �� 5:      �� 5;      " ��6&   .R�5<      �� 
������ 6!   5=      5?      5@      �� 5C   
   " n�6,   .�5D      ��  � �� 5E      �� 5F      " n�6&   .R�5G      �� $	 0��6!   8��  8�� $��1q�6�   5H      5J      5K      �� 5N   
   " ��6-   .�5O      ��  �@�OverridesDefaultStyle . 66    5P      P� 5R       �@��Orientation$ Vertical=�61   . �5S      ��  � ��8   6.   �� 5T       � ��$
 Auto��6/   �� 5U       � �� 5V      u� 5W      $ 0Q�6(   8  8  8  6�   5Y      �� 5Z       � �� 5[      �� 5\      .R�5]   "   �  5^   &   �� 5_   *   8�� 68   �� 5`   *   �� 5a   *   8�� 68   .+�5c   &   �� $��3��6-   $	 0��6>   8�� 6O   �� 5d   &   $��0��63   8�� 8�� $��ScrollBar.LineUpCommand��6�   $��M 0 4 L 8 4 L 4 0 Z��6�   ]� 5e   &   $ 
PART_Track��6,   $��1��6@   ��.  5f   *   �� 5g   .   8��	 $��ScrollBar.PageUpCommand��6i    5i   *   n� 5j   .   8��
 $��0,1,0,1q�6b    5l   *   �� 5m   .   8�� $��ScrollBar.PageDownCommand��6h   5o   '   �� 5p   &   $��2��63   8�� 8�� $��ScrollBar.LineDownCommand��6�   $��M 0 0 L 4 4 L 8 0 Z��6�   5q   #   5r      5t      5u      P� 5w       �@��Orientation$ 
Horizontal=�61   . �5x      ��  � ��$
 Auto��6.   �� 5y       � ��8  6/   �� 5z       � �� 5{      u� 5|      8  $ 0Q�6T   8  8  6�   5~      �� 5       � �� 5�      �� 5�      .R�5�   "   �  5�   &   �� 5�   *   8�� 6;   �� 5�   *   �� 5�   *   8�� 6;   .+�5�   &   �� $��3��6-   $	 0��6A   8�� 6R   �� 5�   &   $��0��63   8�� 8�� $��ScrollBar.LineLeftCommand��6�   $��M 4 0 L 4 8 L 0 4 Z��6�   ]� 5�   &   $ 
PART_Track��6,   $��1��6@   ��.   5�   *   �� 5�   .   8�� $��ScrollBar.PageLeftCommand��6i    5�   *   n� 5�   .   8�� $��1,0,1,0q�6b    5�   *   �� 5�   .   8�� $ ��ScrollBar.PageRightCommand��6h   5�   '   �� 5�   &   $��2��63   8�� 8�� $ ��ScrollBar.LineRightCommand��6�   $��M 0 0 L 4 4 L 0 8 Z��6�   5�   #   5�      5�      5�      5�      �� 5�   
   " ��60   .�5�      ��  � �� 5�      �� 5�      " ��6&   .R�5�      �  5�      �� 5�   "   �� 5�   "   $
��Auto �63    5�      �� 5�   "   �� 5�   "   $
��Auto �60   .+�5�      �� $��0��65   $��0��6E   ��5�   "   u� 5�   &   8   8  8  8  6�   5�      �� 5�      $ PART_VerticalScrollBar��6(   $��1��6H   $��0��6X   $ Vertical=�6e   # z 6|   # z 6�   #! z  6�   #y�z" 6  8�� 6E  �� 5�      $ PART_HorizontalScrollBar��6(   $��0��6J   $��1��6Z   $ 
Horizontal=�6g   # z# 6�   # z$ 6�   #! z% 6�   #y�z& 6  8�� 6K  �� 5�      $��1��6%   $��1��65   5�      5�      5�      5�      �� 5�   
   " ��6   .�5�      ��  � ��
 �����6;   5�      5�      .��5�      �� #��[ 6   $
��Auto=�5�      $
��Auto=�69   .��5�   
   � -   5        $��
MediaPanel��5�      5�      5�      5�      