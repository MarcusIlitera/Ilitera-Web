function centerWin(url, width, height, name){ // Pop no centro da Tela
	var centerX=(screen.width-width)/2; //Need to subtract the window width/height so that it appears centered. If you didn't, the top left corner of the window would be centered.
	var centerY=(screen.height-height)/2;
	return window.open(url,name,"toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbars=no,resizable=no,copyhistory=no,width=" + width + ", height=" + height + ",top=" + centerY + ", left=" + centerX);
}

function validar_data(dia, mes, ano, type)
{
	if (dia <= 31 && mes <=12 && ano >= 1000)
	{
		if (mes == 2)
		{
			if ((dia > 0 ) && (dia <= 29))
			{
				if (dia == 29)
				{
					if ((ano % 4) == 0)
					{
						return true;
					}
					else
					{
						window.alert("O dia para a sua " + type + " não existe, certifique-se de que digitou corretamente!");
						return false;
					}
				}
				else
				{
					return true;
				}
			}
			else
			{
				window.alert("O dia para a sua " + type + " não existe, certifique-se de que digitou corretamente!");
				return false;
			}
		}
		else
		{
			if ((mes == 4)||(mes == 6)||(mes == 9)||(mes == 11))
			{
				if ((dia > 0 ) && (dia <= 30))
				{
					return true;
				}
				else
				{
					window.alert("O dia para a sua " + type + " não existe, certifique-se de que digitou corretamente!");
					return false;
				}
			}
			else
			{
				if ((mes == 1)||(mes == 3)||(mes == 5)||(mes ==7)||(mes == 8)||(mes == 10)||(mes == 12))
				{
					if ((dia > 0) && (dia <= 31))
					{
						return true;
					}
					else
					{
						window.alert("O dia para a sua " + type + " não existe, certifique-se de que digitou corretamente!");
						return false;
					}
				}
				else
				{
					window.alert("O mês para a sua " + type + " não existe, certifique-se de que digitou corretamente!");
					return false;
				}
			}
		}
	}
	else
	{
		window.alert("A sua " + type + " não é uma data válida! Lembre-se que a data deve seguir o padrão 'dd/mm/aaaa'!");
		return false;
	}
}

function isNull(str)
{
	return (str == null || str == "" || str.split(" ").length - 1 == str.length);
}

function ChecarTAB()
{
	VerifiqueTAB=true;
}

function PararTAB()
{
	VerifiqueTAB=false;
}

function addItem(janela, tipo)
{
	if (tipo == "Todos")
	{
		top.window.allwin[top.window.allwin.length] = janela;
		top.window.allwinfunc[top.window.allwinfunc.length] = janela;
	}
	else 
	{
		if (tipo == "Empresa")
			top.window.allwin[top.window.allwin.length] = janela;
	}
}

function addItemPop(janela)
{
	window.opener.top.window.allwin[window.opener.top.window.allwin.length] = janela;
	window.opener.top.window.allwinfunc[window.opener.top.window.allwinfunc.length] = janela;
}