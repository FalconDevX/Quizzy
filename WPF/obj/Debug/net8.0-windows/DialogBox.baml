   M S B A M L   `   `   `  ����>  :WPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:WPFWPF  5   	       WPF.DialogBox   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   #localclr-namespace:WPF     5   	     � 
ResizeMode$  NoResize=�5   	    � WindowStartupLocation$ CenterScreen=�5   	    � WindowStyle$
 None=�5	   	    � Title$ 	DialogBox��5   	   $	��250��6   $	��400��6(    � 	Resources 5         TextBox.Static.Border �� Color  TextBox.MouseOver.Border  TextBox.Focus.Border �� 
TargetType �� Property �� Value	 �� 
TargetType
 ��Name �� CornerRadius J� SnapsToDevicePixels ��Name ��Name �� RelativeSource �� Path �� Triggers P� Property P� Value �� 
TargetName��%A  &	        &	 !     &	 D     '�� g     7   7  7  �� 5   
   $ #8FAFE6��68   �� 5   
   $ 	#FF7EB4EA��6;   �� 5   
   $ 	#FF569DE5��67   �� 5   
   " ��6   .�5      ��  �@��SelectionBrush
 �����6/   �� 5       �@��SelectionOpacity$	 0.5Q�61   �� 5       � ��# � ��6+   �� 5       � ��8   6,   �� 5       � ��# � ��6+   �� 5       � ��$ 1q�60   �� 5       � ��$
 Left=�6;   �� 5       �@�FocusVisualStyle 61   Z� �� 5       �@J�	AllowDrop$
 true��6*   �� 5       � ��$ Center=�69   �� 5        � �� 5!      �� 5"      "	 ��6&   .R�5#      �� $
 border��6!   #��z��61   #��z��6[   #��z��6�   $ 5��6�   $
 True��6�   ._�5$      � .+�5%   "   �� $ PART_ContentHost��6/   $|�false��6I   $��Hidden=�6[   $��Hidden=�6�   �� 5(   "   $ placeHolderTextBlock��6,   ���#8FAFE6#��z��5*   %   $S��Mpack://application:,,,/Resources/RobotoCondensed-Regular.ttf#Roboto Condensed)�5+   %   $��Center=�5,   %   $
��Left=�5-   %   $��5,0,0,0q�5.   %   $y�	Collapsed=�5/   %   ��50   %   ��  �� *TemplatedParent+$	 Tag�51      52       53      P� 54       �@J�IsMouseOver$
 true��6=   . �55   "   �� $ border��6@   8   � ��6T   56      P� 57       �@J�IsKeyboardFocused$
 true��6C   . �58   "   �� $ border��6@   8   � ��6T   59      P� 5:       � ��$  ��66   . �5;   "   �� $ placeHolderTextBlock��6?    � y�$ Visible=�6a   5<      5>      5@      5A      5B      .��5D      � .+�5F   
   �� $��-10,-12,-13,-11q�6   $��/DailogBoxBackground.png��6)   �� 5H   
   $	��259��6   $��Center=�6"   .+�5J      �� -   5        $ ChangeLoginTextBlock��5J      $��Center=�66   
�������$��0,20,0,0q�6f   $
��Wrap=�6x   $��Provide new login��6�   $��30(�6�   $S��Mpack://application:,,,/Resources/RobotoCondensed-Regular.ttf#Roboto Condensed)�6�   $	��212��6  �� 5M      -   5        $ DeleteTextBlock��5M      $��Center=�61   
�������$��0,20,0,0q�6a   $
��Wrap=�6s   $��Delete account��6�   $��30(�6�   $S��Mpack://application:,,,/Resources/RobotoCondensed-Regular.ttf#Roboto Condensed)�6�   $	��212��6  �� 5P      $��Center=�6   $��0,20,0,0q�62   $	��240��6D   $��40��6P   ���#191D67$��1q�6r   $ 10��6�   ���#191D67._�5Q      �� -   5         ��Name$ DialogBoxTextBox��5Q      
 � Tag$ Type new login��67   $��Center=�6L   $��40��6n   ���#8FAFE6 �� 
CaretBrush �#8FAFE6
������ $��0q�6�   $��16(�6�    � Cursor$ IBeam��6�   $��
10,-1,9,-1q�6�   $S��Mpack://application:,,,/Resources/RobotoCondensed-Regular.ttf#Roboto Condensed)�6)  5R      �� 5U      -   5        $ DialogBoxTextBlockInfo��5U      $��Center=�68   ���#DE8689$��0,10,0,0q�6j   $
��Wrap=�6|   $��Login taken��6�   $��15(�6�   $S��Mpack://application:,,,/Resources/RobotoCondensed-Regular.ttf#Roboto Condensed)�6�   $y�Hidden=�6  �� 5X      -   5         ��Name$ ChangeButton��5X      $y�	Collapsed=�6+   $��Center=�6B   $��0,30,0,0q�6_   $��Change��6q   ���#DE86894  .System.Windows.Automation.AutomationProperties   Name$ LoginButton��6�   $	��240��6�   $��46��6�   $��22(�6�   $
��Bold"�6�   
�������$
 Hand��6  $S��Mpack://application:,,,/Resources/RobotoCondensed-Regular.ttf#Roboto Condensed)�6  ��5Y      �� 5Z      "	 ��6&   .R�5[      �� #��z��6!   $ 5��6K   #��z��6\   ._�5\      �� $��Center=�6/   $��Center=�6L   5]      5^      5`      �� 5c      -   5        $ DeleteButton��5c      $��Delete��6+   $y�	Collapsed=�6<   $��Center=�6S   $��0,30,0,0q�6p   ���#DE8689$ LoginButton��6�   $	��240��6�   $��46��6�   $��22(�6�   $
��Bold"�6�   
�������$
 Hand��6  $S��Mpack://application:,,,/Resources/RobotoCondensed-Regular.ttf#Roboto Condensed)�6  ��5d      �� 5e      "	 ��6&   .R�5f      �� #��z��6!   $ 5��6K   #��z��6\   ._�5g      �� $��Center=�6/   $��Center=�6L   5h      5i      5k      5m      �� 5p   
   -   5        $ CloseButton��5p      $ CloseButton��6&   
������ $��0q�6g   $��353,-12,7,206q�6�    � FocusVisualStyle 6{   Z� ��5q      �� 5r      "	 ��6"   .R�5s      �� 
������ $ 15��66   ._�5t      ��  ��Name$ AvatarImage��6    $*��$/Resources/StartScreen/CloseIcon.png��65   $��Uniform=�6c   $��Center=�6u   $��Center=�6�   5u       5v      P� 5w       �@J�IsMouseOver$
 True��69   . �5x      �� $ AvatarImage��6%    � ��$0 */Resources/StartScreen/CloseIcon_Hover.png��6P   5y      5{      5}      5      5�      