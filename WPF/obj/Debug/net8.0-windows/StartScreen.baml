   M S B A M L   `   `   `  ����>  :WPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:WPFWPF  5          WPF.StartScreen   5      Y UMpCoding.WPF.LoadingAnimations, Version=1.0.2.0, Culture=neutral, PublicKeyToken=nullBmp9http://schemas.mpcoding.com/mpcoding/wpf/loadinganimation  5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      #localclr-namespace:WPF     5        � WindowStyle$
  None=�5	       � AllowsTransparency$
 True��5
      
������  � 
ResizeMode$ CanResizeWithGrip=�5       � WindowStartupLocation$ CenterScreen=�5       � Title$ Quizzy��5      $	��540��6    $	��960��6-    � SizeToContent$ WidthAndHeight=�69   $	��550��5      $	��450��6!    � Icon$$ Resources/StartScreen/Icon.ico��5       � 	Resources 5         TextBox.Static.Border �� Color  TextBox.MouseOver.Border  TextBox.Focus.Border	 �� 
TargetType
 �� Property �� Value!  System.Windows.SystemColors   WindowBrushKey   ControlTextBrushKey �� 
TargetType ��Name �� CornerRadius J� SnapsToDevicePixels ��Name ��Name �� RelativeSource �� Path �� Triggers P� Property P� Value �� 
TargetName  PasswordStyle �� TextAlignment �� Padding �� Value �� Binding �� 
Background �� Bullet  FadeOut  �� TargetProperty	! W� To" g� Duration 
 FadeIn# W� From��%/  &	        &	 !     &	 D     '�� g     7   7  7  &	     '�� k	    &	     &	 o    �� 5      $ #8FAFE6��6@   �� 5      $ 	#FF7EB4EA��6C   �� 5      $ 	#FF569DE5��6?   �� 5      "	 ��6   .�5      �� 
 �@��SelectionBrush
 �����67   �� 5      
 �@��SelectionOpacity$	 0.5Q�69   �� 5      
 � ��# �  63   �� 5      
 � ��8   64   �� 5      
 � ��# �  63   �� 5      
 � ��$ 1q�68   �� 5       
 � ��$
 Left=�6C   �� 5!      
 �@�FocusVisualStyle 69   Z� �� 5"      
 �@J�	AllowDrop$
 true��62   �� 5#      
 � ��$ Center=�6A   �� 5&      
 � �� 5'      �� 5(      " ��6.   .R�5)   "   �� $ border��6)   #��z��69   #��z��6c   #��z��6�   $ 5��6�   $
 True��6�   ._�5*   &   � .+�5+   *   �� $ PART_ContentHost��67   $|�false��6Q   $��Hidden=�6c   $��Hidden=�6�   �� 5.   *   $ placeHolderTextBlock��64   ���#8FAFE6#��z��50   4   $S��Mpack://application:,,,/Resources/RobotoCondensed-Regular.ttf#Roboto Condensed)�51   4   $��Center=�52   4   $
��Left=�53   4   $��5,0,0,0q�54   4   $y�	Collapsed=�55   4   ��56   4   ��  �� *TemplatedParent+$	 Tag�57   '   58   #    59   "   P� 5:   &    �@J�IsMouseOver$
 true��6E   . �5;   *   �� $ border��6H   8  
 � ��6\   5<   '   P� 5=   &    �@J�IsKeyboardFocused$
 true��6K   . �5>   *   �� $ border��6H   8  
 � ��6\   5?   '   P� 5@   &    � ��$  ��6>   . �5A   *   �� $ placeHolderTextBlock��6G   
 � y�$ Visible=�6i   5B   '   5D      5F      5G      �� 5K      "	 G�6.   .�5L      �� 
 �@G�SelectionBrush
 �����67   �� 5M      
 �@G�SelectionOpacity$	 0.5Q�69   �� 5N      
 � ��$ Center=�6:   �� 5O      
 � �� 5P      �� 5Q      " G�6.   .R�5R   "   �� #��z��6)   ._�5S   &   � .+�5U   *   �� $ PART_ContentHost��67   $��8,0,0,0q�6Q   �� 5X   *   $ PART_TempText��64   $��Type password��5Y   ,   ���#8FAFE6#��z��5[   ,   $y�	Collapsed=�5\   ,   $ Center=�5]   ,   $��Center=�5^   ,   $
��Left=�5_   ,   $ 8,0,0,0q�5`   ,   $S��Mpack://application:,,,/Resources/RobotoCondensed-Regular.ttf#Roboto Condensed)�5a   ,   5b   '   5c   #    5d   "   �� 5e   &   $ 0��6|    62   �� *Password.Length+ �� *Self+.a�5f   *   �� $ PART_TempText��61   
 � y�$ Visible=�6b   5g   '   5i      5k      5l      �� 5q      "	 ��6   .�5r      �� 
 �@�FocusVisualStyle 69   Z� �� 5s      
 � ��
 ���� 64   �� 5t      
 � ��$ 0q�68   �� 5u      
 � �� 5v      �� 5w      " ��6.   .R�5x   "   � .+�5y   &   �� 
 ����  5z   *   �� 5{   .   $ bullet��65   $��16��6E   $��16��6P   ���#8FAFE6$��1q�6r   $ 5��6�   ._�5}   *   �� $��Center=�6;   $��5,0,0,0q�6V   5~   '   5   #    5�   "   P� 5�   &    �@d�	IsChecked$
 TrueY�6C   . �5�   *   �� $ bullet��61   
 � �� �	#FF7EB4EA6[   5�   '   5�      5�      �� 5�      
 � �� �#8FAFE663   �� 5�      
 � ��$ 14(�61   5�      �� 5�      .��5�      W� $  Opacity�6&   ! 06J   $" 0:0:0.5D�6Q   5�      �� 5�      .��5�      W� $  Opacity�6&   # 06J   ! 16S   $" 0:0:0.5D�6Z   5�      5�      .��5�      � -   5        $ �Name$$ StartScreenMainGrid��5�      ��5�      �� 5�      % �� ImageSource$*% $Resources/StartScreen/Background.png��6!   & j� Stretch$& UniformToFill=�6T   .+�5�      �� -   5        ' ��Name$' StartScreenLogo��5�   6   $��/StartScreenLogo.png��6   $��Center=�6O   $	��Top=�6l   $��UniformToFill=�6�   $	��204��6�   $	��315��6�   $��	0,-60,0,0q�6�   �� 5�      -   5        ( ��Name$( 
LoginPanel��5�      4  .System.Windows.Automation.AutomationProperties)   Name$) 
LoginPanel��61   $��	0,120,0,0q�6X   $	��Top=�6k   $��Center=�6�   .+�5�      �� $
��Left=�6   $	��237��68   $��40��6D   ���#191D67$��1q�6f   $ 10��6z   ���#191D67$��	30,30,0,0q�6�   ._�5�      �� -   5        * ��Name$* EmailTextBoxLogin��5�   "   
+ � Tag$+ Type e-mail or login��6>   $��Center=�6Y   $) EmailTextBoxLogin��6{   $��40��6�   ���#8FAFE6, �� 
CaretBrush, �#8FAFE6
������ $��0q�6�   $��16(�6  - � Cursor$- IBeam��6  $��
10,-1,9,-1q�6w  $S��Mpack://application:,,,/Resources/RobotoCondensed-Regular.ttf#Roboto Condensed)�6�  5�      �� 5�      -   5        $ PassTextBorLogin��5�      $y�	Collapsed=�67   $
��Left=�6N   $	��236��6i   $��40��6u   ���#191D67$��1q�6�   $ 10��6�   ���#191D67$��	30,20,0,0q�6�   ._�5�      �� -   5        $* PassTextBoxLogin��5�   "   $+ Type password��6=   $��Center=�6Q   $) PassTextBoxLogin��6s   $��40��6�   ���#8FAFE6, �#8FAFE6
������ $S��Mpack://application:,,,/Resources/RobotoCondensed-Regular.ttf#Roboto Condensed)�6�   $��0q�6J  $��16(�6^  $- IBeam��6l  $��
10,-1,9,-1q�6{  5�      �� 5�      -   5        $ PassBorLogin��5�      $y�Visible=�63   $
��Left=�6H   $	��236��6c   $��40��6o   ���#191D67$��1q�6�   $ 10��6�   ���#191D67$��
30,20,0, 0q�6�   ._�5�      G� -   5        . G�Name$. PassBoxLogin��5�   &   #��[ 6<   $��Center=�6c   $
��Left=�6�   $) PassBoxLogin��6�   $��40��6�   ���#8FAFE6/ G� 
CaretBrush/ �#8FAFE6
������ $��0q�6  $��16(�6,  $- IBeam��6:  $��7,0,0,0q�6I  $	��225��6Z  $S��Mpack://application:,,,/Resources/RobotoCondensed-Regular.ttf#Roboto Condensed)�6�  5�      �� 5�      -	   5        0 ��Name$0 ShowPasswordLogin��5�      $��Show Password��6V   $��Center=�6o   $��Center=�6�   $��0,15,0,0q�6�   ���#8FAFE6$S��Mpack://application:,,,/Resources/RobotoCondensed-Regular.ttf#Roboto Condensed)�6�   $��14(�6)  �� 5�      -
   5        1 ��Name$1 LoginButton��5�      $��Login��62   ���#DE8689$) LoginButton��6X   $	��240��6�   $��46��6�   $
��Left=�6�   $	��Top=�6�   $��22(�6�   $
��Bold"�6�   
�������$��	27,20,0,0q�6�   $
- Hand��6  $S��Mpack://application:,,,/Resources/RobotoCondensed-Regular.ttf#Roboto Condensed)�6:  ��5�      �� 5�      " ��6.   .R�5�   "   �� #��z��6)   $ 5��6S   #��z��6d   ._�5�   &   �� $��Center=�67   $��Center=�6T   5�   #   5�      5�      �� 5�      -   5        $��I don't have an account��5�      $
��Left=�6>   $	��Top=�6Y   ���#8FAFE6$��16(�6�   $��70,8,0,0q�6�   $
- Hand��6�   $) NoAccountLabel��6�   $S��Mpack://application:,,,/Resources/RobotoCondensed-Regular.ttf#Roboto Condensed)�6  =  7MpCoding.WPF.LoadingAnimations.Controls.DotsRingSpinner  5�      -   5        2  Name$2 LoadingSpinner��5�   )   $��64��6A   $��63��6L   3   Color3 �#8A11784   Duration$4 0:0:2.0D�6h   $y�Hidden=�6{   5�      �� 5�   
   -   5        $( RegisterPanel��5�      $) RegisterPanel��6,   $��Center=�6V   $��	0,140,0,0q�6s   $	��Top=�6�   .+�5�      �� $
��Left=�6   $	��250��68   $��40��6D   ���#191D67$��1q�6f   $ 10��6z   ���#191D67$��23,6,0,0q�6�   ._�5�      �� -   5        $* EmailTextBoxRegister��5�   "   $+ Type e-mail��6A   $��Center=�6S   $) EmailTextBoxRegister��6u   $��40��6�   , �#8FAFE6
������ ���#8FAFE6$��0q�6�   $��16(�6	  $- IBeam��6  $��	9,-1,9,-1q�6z  $S��Mpack://application:,,,/Resources/RobotoCondensed-Regular.ttf#Roboto Condensed)�6�  5�      �� 5�      -   5        5 ��Name$5 InvalidEmailLabelRegister��5�      $��Invalid Email��6A   $
��Left=�6Y   $��	105,0,0,0q�6t   $	��Top=�6�   ���#DE8689$) InvalidEmailLabelRegister��6�   $y�Hidden=�6�   �� 5�      $
��Left=�6   $	��250��68   $��40��6D   ���#191D67$��1q�6f   $ 10��6z   ���#191D67$��	23,0,15,0q�6�   ._�5�      �� -   5        $* NickTextBoxRegister��5�   "   $+ 
Type login��6?   $��Center=�6P   ���#8FAFE6$) NickTextBoxRegister��6�   $��40��6�   , �#8FAFE6
������ $��0q�6�   $��16(�6  $- IBeam��6  $��	9,-1,9,-1q�6t  $S��Mpack://application:,,,/Resources/RobotoCondensed-Regular.ttf#Roboto Condensed)�6�  5�      �� 5�      -   5        $ PassTextBorRegister��5�      $y�	Collapsed=�6:   $
��Left=�6R   $	��250��6m   $��40��6y   ���#191D67$��1q�6�   $ 10��6�   ���#191D67$��	23,20,0,0q�6�   ._�5�      �� -   5        $* PassTextBoxRegister��5�   "   $+ Type password��6?   $��Center=�6S   $) PassTextBoxRegister��6u   $��40��6�   , �#8FAFE6
������ $S��Mpack://application:,,,/Resources/RobotoCondensed-Regular.ttf#Roboto Condensed)�6�   ���#8FAFE6$��0q�6O  $��16(�6c  $- IBeam��6q  $��	9,-1,9,-1q�6�  5�      �� 5�      -   5        $ PassBorRegister��5�      $y�Visible=�66   $
��Left=�6L   $	��250��6g   $��40��6s   ���#191D67$��1q�6�   $ 10��6�   ���#191D67$��	23,20,0,0q�6�   ._�5�      G� -   5        $. PassBoxRegister��5�   &   #��[ 6?   $��Center=�6f   $+ Type password��6�   $��Center=�6�   $) PassTextBoxRegister��6�   $��40��6�   ���#8FAFE6/ �#8FAFE6
������ $��0q�68  $��16(�6L  $- IBeam��6Z  $	��230��6�  $S��Mpack://application:,,,/Resources/RobotoCondensed-Regular.ttf#Roboto Condensed)�6�  $��-6,0,0,0q�6X  5�      �� 5�      -   5        $5 InvalidPassLabel��5�      $1��+Passwd must be 6-15 long 1 Uppercase 1 sign��68   $��Center=�6n   $��0,0,0,0q�6�   $	��Top=�6�   ���#DE8689$) InvalidEmailLabelRegister��6�   $y�Hidden=�6�   �� 5�      -   5        $ RepPassTextBorRegister��5�   4   $y�	Collapsed=�6   $
��Left=�6T   $	��250��6o   $��40��6{   ���#191D67$��1q�6�   $ 10��6�   ���#191D67$��23,5,0,0q�6�   ._�5�      �� -   5        $* RepPassTextBoxRegister��5�   "   $+ Type password��6B   $��Center=�6V   $) PassTextBoxRegister��6x   $��40��6�   ���#8FAFE6, �#8FAFE6
������ $S��Mpack://application:,,,/Resources/RobotoCondensed-Regular.ttf#Roboto Condensed)�6�   $��0q�6R  $��16(�6f  $- IBeam��6t  $��	9,-1,9,-1q�6�  5�      �� 5�      -   5        $ RepPassBorRegister��5�   2   $y�Visible=�6   $
��Left=�6N   $	��250��6i   $��40��6u   ���#191D67$��1q�6�   $ 10��6�   ���#191D67$��23,5,0,0q�6�   ._�5�      G� -   5        $. RepPassBoxRegister��5�   &   #��[ 6B   $
��Left=�6i   $+ Repeat password��6�   $��Center=�6�   $) RepPassTextBoxRegister��6�   $��40��6�   ���#8FAFE6/ �#8FAFE6
������ $��0q�6>  $��16(�6R  $- IBeam��6`  $	��230��6�  $S��Mpack://application:,,,/Resources/RobotoCondensed-Regular.ttf#Roboto Condensed)�6  $��6,0,0,0q�6g  5�      �� 5�      -   5        $0 ShowPasswordRegister��5�      $��Show Password��6\   $��Center=�6u   $��Center=�6�   $��0,15,0,0q�6�   ���#8FAFE6$S��Mpack://application:,,,/Resources/RobotoCondensed-Regular.ttf#Roboto Condensed)�6�   $��14(�6/  �� 5�      -   5        $5 PassNotMatchLabel��5�      $��Password do not match��69   $��80,0,1,0q�6Y   $	��Top=�6k   ���	#FFFF5700$) PassNotMatchLabel��6�   $y�Hidden=�6�   �� 5�      -   5        $1 RegisterButton��5�   0   $��Register��6   ���#DE8689$) RegisterButton��6^   $��22(�6�   $
��Bold"�6�   
�������$
- Hand��6�   $��15,5,0,0q�6�   $��48��6�   $	��240��6  $S��Mpack://application:,,,/Resources/RobotoCondensed-Regular.ttf#Roboto Condensed)�6  ��5�      �� 5�      " ��6.   .R�5�   "   �� #��z��6)   $ 5��6S   #��z��6d   ._�5�   &   �� $��Center=�67   $��Center=�6T   5�   #   5�      5�      �� 5�      -   5        $��I have an account��5�      ���#8FAFE6$��16(�6M   $
- Hand��6[   $) YesAccountLabel��6i   $	��120��6�   $��17,5,0,0q�6�   $S��Mpack://application:,,,/Resources/RobotoCondensed-Regular.ttf#Roboto Condensed)�6�   5�      �� 5�      -   5        $��Right=�5�      $	��Top=�65   $��43��6M   $��31��6Y   $) CloseButton��6d   
������ $��0q�6�   6 � FocusVisualStyle6 6�   Z� ��5      �� 5     " ��6*   .R�5     �� 
������ $ 15��6>   ._�5  "   �� $' AvatarImage��6(   $��40��6=   $��30��6I   $*��$/Resources/StartScreen/CloseIcon.png��6T   $��Uniform=�6�   $��Center=�6�   $��Center=�6�   5      5     P� 5  "    �@J�IsMouseOver$
 True��6A   . �5  &   �� $ AvatarImage��6-   
 � ��$0 */Resources/StartScreen/CloseIcon_Hover.png��6X   5  #   5
     5     �� 5     -   5        $��Right=�5     $	��Top=�65   $��43��6M   $��31��6Y   $) MinimizeButton��6d   
������ $��0q�6�   $��0,0,35,0q�6�   6 6�   Z� ��5     �� 5     " ��6*   .R�5     �� 
������ $ 15��6>   ._�5  "   �� $' AvatarImage��6(   $,��&Resources/StartScreen/minimizeIcon.png��6=   $��40��6m   $��30��6y   $��Uniform=�6�   $��Center=�6�   $��Center=�6�   5      5     P� 5  "    �@J�IsMouseOver$
 True��6A   . �5  &   �� $ AvatarImage��6-   
 � ��$3 -/Resources/StartScreen/MinimizeIcon_Hover.png��6X   5  #   5     5     5     5      