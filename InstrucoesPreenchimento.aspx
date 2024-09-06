<%@ Page language="c#" Inherits="Ajuda.InstrucoesPreenchimento" CodeFile="InstrucoesPreenchimento.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Ilitera.NET</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="scripts/style.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0">
		<form id="CadastroEPI" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="750" align="center" border="0">
				<TR>
					<TD align="center">
						<P><SPAN lang="PT" style="FONT-SIZE: 10pt; FONT-FAMILY: Arial;"><FONT face="Verdana" color="#003300" size="1"><STRONG><U><BR>
											INSTRU��ES DE PREENCHIMENTO</U></STRONG></FONT></SPAN><BR>
											<BR>
						</P>
						<TABLE class="normalFont" id="Table2" borderColor="silver" cellSpacing="0" cellPadding="2"
							width="750" border="1">
							<TR>
								<TD align="center" width="100" style="height: 18px"><SPAN lang="PT" style="FONT-SIZE: 10pt; FONT-FAMILY: Arial;"><STRONG><FONT face="Verdana" size="1">CAMPO</FONT></STRONG></SPAN></TD>
								<TD align="center" width="200" style="height: 18px"><SPAN lang="PT" style="FONT-SIZE: 10pt; FONT-FAMILY: Arial;"><STRONG><FONT face="Verdana" size="1">DESCRI��O</FONT></STRONG></SPAN></TD>
								<TD align="center" width="450" style="height: 18px"><SPAN lang="PT" style="FONT-SIZE: 10pt; FONT-FAMILY: Arial;"><STRONG><FONT face="Verdana" size="1">INSTRU��O 
												DE PREENCHIMENTO</FONT></STRONG></SPAN></TD>
							</TR>
							<TR>
								<TD align="center" width="100"></TD>
								<TD align="center" width="650" colSpan="2"><B><SPAN lang="PT" style="FONT-SIZE: 10pt; FONT-FAMILY: Arial;"><FONT size="1"><FONT face="Verdana">SE��O 
													I - <B><SPAN lang="PT">SE��O DE DADOS ADMINISTRATIVOS</SPAN></B></FONT></FONT></SPAN></B></TD>
							</TR>
							<TR>
								<TD align="center" width="100">1</TD>
								<TD align="center" width="200"><SPAN lang="PT" style="FONT-SIZE: 10pt; FONT-FAMILY: Arial;"><SPAN lang="PT" style="FONT-SIZE: 10pt; FONT-FAMILY: Verdana;"><FONT size="1">CNPJ 
												do Domic�lio Tribut�rio/CEI</FONT></SPAN></SPAN></TD>
								<TD align="left" width="450">
									<P class="MsoNormal"><FONT size="1"><SPAN lang="PT">CNPJ relativo ao estabelecimento 
												escolhido como domic�lio tribut�rio, nos termos do art. 127 do CTN, no formato 
												XXXXXXXX/XXXX-XX; ou </SPAN><SPAN lang="PT">Matr�cula no Cadastro Espec�fico do 
												INSS (Matr�cula CEI) relativa � obra realizada por Contribuinte Individual ou 
												ao estabelecimento escolhido como domic�lio tribut�rio que n�o possua CNPJ, no 
												formato XX.XXX.XXXXX/XX, ambos compostos por<SPAN style="COLOR: red"> </SPAN>caracteres 
												num�ricos.</SPAN></FONT></P>
								</TD>
							</TR>
							<TR>
								<TD align="center" width="100">2</TD>
								<TD align="center" width="200"><SPAN lang="PT" style="FONT-SIZE: 6pt; FONT-FAMILY: Verdana;"><FONT size="1">Nome 
											Empresarial</FONT></SPAN></TD>
								<TD align="left" width="450"><SPAN lang="PT" style="FONT-SIZE: 7pt; FONT-FAMILY: Verdana;"><FONT size="1">At� 
											40 (quarenta) caracteres alfanum�ricos.</FONT></SPAN></TD>
							</TR>
							<TR>
								<TD align="center" width="100">3</TD>
								<TD align="center" width="200"><SPAN lang="PT" style="FONT-SIZE: 7pt; FONT-FAMILY: Verdana;"><FONT size="1">CNAE</FONT></SPAN></TD>
								<TD align="left" width="450">
									<P class="MsoNormal"><FONT size="1"><SPAN lang="PT">Classifica��o Nacional de Atividades 
												Econ�micas da<SPAN style="COLOR: red"> </SPAN>empresa, completo, com 7 (sete) 
												caracteres num�ricos, no formato XXXXXX-X<SPAN>, institu�do pelo IBGE </SPAN>atrav�s 
												da Resolu��o CONCLA n� 07, de 16/12/2002. </SPAN><SPAN lang="PT" style="FONT-SIZE: 7pt; FONT-FAMILY: Verdana;">
												A tabela de c�digos CNAE-Fiscal pode ser consultada na Internet, no site <SPAN style="COLOR: blue">
													<A href="http://www.cnae.ibge.gov.br/"><SPAN style="TEXT-DECORATION: none;">
															www.cnae.ibge.gov.br</SPAN></A></SPAN>.</SPAN></FONT></P>
								</TD>
							</TR>
							<TR>
								<TD align="center" width="100">4</TD>
								<TD align="center" width="200"><SPAN lang="PT" style="FONT-SIZE: 7pt; FONT-FAMILY: Verdana;"><FONT size="1">Nome 
											do Trabalhador</FONT></SPAN></TD>
								<TD align="left" width="450"><SPAN lang="PT" style="FONT-SIZE: 7pt; FONT-FAMILY: Verdana;"><FONT size="1">At� 
											40 (quarenta) caracteres alfab�ticos.</FONT></SPAN></TD>
							</TR>
							<TR>
								<TD align="center" width="100">5</TD>
								<TD align="center" width="200"><SPAN lang="PT" style="FONT-SIZE: 7pt; FONT-FAMILY: Verdana;"><FONT size="1">BR/PDH</FONT></SPAN></TD>
								<TD align="left" width="450">
									<P class="MsoNormal"><SPAN lang="PT">BR � Benefici�rio Reabilitado; PDH � Portador de 
											Defici�ncia Habilitado; NA � N�o Aplic�vel. </SPAN><SPAN lang="PT">Preencher 
											com base no art. 93, da Lei n� 8.213, de 1991, que estabelece a obrigatoriedade 
											do preenchimento dos cargos de empresas com 100 (cem) ou mais empregados com 
											benefici�rios reabilitados ou pessoas portadoras de defici�ncia, habilitadas, 
											na seguinte propor��o:
										</SPAN></P>
									<P class="MsoBodyText"><SPAN lang="PT" style="FONT-SIZE: 7pt; FONT-FAMILY: Verdana"><FONT size="1">I 
												- at� 200 
												empregados................................................................2%;<BR>
											</FONT></SPAN><SPAN lang="PT" style="FONT-SIZE: 7pt; FONT-FAMILY: Verdana"><FONT size="1">
												II - de 201 a 
												500.........................................................................3%;<BR>
											</FONT></SPAN><FONT size="1"><SPAN lang="PT">III - de 501 a 
												1.000.....................................................................4%;<BR>
											</SPAN><SPAN lang="PT">IV - de 1.001 em diante. 
												.............................................................5%.</SPAN></FONT></P>
								</TD>
							</TR>
							<TR>
								<TD align="center" width="100">6</TD>
								<TD align="center" width="200"><SPAN lang="PT" style="FONT-SIZE: 7pt; FONT-FAMILY: Verdana;"><FONT size="1">NIT</FONT></SPAN></TD>
								<TD align="left" width="450">
									<P class="MsoNormal"><FONT size="1"><SPAN lang="PT">N�mero de Identifica��o do Trabalhador 
												com 11 (onze) caracteres num�ricos, no formato XXX.XXXXX.XX-X. </SPAN><SPAN lang="PT">
												O NIT corresponde ao n�mero do PIS/PASEP/CI sendo que, no caso de Contribuinte 
												Individual (CI), pode ser utilizado o n�mero de inscri��o no Sistema �nico de 
												Sa�de (SUS) ou na Previd�ncia Social.</SPAN></FONT></P>
								</TD>
							</TR>
							<TR>
								<TD align="center" width="100">7</TD>
								<TD align="center" width="200"><SPAN lang="PT" style="FONT-SIZE: 7pt; FONT-FAMILY: Verdana;"><FONT size="1">Data 
											do Nascimento</FONT></SPAN></TD>
								<TD align="left" width="450"><SPAN lang="PT" style="FONT-SIZE: 7pt; FONT-FAMILY: Verdana;"><FONT size="1">No 
											formato DD/MM/AAAA.</FONT></SPAN></TD>
							</TR>
							<TR>
								<TD align="center" width="100">8</TD>
								<TD align="center" width="200"><SPAN lang="PT" style="FONT-SIZE: 7pt; FONT-FAMILY: Verdana;"><FONT size="1">Sexo 
											(F/M)</FONT></SPAN></TD>
								<TD align="left" width="450"><SPAN lang="PT" style="FONT-SIZE: 7pt; FONT-FAMILY: Verdana;"><FONT size="1">F 
											� Feminino; M � Masculino.</FONT></SPAN></TD>
							</TR>
							<TR>
								<TD align="center" width="100">9</TD>
								<TD align="center" width="200"><SPAN lang="PT" style="FONT-SIZE: 7pt; FONT-FAMILY: Verdana;"><FONT size="1">CTPS 
											(N�, S�rie e UF)</FONT></SPAN></TD>
								<TD align="left" width="450"><SPAN lang="PT" style="FONT-SIZE: 7pt; FONT-FAMILY: Verdana;"><FONT size="1">N�mero, 
											com 7 (sete) caracteres num�ricos, S�rie, com 5 (cinco) caracteres num�ricos e 
											UF, com 2 (dois) caracteres alfab�ticos, da Carteira de Trabalho e Previd�ncia 
											Social.</FONT></SPAN></TD>
							</TR>
							<TR>
								<TD align="center" width="100">10</TD>
								<TD align="center" width="200">Data de Admiss�o</TD>
								<TD align="left" width="450">No formato DD/MM/AAAA.</TD>
							</TR>
							<TR>
								<TD align="center" width="100">11</TD>
								<TD align="center" width="200">Regime de Revezamento</TD>
								<TD align="left" width="450">Regime de Revezamento de trabalho, para trabalhos em 
									turnos ou escala, especificando tempo trabalhado e tempo de descanso, com at� 
									15 (quinze) caracteres alfanum�ricos. Exemplo: 24 x 72 horas; 14 x 21 dias; 2 x 
									1 meses.</TD>
							</TR>
							<TR>
								<TD style="HEIGHT: 17px" align="center" width="100">12</TD>
								<TD style="HEIGHT: 17px" align="center" width="200">CAT REGISTRADA</TD>
								<TD style="HEIGHT: 17px" align="left" width="450">Informa��es sobre as Comunica��es 
									de Acidente do Trabalho registradas pela empresa na Previd�ncia Social, nos 
									termos do art. 22 da Lei n� 8.213, de 1991, do art. 169 da CLT, do art. 336 do 
									RPS, aprovado pelo Dec. n� 3.048, de 1999, do item 7.4.8, al�nea �a� da NR-07 
									do MTE e dos itens 4.3.1 e 6.1.2 do Anexo 13-A da NR-15 do MTE, disciplinado 
									pela Portaria MPAS n� 5.051, de 1999, que aprova o Manual de Instru��es para 
									Preenchimento da CAT.</TD>
							</TR>
							<TR>
								<TD align="center" width="100">12.1</TD>
								<TD align="center" width="200">Data do Registro</TD>
								<TD align="left" width="450">No formato DD/MM/AAAA.</TD>
							</TR>
							<TR>
								<TD align="center" width="100">12.2</TD>
								<TD align="center" width="200">N�mero da CAT</TD>
								<TD align="left" width="450">Com 13 (treze) caracteres num�ricos, com formato 
									XXXXXXXXXX-X/XX. Os dois �ltimos caracteres correspondem a um n�mero seq�encial 
									relativo ao mesmo acidente, identificado por NIT, CNPJ e data do acidente.</TD>
							</TR>
							<TR>
								<TD align="center" width="100">13</TD>
								<TD align="center" width="200">LOTA��O E ATRIBUI��O</TD>
								<TD align="left" width="450">Informa��es sobre o hist�rico de lota��o e atribui��es 
									do trabalhador, por per�odo. A altera��o de qualquer um dos campos - 13.2 a 
									13.7 - implica, obrigatoriamente, a cria��o de nova linha, com discrimina��o do 
									per�odo, repetindo as informa��es que n�o foram alteradas.</TD>
							</TR>
							<TR>
								<TD align="center" width="100">13.1</TD>
								<TD align="center" width="200">Per�odo</TD>
								<TD align="left" width="450">Data de in�cio e data de fim do per�odo, ambas no 
									formato DD/MM/AAAA. No caso de trabalhador ativo, a data de fim do �ltimo 
									per�odo n�o dever� ser preenchida.</TD>
							</TR>
							<TR>
								<TD align="center" width="100">13.2</TD>
								<TD align="center" width="200">CNPJ/CEI</TD>
								<TD align="left" width="450">Local onde efetivamente o trabalhador exerce suas 
									atividades. Dever� ser informado o CNPJ do estabelecimento de lota��o do 
									trabalhador ou da empresa tomadora de servi�os, no formato XXXXXXXX/XXXX-XX; ou 
									Matr�cula CEI da obra ou do estabelecimento que n�o possua CNPJ, no formato 
									XX.XXX.XXXXX/XX, ambos compostos por caracteres num�ricos.</TD>
							</TR>
							<TR>
								<TD align="center" width="100">13.3</TD>
								<TD align="center" width="200">Setor</TD>
								<TD align="left" width="450">Lugar administrativo na estrutura organizacional da 
									empresa, onde o trabalhador exerce suas atividades laborais, com at� 15 
									(quinze) caracteres alfanum�ricos.</TD>
							</TR>
							<TR>
								<TD align="center" width="100">13.4</TD>
								<TD align="center" width="200">Cargo</TD>
								<TD align="left" width="450">Cargo do trabalhador, constante na CTPS, se empregado 
									ou trabalhador avulso, ou constante no Recibo de Produ��o e Livro de Matr�cula, 
									se cooperado, com at� 30 (trinta) caracteres alfanum�ricos.</TD>
							</TR>
							<TR>
								<TD align="center" width="100">13.5</TD>
								<TD align="center" width="200">Fun��o</TD>
								<TD align="left" width="450">Lugar administrativo na estrutura organizacional da 
									empresa, onde o trabalhador tenha atribui��o de comando, chefia, coordena��o, 
									supervis�o ou ger�ncia. Quando inexistente a fun��o, preencher com NA � N�o 
									Aplic�vel, com at� 30 (trinta) caracteres alfanum�ricos.</TD>
							</TR>
							<TR>
								<TD align="center" width="100">13.6</TD>
								<TD align="center" width="200">CBO</TD>
								<TD align="left" width="450">Classifica��o Brasileira de Ocupa��o vigente � �poca, 
									com 6 (seis) caracteres num�ricos:<BR>
									1- No caso de utiliza��o da tabela CBO relativa a 1994, utilizar a CBO completa 
									com 5 (cinco) caracteres, completando com �0� (zero) a primeira posi��o;<BR>
									2- No caso de utiliza��o da tabela CBO relativa a 2002, utilizar a CBO completa 
									com 6 (seis) caracteres.<BR>
									<BR>
									<U>Alternativamente</U>, pode ser utilizada a CBO, com 5 (cinco) caracteres 
									num�ricos, conforme Manual da GFIP para usu�rios do SEFIP, publicado por 
									Instru��o Normativa da Diretoria Colegiada do INSS:<BR>
									1- No caso de utiliza��o da tabela CBO relativa a 1994, utilizar a CBO completa 
									com 5 (cinco) caracteres;<BR>
									2- No caso de utiliza��o da tabela CBO relativa a 2002, utilizar a fam�lia do 
									CBO com 4 (quatro) caracteres, completando com �0� (zero) a primeira posi��o.<BR>
									<BR>
									A tabela de CBO pode ser consultada na Internet, no site <A href="http://www.mtecbo.gov.br">
										www.mtecbo.gov.br</A>.<BR>
									<BR>
									OBS: Ap�s a altera��o da GFIP, somente ser� aceita a CBO completa, com 6 (seis) 
									caracteres num�ricos, conforme a nova tabela CBO relativa a 2002.</TD>
							</TR>
							<TR>
								<TD align="center" width="100">13.7</TD>
								<TD align="center" width="200">C�digo Ocorr�ncia da GFIP</TD>
								<TD align="left" width="450">C�digo Ocorr�ncia da GFIP para o trabalhador, com 2 
									(dois) caracteres num�ricos, conforme Manual da GFIP para usu�rios do SEFIP, 
									publicado por Instru��o Normativa da Diretoria Colegiada do INSS.</TD>
							</TR>
							<TR>
								<TD align="center" width="100">14</TD>
								<TD align="center" width="200">PROFISSIOGRAFIA</TD>
								<TD align="left" width="450">Informa��es sobre a profissiografia do trabalhador, 
									por per�odo. A altera��o do campo 14.2 implica, obrigatoriamente, a cria��o de 
									nova linha, com discrimina��o do per�odo.</TD>
							</TR>
							<TR>
								<TD align="center" width="100">14.1</TD>
								<TD align="center" width="200">Per�odo</TD>
								<TD align="left" width="450">Data de in�cio e data de fim do per�odo, ambas no 
									formato DD/MM/AAAA. No caso de trabalhador ativo, a data de fim do �ltimo 
									per�odo n�o dever� ser preenchida.</TD>
							</TR>
							<TR>
								<TD align="center" width="100">14.2</TD>
								<TD align="center" width="200">Descri��o das Atividades</TD>
								<TD align="left" width="450">Descri��o das atividades, f�sicas ou mentais, 
									realizadas pelo trabalhador, por for�a do poder de comando a que se submete, 
									com at� 400 (quatrocentos) caracteres alfanum�ricos. As atividades dever�o ser 
									descritas com exatid�o, e de forma sucinta, com a utiliza��o de verbos no 
									infinitivo impessoal.</TD>
							</TR>
							<TR>
								<TD align="center" width="100"></TD>
								<TD align="center" width="650" colSpan="2"><STRONG>SE��O II - SE��O DE REGISTROS 
										AMBIENTAIS</STRONG></TD>
							</TR>
							<TR>
								<TD align="center" width="100">15</TD>
								<TD align="center" width="200">EXPOSI��O A FATORES DE RISCOS</TD>
								<TD align="left" width="450">Informa��es sobre a exposi��o do trabalhador a fatores 
									de riscos ambientais, por per�odo, ainda que estejam neutralizados, atenuados 
									ou exista prote��o eficaz.<BR>
									Facultativamente, tamb�m poder�o ser indicados os fatores de riscos ergon�micos 
									e mec�nicos.<BR>
									A altera��o de qualquer um dos campos - 15.2 a 15.8 - implica, 
									obrigatoriamente, a cria��o de nova linha, com discrimina��o do per�odo, 
									repetindo as informa��es que n�o foram alteradas.<BR>
									OBS.: Ap�s a implanta��o da migra��o dos dados do PPP em meio magn�tico pela 
									Previd�ncia Social, as informa��es relativas aos fatores de riscos ergon�micos 
									e mec�nicos passar�o a ser obrigat�rias.</TD>
							</TR>
							<TR>
								<TD align="center" width="100">15.1</TD>
								<TD align="center" width="200">Per�odo</TD>
								<TD align="left" width="450">Data de in�cio e data de fim do per�odo, ambas no 
									formato DD/MM/AAAA. No caso de trabalhador ativo, a data de fim do �ltimo 
									per�odo n�o dever� ser preenchida.</TD>
							</TR>
							<TR>
								<TD align="center" width="100">15.2</TD>
								<TD align="center" width="200">Tipo</TD>
								<TD align="left" width="450">F � F�sico; Q � Qu�mico; B � Biol�gico; E � 
									Ergon�mico/Psicossocial, M � Mec�nico/de Acidente, conforme classifica��o 
									adotada pelo Minist�rio da Sa�de, em �Doen�as Relacionadas ao Trabalho: Manual 
									de Procedimentos para os Servi�os de Sa�de�, de 2001.<BR>
									A indica��o do Tipo �E� e �M� � facultativa.<BR>
									O que determina a associa��o de agentes � a superposi��o de per�odos com 
									fatores de risco diferentes.</TD>
							</TR>
							<TR>
								<TD align="center" width="100">15.3</TD>
								<TD align="center" width="200">Fator de Risco</TD>
								<TD align="left" width="450">Descri��o do fator de risco, com at� 40 (quarenta) 
									caracteres alfanum�ricos. Em se tratando do Tipo �Q�, dever� ser informado o 
									nome da subst�ncia ativa, n�o sendo aceitas cita��es de nomes comerciais.</TD>
							</TR>
							<TR>
								<TD align="center" width="100">15.4</TD>
								<TD align="center" width="200">Intensidade / Concentra��o</TD>
								<TD align="left" width="450">Intensidade ou Concentra��o, dependendo do tipo de 
									agente, com at� 15 (quinze) caracteres alfanum�ricos. Caso o fator de risco n�o 
									seja pass�vel de mensura��o, preencher com NA � N�o Aplic�vel.</TD>
							</TR>
							<TR>
								<TD align="center" width="100">15.5</TD>
								<TD align="center" width="200">T�cnica Utilizada</TD>
								<TD align="left" width="450">T�cnica utilizada para apura��o do item 15.4, com at� 
									40 (quarenta) caracteres alfanum�ricos. Caso o fator de risco n�o seja pass�vel 
									de mensura��o, preencher com NA � N�o Aplic�vel.</TD>
							</TR>
							<TR>
								<TD align="center" width="100">15.6</TD>
								<TD align="center" width="200">EPC Eficaz (S/N)</TD>
								<TD align="left" width="450">S � Sim; N � N�o, considerando se houve ou n�o a 
									elimina��o ou a neutraliza��o, com base no informado nos itens 15.2 a 15.5, 
									assegurada as condi��es de funcionamento do EPC ao longo do tempo, conforme 
									especifica��o t�cnica do fabricante e respectivo plano de manuten��o.</TD>
							</TR>
							<TR>
								<TD align="center" width="100">15.7</TD>
								<TD align="center" width="200">EPI Eficaz (S/N)</TD>
								<TD align="left" width="450">S � Sim; N � N�o, considerando se houve ou n�o a 
									atenua��o, com base no informado nos itens 15.2 a 15.5, observado o disposto na 
									NR-06 do MTE, assegurada a observ�ncia:<BR>
									1- da hierarquia estabelecida no item 9.3.5.4 da NR-09 do MTE (medidas de 
									prote��o coletiva, medidas de car�ter administrativo ou de organiza��o do 
									trabalho e utiliza��o de EPI, nesta ordem, admitindo-se a utiliza��o de EPI 
									somente em situa��es de inviabilidade t�cnica, insufici�ncia ou interinidade � 
									implementa��o do EPC, ou ainda em car�ter complementar ou emergencial);<BR>
									2- das condi��es de funcionamento do EPI ao longo do tempo, conforme 
									especifica��o t�cnica do fabricante ajustada �s condi��es de campo;<BR>
									3- do prazo de validade, conforme Certificado de Aprova��o do MTE;<BR>
									4- da periodicidade de troca definida pelos programas ambientais, devendo esta 
									ser comprovada mediante recibo; e<BR>
									5- dos meios de higieniza��o.</TD>
							</TR>
							<TR>
								<TD align="center" width="100">15.8</TD>
								<TD align="center" width="200">C.A. EPI</TD>
								<TD align="left" width="450">N�mero do Certificado de Aprova��o do MTE para o 
									Equipamento de Prote��o Individual referido no campo 154.7, com 5 (cinco) 
									caracteres num�ricos. Caso n�o seja utilizado EPI, preencher com NA � N�o 
									Aplic�vel.</TD>
							</TR>
							<TR>
								<TD align="center" width="100">16</TD>
								<TD align="center" width="200">RESPONS�VEL PELOS REGISTROS AMBIENTAIS</TD>
								<TD align="left" width="450">Informa��es sobre os respons�veis pelos registros 
									ambientais, por per�odo.</TD>
							</TR>
							<TR>
								<TD align="center" width="100">16.1</TD>
								<TD align="center" width="200">Per�odo</TD>
								<TD align="left" width="450">Data de in�cio e data de fim do per�odo, ambas no 
									formato DD/MM/AAAA. No caso de trabalhador ativo sem altera��o do respons�vel, 
									a data de fim do �ltimo per�odo n�o dever� ser preenchida.</TD>
							</TR>
							<TR>
								<TD align="center" width="100">16.2</TD>
								<TD align="center" width="200">NIT</TD>
								<TD align="left" width="450">N�mero de Identifica��o do Trabalhador com 11 (onze) 
									caracteres num�ricos, no formato XXX.XXXXX.XX-X.<BR>
									O NIT corresponde ao n�mero do PIS/PASEP/CI sendo que, no caso de Contribuinte 
									Individual (CI), pode ser utilizado o n�mero de inscri��o no Sistema �nico de 
									Sa�de (SUS) ou na Previd�ncia Social.</TD>
							</TR>
							<TR>
								<TD align="center" width="100">16.3</TD>
								<TD align="center" width="200">Registro Conselho de Classe</TD>
								<TD align="left" width="450">N�mero do registro profissional no Conselho de Classe, 
									com 9 (nove) caracteres alfanum�ricos, no formato XXXXXX-X/XX ou XXXXXXX/XX.<BR>
									A parte �-X� corresponde � D � Definitivo ou P � Provis�rio.<BR>
									A parte �/XX� deve ser preenchida com a UF, com 2 (dois) caracteres 
									alfab�ticos.<BR>
									A parte num�rica dever� ser completada com zeros � esquerda.</TD>
							</TR>
							<TR>
								<TD align="center" width="100">16.4</TD>
								<TD align="center" width="200">Nome do Profissional Legalmente Habilitado</TD>
								<TD align="left" width="450">At� 40 (quarenta) caracteres alfab�ticos.</TD>
							</TR>
							<TR>
								<TD align="center" width="100"></TD>
								<TD align="center" width="650" colSpan="2"><STRONG>SE��O III -&nbsp;SE��O DE RESULTADOS 
										DE MONITORA��O BIOL�GICA</STRONG></TD>
							</TR>
							<TR>
								<TD align="center" width="100">17</TD>
								<TD align="center" width="200">EXAMES M�DICOS CL�NICOS E COMPLEMENTARES</TD>
								<TD align="left" width="450">Informa��es sobre os exames m�dicos obrigat�rios, 
									cl�nicos e complementares, realizados para o trabalhador, constantes nos 
									Quadros I e II, da NR-07 do MTE.</TD>
							</TR>
							<TR>
								<TD align="center" width="100">17.1</TD>
								<TD align="center" width="200">Data</TD>
								<TD align="left" width="450">No formato DD/MM/AAAA.</TD>
							</TR>
							<TR>
								<TD align="center" width="100">17.2</TD>
								<TD align="center" width="200">Tipo</TD>
								<TD align="left" width="450">A � Admissional; P � Peri�dico; R � Retorno ao 
									Trabalho; M � Mudan�a de Fun��o; D � Demissional.</TD>
							</TR>
							<TR>
								<TD align="center" width="100">17.3</TD>
								<TD align="center" width="200">Natureza</TD>
								<TD align="left" width="450">Natureza do exame realizado, com at� 50 (cinq�enta) 
									caracteres alfanum�ricos.<BR>
									No caso dos exames relacionados no Quadro I da NR-07, do MTE, dever� ser 
									especificada a an�lise realizada, al�m do material biol�gico coletado.</TD>
							</TR>
							<TR>
								<TD align="center" width="100">17.4</TD>
								<TD align="center" width="200">Exame (R/S)</TD>
								<TD align="left" width="450">R � Referencial; S � Seq�encial.</TD>
							</TR>
							<TR>
								<TD align="center" width="100">17.5</TD>
								<TD align="center" width="200">Indica��o de Resultados</TD>
								<TD align="left" width="450">Preencher Normal ou Alterado. S� deve ser preenchido 
									Est�vel ou Agravamento no caso de Alterado em exame Seq�encial. S� deve ser 
									preenchido Ocupacional ou N�o Ocupacional no caso de Agravamento.<BR>
									OBS: No caso de Natureza do Exame �Audiometria�, a altera��o unilateral poder� 
									ser classificada como ocupacional, apesar de a maioria das altera��es 
									ocupacionais serem constatadas bilateralmente.</TD>
							</TR>
							<TR>
								<TD align="center" width="100">18</TD>
								<TD align="center" width="200">RESPONS�VEL PELA MONITORA��O BIOL�GICA</TD>
								<TD align="left" width="450">Informa��es sobre os respons�veis pela monitora��o 
									biol�gica, por per�odo.</TD>
							</TR>
							<TR>
								<TD align="center" width="100">18.1</TD>
								<TD align="center" width="200">Per�odo</TD>
								<TD align="left" width="450">Data de in�cio e data de fim do per�odo, ambas no 
									formato DD/MM/AAAA. No caso de trabalhador ativo sem altera��o do respons�vel, 
									a data de fim do �ltimo per�odo n�o dever� ser preenchida.</TD>
							</TR>
							<TR>
								<TD align="center" width="100">18.2</TD>
								<TD align="center" width="200">NIT</TD>
								<TD align="left" width="450">N�mero de Identifica��o do Trabalhador com 11 (onze) 
									caracteres num�ricos, no formato XXX.XXXXX.XX-X.<BR>
									O NIT corresponde ao n�mero do PIS/PASEP/CI sendo que, no caso de Contribuinte 
									Individual (CI), pode ser utilizado o n�mero de inscri��o no Sistema �nico de 
									Sa�de (SUS) ou na Previd�ncia Social.</TD>
							</TR>
							<TR>
								<TD align="center" width="100">18.3</TD>
								<TD align="center" width="200">Registro Conselho de Classe</TD>
								<TD align="left" width="450">N�mero do registro profissional no Conselho de Classe, 
									com 9 (nove) caracteres alfanum�ricos, no formato XXXXXX-X/XX ou XXXXXXX/XX.<BR>
									A parte �-X� corresponde � D � Definitivo ou P � Provis�rio.<BR>
									A parte �/XX� deve ser preenchida com a UF, com 2 (dois) caracteres 
									alfab�ticos.<BR>
									A parte num�rica dever� ser completada com zeros � esquerda.</TD>
							</TR>
							<TR>
								<TD align="center" width="100">18.4</TD>
								<TD align="center" width="200">Nome do Profissional Legalmente Habilitado</TD>
								<TD align="left" width="450">At� 40 (quarenta) caracteres alfab�ticos.</TD>
							</TR>
							<TR>
								<TD align="center" width="100">19</TD>
								<TD align="center" width="200">Data de Emiss�o do PPP</TD>
								<TD align="left" width="450">Data em que o PPP � impresso e assinado pelos 
									respons�veis, no formato DD/MM/AAAA.</TD>
							</TR>
							<TR>
								<TD align="center" width="100">20</TD>
								<TD align="center" width="200">REPRESENTANTE LEGAL DA EMPRESA</TD>
								<TD align="left" width="450">Informa��es sobre o Representante Legal da empresa, 
									com poderes espec�ficos outorgados por procura��o.</TD>
							</TR>
							<TR>
								<TD align="center" width="100">20.1</TD>
								<TD align="center" width="200">NIT</TD>
								<TD align="left" width="450">N�mero de Identifica��o do Trabalhador com 11 (onze) 
									caracteres num�ricos, no formato XXX.XXXXX.XX-X.<BR>
									O NIT corresponde ao n�mero do PIS/PASEP/CI sendo que, no caso de contribuinte 
									individual (CI), pode ser utilizado o n�mero de inscri��o no Sistema �nico de 
									Sa�de (SUS) ou na Previd�ncia Social.</TD>
							</TR>
							<TR>
								<TD align="center" width="100">20.2</TD>
								<TD align="center" width="200">Nome</TD>
								<TD align="left" width="450">At� 40 caracteres alfab�ticos.</TD>
							</TR>
							<TR>
								<TD align="center" width="100"></TD>
								<TD align="center" width="200">Carimbo e Assinatura</TD>
								<TD align="left" width="450">Carimbo da Empresa e Assinatura do Representante 
									Legal.</TD>
							</TR>
							<TR>
								<TD align="center" width="100"></TD>
								<TD align="center" width="650" colSpan="2"><STRONG>OBSERVA��ES</STRONG></TD>
							</TR>
							<TR>
								<TD align="center" width="100"></TD>
								<TD align="center" width="650" colSpan="2">Devem ser inclu�das neste campo, 
									informa��es necess�rias � an�lise do PPP, bem como facilitadoras do 
									requerimento do benef�cio, como por exemplo, esclarecimento sobre altera��o de 
									raz�o social da empresa, no caso de sucessora ou indicador de empresa 
									pertencente a grupo econ�mico.<BR>
									<BR>
									OBS: � facultada a inclus�o de informa��es complementares ou adicionais ao PPP.
								</TD>
							</TR>
						</TABLE><br>
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
