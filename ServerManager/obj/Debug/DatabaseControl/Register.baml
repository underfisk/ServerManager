   M S B A M L   `   `   `  ����H  DServerManager, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullM+clr-namespace:ServerManager.DatabaseControlServerManager.DatabaseControl  5   	   ,    &ServerManager.DatabaseControl.Register   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   T PMahApps.Metro, Version=1.4.3.0, Culture=neutral, PublicKeyToken=f4fb5a3c4d1e5b4f;Controls,http://metro.mahapps.com/winfx/xaml/controls  5   	   :d2http://schemas.microsoft.com/expression/blend/2008  5   	   Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  5   	   7local+clr-namespace:ServerManager.DatabaseControl  5   	      AccentColorBrush(  "MahApps.Metro.Controls.MetroWindow    	GlowBrush#  �   5	   	    � Title$ Create new account��5
   	   $��313.371��6$   $��507.243��65    � 
ResizeMode$ NoResize=�6E    � WindowStartupLocation$ CenterScreen=�6[    � Icon$. (/ServerManager;component/Images/logo.ico��6�   ��5      �� 5   
    �� ImageSource$4 ./ServerManager;component/Images/registerbg.png��6    j� Stretch$ UniformToFill=�6R   .��5      � $
��Left=�6   $	��301��6&   $��
-3,-4,-6,0q�63   $	��Top=�6G   $	��504��6_   .+�5   
   �� -   5         ��Name$ label��5      $��	Username:��6   $
��Left=�63   $	��Top=�6N   $��	33,53,0,0q�6f   ��6y   Z� ��5      �� 5       �� Color# � ��6"   5      �� 5   
   -   5        $ label1��5      $��	Password:��6    $
��Left=�64   $��	33,84,0,0q�6O   $	��Top=�6b   #��� ��6z   �� 5   
   -   5        $ label2��5      $��Confirm Password:��6    $
��Left=�6<   $��
33,115,0,0q�6W   $	��Top=�6k   #��� ��6�   ��6�   Z� 6�   �� 5   
   -   5        $ label3��5      $��E-mail:��6    $
��Left=�62   $��
33,146,0,0q�6M   $	��Top=�6a   #��� ��6y   �� 5   
   -   5        	 ��Name$	 register��5      $��Register��6#   $
��Left=�66   $��148,211,0,0q�6Q   $	��Top=�6f   $��75��6~   $��5��6�   �� 5   
   -   5        $	 cancel��5      $��Cancel��6!   $
��Left=�62   $��249,211,0,0q�6M   $	��Top=�6b   $��75��6z   $��6��6�   �� 5   
   -   5        
 ��Name$	
 pw2��5      $
��Left=�6   $��10��6:   $��148,115,0,0q�6F    �� TextWrapping$
 Wrap=�6[   $	��Top=�6o   $	��176��6�   $��3��6�    �� 	MaxLength$ 25��6�   �� 5   
   -   5        $	
 usr��5      $
��Left=�6   $��23��6:   $
 Wrap=�6F   $	��Top=�6Z   $	��176��6r   $��
148,53,0,0q�6~   $��1��6�   $ 15��6�   �� 5   
   -	   5        $	
 pw1��5      $
��Left=�6   $��23��6:   $
 Wrap=�6F   $	��Top=�6Z   $	��176��6r   $��
148,84,0,0q�6~   $��2��6�   $ 25��6�   �� 5   
   -
   5        $
 email��5      $
��Left=�6!   $��23��6<   $
 Wrap=�6H   $	��Top=�6\   $	��176��6t   $��148,146,0,0q�6�   $��4��6�   $ 50��6�   5      5      