   M S B A M L   `   `   `  ����H  DServerManager, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:ServerManagerServerManager  5   	       ServerManager.Loading   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   T PMahApps.Metro, Version=1.4.3.0, Culture=neutral, PublicKeyToken=f4fb5a3c4d1e5b4f;Controls,http://metro.mahapps.com/winfx/xaml/controls  5   	   Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  5   	   'localclr-namespace:ServerManager  5   	      AccentColorBrush(  "MahApps.Metro.Controls.MetroWindow    	GlowBrush#  �   5   	    � Title$ Database Connection��5
   	   $��285.553��6%   $��442.775��66    � 
ResizeMode$ NoResize=�6F    � WindowStartupLocation$ CenterScreen=�6\   
������� � Icon$ Images/logo.ico��6�   .��5      � $
��Left=�6   $	��453��6&   $��	-3,-3,0,5q�62   .+�5   
   �� -   5         ��Name$ label��5      $��Connect to Server��6   $
��Left=�6;   $	��Top=�6V   $��
157,33,0,0q�6n   �� 5   
   -   5         ��Name$ image��5      $
��Left=�6   $��83��6:   $	��Top=�6F   $��83��6^   $��308,103,0,0q�6i   $��Images/logo.png��6~   �� 5   
   -   5         ��Name$ sqlusr��5      $
��Left=�6"   $��23��6=    �� TextWrapping$
 Wrap=�6I   $	��Top=�6]   $	��146��6u   $��149,107,0,0q�6�   �� 5   
   -   5        $ sqlpwd��5      $
��Left=�6"   $��23��6=   $
 Wrap=�6I   $	��Top=�6]   $	��146��6u   $��149,141,0,0q�6�   �� 5   
   -   5        $ label1��5      $��	SQL User:��6    $
��Left=�64   $	��Top=�6O   $��
64,103,0,0q�6g   �� 5   
   -   5        $ label2��5      $��SQL PW:��6    $
��Left=�62   $	��Top=�6M   $��
65,136,0,0q�6e   �� 5   
   -   5        	 ��Name$	 connect��5      $��Connect��6"   $
��Left=�64   $	��Top=�6O   $��78��6g   $��222,197,0,0q�6r   $��23��6�   6�   �� 5   
   -   5        $	 saveBtn��5      $��
Save Login��6"   $
��Left=�67   $��23��6R   $��125,197,0,0q�6^   $	��Top=�6s   $��75��6�   
 � ToolTip$J
 DClicking here will save in file the login details for further logins��6�   6�   �� 5   
   -	   5        $ sqlip��5      $
��Left=�6!   $��23��6<   $
 Wrap=�6H   $	��Top=�6\   $	��146��6t   $��
149,73,0,0q�6�   �� 5   
   -
   5        $ label1_Copy��5      $��
IP Adress:��6%   $
��Left=�6:   $	��Top=�6U   $��	62,72,0,0q�6m   )  #MahApps.Metro.Controls.ProgressRing  5   
   -   5          Name$
 load��5       $
��Left=�6.   $��317,197,0,0q�6I   $	��Top=�6^   $��21��6v   $��32��6�   ���	#CC00C5FF6�   �� 5   
   -   5        $	 por��5      $��SQL Port��6   $
��Left=�60   $��
308,41,0,0q�6K   $	��Top=�6_   �� 5   
   -   5        $ sqlport��5      $
��Left=�6#   $��23��6>   $��
308,72,0,0q�6J   $
 Wrap=�6^   $	��Top=�6r   $��61��6�   5      5      