   M S B A M L   `   `   `  ����M  ISE1610_MyWPF_DEMO1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:SE1610_MyWPF_DEMO1SE1610_MyWPF_DEMO1  5   	   #    SE1610_MyWPF_DEMO1.MainWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   2local clr-namespace:SE1610_MyWPF_DEMO1     5   	     � Title$  
MainWindow��5   	   $	��450��6   $	��800��6)    � 	Resources 5	         	SolidGray �� Color  
SolidBlack  myGradientBrush � Color � Offset��%I  &	        &	 #     &	 G     �� 5
   
   $
 Gray��6,   6;   �� 5   
   $ Black��6-   6=   �� 5   
   .��5      � $ White��6   $ 0Q�6)   67   � 5      $ Yellow��6   $
 0.25Q�6*   6:   � 5      $ Green��6   $
 0.75Q�6)   69   � 5      $
 Blue��6   $ 1Q�6(   65   5      5      .��5      � -   5         �Name$
 Grid��5      #��[ 6    � RowDefinitions 5   
   �� 5      $
��171* �6   �� 5      $��0* �6   �� 5      $
��263* �6   .+�5   
    � -   5          �Name$ frMain��5        � NavigationUIVisibility$ Visible=�6    $��3��6A   �� 5   
   -   5        $��btnToPage01��5   M   #��[  6   $��	To page 1��69   $
��Left=�5      $��316,224,0,11q�6,   $��28��5      $��79��6   $��2��6(   �� 5   
   -   5        $��btnToPage02��5   N   #���   6   $��	To page 2��6:   $
��Left=�5      $��400,224,0,11q�6,   $��79��6B   $��2��6M   5       5"      