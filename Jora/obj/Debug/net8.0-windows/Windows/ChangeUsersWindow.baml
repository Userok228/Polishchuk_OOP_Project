   M S B A M L   `   `   `  ����?  ;Jora, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:JoraJora  5   	       Jora.ChangeUsersWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   $localclr-namespace:Jora     5   	     � Title$  ChangeUsersWindow��5   	   $	��800��6#   $	��600��60   .��5	      � $	��600��6   $	��800��6   .+�5
   
   �� $��
User name:��6   $��Center=�6%   $
��Left=�6G   $��40��6b   $��
20,110,0,0q�6n   $	��Top=�6�   $	��388��6�   $��Arial)�6�   $��24(�6�   �� 5   
   -   5         ��Name$ txtbx_UserName��5      $��Center=�6*   $��Center=�6L   $��40��6i   $��	0,150,0,0q�6u    �� TextWrapping$
 Wrap=�6�   $	��Top=�6�   $	��560��6�   $��Arial)�6�   $��24(�6�   �� 5   
   $��Login:��6   $��Center=�6!   $
��Left=�6C   $��40��6^   $��	20,10,0,0q�6j   $	��Top=�6}   $	��388��6�   $��Arial)�6�   $��24(�6�   �� 5   
   -   5        $ txtbx_Login��5      $��Center=�6'   $��Center=�6I   $��40��6f   $��0,50,0,0q�6r   $
 Wrap=�6�   $	��Top=�6�   $	��560��6�   $��Arial)�6�   $��24(�6�    �� 
IsReadOnly$
 True��6�   �� 5   
   $��Email:��6   $��Center=�6!   $
��Left=�6C   $��40��6^   $��
20,210,0,0q�6j   $	��Top=�6~   $	��388��6�   $��Arial)�6�   $��24(�6�   �� 5   
   -   5        $ txtbx_Email��5      $��Center=�6'   $��Center=�6I   $��40��6f   $��	0,250,0,0q�6r   $
 Wrap=�6�   $	��Top=�6�   $	��560��6�   $��Arial)�6�   $��24(�6�   �� 5   
   -   5         ��Name$ btn_ChangePassword��5      $��Change password��6-   $��Center=�6G   $��70��6d   $��	0,504,0,0q�6p   $	��Top=�6�   $	��560��6�   ���	#FFCBF1FF$��24(�6�   $��Arial)�6�   
���   �6�   �� 5   
   -   5        $ 
btn_Cancel��5      $��Cancel��6%   $
��Left=�66   $��70��6Q   $��318,594,0,0q�6]   $	��Top=�6r   $	��262��6�   ���	#FFCBF1FF$��24(�6�   $��Arial)�6�   
���   �6�   �� 5   
   -   5        $ btn_SaveChanges��5      $��Save Changes��6*   $
��Left=�6A   $��70��6\   $��
20,594,0,0q�6h   $	��Top=�6|   $	��262��6�   ���	#FFCBF1FF$��24(�6�   $��Arial)�6�   
���   �6�   �� 5   
   -   5         ��Name$ lbl_OldPassword��5      $��Enter your old password:��6)   $��Center=�6L   $
��Left=�6n   $��40��6�   $��
20,308,0,0q�6�   $	��Top=�6�   $	��388��6�   $��Arial)�6�   $��24(�6�   $y�Hidden=�6�   �� 5   
   -   5        $ txtbx_OldPassword��5      $��Center=�6-   $��Center=�6O   $��40��6l   $
 Wrap=�6x   $	��Top=�6�   $	��560��6�   $��Arial)�6�   $��24(�6�   $��	0,348,0,0q�6�   $y�Hidden=�6�   �� 5   
   -	   5        $ lbl_NewPassword��5      $��Enter new password:��6)   $��Center=�6G   $
��Left=�6i   $��40��6�   $��
20,410,0,0q�6�   $	��Top=�6�   $	��388��6�   $��Arial)�6�   $��24(�6�   $y�Hidden=�6�   �� 5   
   -
   5        $ txtbx_NewPassword��5      $��Center=�6-   $��Center=�6O   $��40��6l   $
 Wrap=�6x   $	��Top=�6�   $	��560��6�   $��Arial)�6�   $��24(�6�   $��	0,450,0,0q�6�   $y�Hidden=�6�   �� 5   
   -   5        $ 
btn_LogOut��5      $��Log out��6%   $��Center=�67   $��36��6T   $��	0,723,0,0q�6`   $	��Top=�6s   $��92��6�   $��24(�6�   $��Arial)�6�   ���	#FFF50000��6�   Z� ��6�   Z� 5      5      