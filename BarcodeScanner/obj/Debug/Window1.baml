   M S B A M L   `   `   `  ����  BarcodeScanner    BarcodeScanner.Window1   5   	   \ XPresentationFramework, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      8x,http://schemas.microsoft.com/winfx/2006/xaml    5      Z VWPFToolkit.Extended, Version=1.5.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f4XtoolkitJhttp://schemas.microsoft.com/winfx/2006/xaml/presentation/toolkit/extended  5        � Title$  JOTARI Programma��5      $	��800��6   $
��1000��6+    � WindowState$ 	Maximized=�5      .��5      �� $��Center=�6    � 	Resources 5	   
    �� 
TargetType �� Property �� Value��%�  '��       '�� q     '��     '�� u    �� 5
      " ��6   .�5      ��  � ��$	 100(�6-   �� 5       � ��$ Center=�68   5      �� 5      " ��6   .�5      ��  � ��$	 100(�6-   �� 5       � ��$ Center=�68   �� 5       � ��$
 Wrap=�61   5      �� 5      " ��6   .�5      ��  � ��$ 50��6+   �� 5       � ��$	 200��6*   5      �� 5      " ��6   .�5      ��  � ��$ 20(�6-   5      5      .+�5   
   ��  �� Orientation$ 
Horizontal=�6   $��Center=�6.   .+�5      �� -   5        $��daySelector��5       �� SelectedIndex$ 1��6*   $	��0,0q�6<   $��20(�6I   $��50��5      .@�5!      ��  �� 
IsSelected . .��61   Auto67   �� 5"      .��6   
Zaterdag6)   �� 5#      .��6   Zondag6'   5$      �� 5&      -   5        $ 
Horizontal=�5&      $��50��5'      .+�5)      �� -   5        	 ��Name$	 txtNum1��5)      $��50��6+   $��0��66   $��Center=�5*      $��Center=�65   $��50��5+      �� 5,      $ Vertical=�6   $��Stretch=�64   $��50��5-      .+�5.      �� -   5        
 ��Name$
 cmdUp1��5.      $��˄��6-   $��50��69   $��25��5/      �� 50      -   5        $
 cmdDown1��50      $��˅��6/   $��50��6;   $��25��51      52      53      �� 54      $��20(�6   $��50��55      :6&   �� 56      -   5        $ 
Horizontal=�56      $��50��57      .+�59      �� -   5        $	 txtNum2��59      $��50��6+   $��0��66   $��Center=�5:      $��Center=�65   $��50��5;      �� 5<      $ Vertical=�6   $��Stretch=�64   $��50��5=      .+�5>      �� -   5        $
 cmdUp2��5>      $��˄��6-   $��50��69   $��25��5?      �� 5@      -	   5        $
 cmdDown2��5@      $��˅��6/   $��50��6;   $��25��5A      5B      5C      �� 5E      -
   5        $��groupSelector��5E      $	��0,0q�6,   $ 0��69   $��20(�6K   $��50��5F      .@�5H      �� .��6   Auto6%   5I      �� 5J      -   5        .��5J   *   Zoek60   5K      �� 5N   
   $ 
Horizontal=�6   $��Center=�6.   .+�5O      �� .��6   Groep6   �� 5P      $��50��6    J� Opacity$ 0Q�6#   �� 5Q      -   5         ��Name$ groupDisplay��5Q      $��xx��6*   5R      �� 5S   
   $��50(�6   .��6   	gaat naar6)   �� 5V   
   
��� � �$��4q�6%   ._�5W   
   �� $ 
Horizontal=�6   $��Center=�6.   $��Center=�6K   .+�5X      �� -   5         ��Name$ ActivityDisplay��5X      $��
activiteit��63   $��Center=�6E   �� 5Y      -   5         ��Name$ ActivityImage��5Y      $	��200��6/   $��10q�6?   5Z      5[      �� 5^   
   $��50(�6   .��6   daarna6&   �� 5_   
   $ 
Horizontal=�6   $��Center=�6.   .+�5`      �� -   5        $ NextActivityDisplay��5`      $��volgende��65   $��60(�6E   $��Center=�6S   �� 5a      $
�� om ��6   $��60(�6$   $��Center=�62   �� 5b      -   5        $ NextActivityStartTime��5b      $
��tijd��67   $��60(�6C   $��Center=�6Q   5d      5e      5f      