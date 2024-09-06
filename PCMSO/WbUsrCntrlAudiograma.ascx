<%@ Control Language="c#"  AutoEventWireup="True" Inherits="Ilitera.Net.PCMSO.WbUsrCntrlAudiograma" CodeBehind="WbUsrCntrlAudiograma.ascx.cs" %>


<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>


				     <eo:TabStrip ID="TabStrip1" runat="server" ControlSkinID="None" 
                            MultiPageID="MultiPage1">
                            <topgroup>
                                <Items>
                                    <eo:TabItem Text-Html="Dados Audiograma">
                                    </eo:TabItem>
                                    <eo:TabItem Text-Html="Dados Complementares">
                                    </eo:TabItem>
                                </Items>
                            </topgroup>
                            <lookitems>
                                 <eo:TabItem ItemID="_Default"
                                    NormalStyle-CssText="font-family:'Ubuntu'; font-size: 12px; color: #B0ABAB; padding: 10px 10px; background: #F1F1F1; border-radius: 8px; cursor: hand; width: fit-content; margin-right: 1rem;"
                                    SelectedStyle-CssText="font-family:'Ubuntu'; font-size: 12px; color: #1C9489; font-weight: bold; padding: 10px 15px; background: #D9D9D9; border-radius: 8px; cursor: hand; width: fit-content; margin-right: 1rem;">
                                    <SubGroup OverlapDepth="8" ItemSpacing="5"
                                        Style-CssText="font-family:'Ubuntu'; font-size: 12px; color: #B0ABAB; padding: 10px 10px 10px 0px; border-radius: 8px; cursor: hand; width: fit-content;">
                                    </SubGroup>
                                </eo:TabItem>
                            </LookItems>
                        </eo:TabStrip>


                         <eo:MultiPage ID="MultiPage1" runat="server" Height="400" Width="1050">
                                <eo:PageView ID="Pageview1" runat="server" Width="1050">

									<div class="col-12 mb-3">
										<div class="row">
											<div class="col-md-3 gx-3 gy-2">
												<asp:RadioButtonList id="rblRefSeq" OnSelectedIndexChanged="rblRefSeq_SelectedIndexChanged" runat="server" CssClass="texto form-check-input bg-transparent border-0 ms-3" Width="250" RepeatDirection="Horizontal"
													CellSpacing="0" CellPadding="0" AutoPostBack="True">
													<asp:ListItem Value="0" Selected="True">Referencial</asp:ListItem>
													<asp:ListItem Value="1">Sequencial</asp:ListItem>
												</asp:RadioButtonList>
											</div>

											<div class="col-md-4 gx-3 gy-2">
												<asp:CheckBox id="ckbMascarado" OnCheckedChanged="ckbMascarado_CheckedChanged" runat="server" CssClass="texto form-check-input bg-transparent border-0" Text="Com mascaramento" AutoPostBack="True"></asp:CheckBox>
											</div>
										</div>
									</div>

									<div class="col-12 mb-3">
										<div class="row">
											<div class="col-12 gx-3 gy-2">
												<asp:label id="Label7" runat="server" CssClass="tituloLabel">Aéreos Atual</asp:label>
											</div>

											<div class="col-1 gx-3 gy-2 ms-3">
												<div class="row">
													<div class="col-1">
														<asp:CheckBox id="ckbMascAA025" runat="server" CssClass="normalFont" ToolTip="250 Hz Aéreo Atual Mascarado" Enabled="False"></asp:CheckBox>
													</div>

													<div class="col-11">
														<asp:label id="Label44" runat="server" CssClass="texto">250 Hz</asp:label>
													</div>

													<div class="col-12">
														<asp:DropDownList id="ddlAA025" runat="server" CssClass="texto form-select">
															<asp:ListItem Value="NE">NE</asp:ListItem>
															<asp:ListItem Value="-10">-10</asp:ListItem>
															<asp:ListItem Value="-5">-5</asp:ListItem>
															<asp:ListItem Value="0">0</asp:ListItem>
															<asp:ListItem Value="5">5</asp:ListItem>
															<asp:ListItem Value="10">10</asp:ListItem>
															<asp:ListItem Value="15">15</asp:ListItem>
															<asp:ListItem Value="20">20</asp:ListItem>
															<asp:ListItem Value="25">25</asp:ListItem>
															<asp:ListItem Value="30">30</asp:ListItem>
															<asp:ListItem Value="35">35</asp:ListItem>
															<asp:ListItem Value="40">40</asp:ListItem>
															<asp:ListItem Value="45">45</asp:ListItem>
															<asp:ListItem Value="50">50</asp:ListItem>
															<asp:ListItem Value="55">55</asp:ListItem>
															<asp:ListItem Value="60">60</asp:ListItem>
															<asp:ListItem Value="65">65</asp:ListItem>
															<asp:ListItem Value="70">70</asp:ListItem>
															<asp:ListItem Value="75">75</asp:ListItem>
															<asp:ListItem Value="80">80</asp:ListItem>
															<asp:ListItem Value="85">85</asp:ListItem>
															<asp:ListItem Value="90">90</asp:ListItem>
															<asp:ListItem Value="95">95</asp:ListItem>
															<asp:ListItem Value="100">100</asp:ListItem>
															<asp:ListItem Value="105">105</asp:ListItem>
															<asp:ListItem Value="110">110</asp:ListItem>
															<asp:ListItem Value="115">115</asp:ListItem>
															<asp:ListItem Value="AR">AR</asp:ListItem>
														</asp:DropDownList>
													</div>
												</div>
											</div>

											<div class="col-1 gx-3 gy-2 ms-3">
												<div class="row">
													<div class="col-1">
														<asp:CheckBox id="ckbMascAA05" runat="server" CssClass="texto form-check-input bg-transparent border-0" ToolTip="500 Hz Aéreo Atual Mascarado" Enabled="False"></asp:CheckBox>
													</div>

													<div class="col-11">
														<asp:label id="Label45" runat="server" CssClass="texto">500 Hz</asp:label>
													</div>

													<div class="col-12">
														<asp:DropDownList id="ddlAA05" runat="server" CssClass="texto form-select">
															<asp:ListItem Value="NE">NE</asp:ListItem>
															<asp:ListItem Value="-10">-10</asp:ListItem>
															<asp:ListItem Value="-5">-5</asp:ListItem>
															<asp:ListItem Value="0">0</asp:ListItem>
															<asp:ListItem Value="5">5</asp:ListItem>
															<asp:ListItem Value="10">10</asp:ListItem>
															<asp:ListItem Value="15">15</asp:ListItem>
															<asp:ListItem Value="20">20</asp:ListItem>
															<asp:ListItem Value="25">25</asp:ListItem>
															<asp:ListItem Value="30">30</asp:ListItem>
															<asp:ListItem Value="35">35</asp:ListItem>
															<asp:ListItem Value="40">40</asp:ListItem>
															<asp:ListItem Value="45">45</asp:ListItem>
															<asp:ListItem Value="50">50</asp:ListItem>
															<asp:ListItem Value="55">55</asp:ListItem>
															<asp:ListItem Value="60">60</asp:ListItem>
															<asp:ListItem Value="65">65</asp:ListItem>
															<asp:ListItem Value="70">70</asp:ListItem>
															<asp:ListItem Value="75">75</asp:ListItem>
															<asp:ListItem Value="80">80</asp:ListItem>
															<asp:ListItem Value="85">85</asp:ListItem>
															<asp:ListItem Value="90">90</asp:ListItem>
															<asp:ListItem Value="95">95</asp:ListItem>
															<asp:ListItem Value="100">100</asp:ListItem>
															<asp:ListItem Value="105">105</asp:ListItem>
															<asp:ListItem Value="110">110</asp:ListItem>
															<asp:ListItem Value="115">115</asp:ListItem>
															<asp:ListItem Value="AR">AR</asp:ListItem>
														</asp:DropDownList>
													</div>
												</div>
											</div>

											<div class="col-1 gx-3 gy-2 ms-3">
												<div class="row">
													<div class="col-1">
														<asp:CheckBox id="ckbMascAA1" runat="server" CssClass="texto form-check-input bg-transparent border-0" ToolTip="1 KHz Aéreo Atual Mascarado" Enabled="False"></asp:CheckBox>
													</div>

													<div class="col-11">
														<asp:label id="Label46" runat="server" CssClass="texto">1 KHz</asp:label>
													</div>

													<div class="col-12">
														<asp:DropDownList id="ddlAA1" runat="server" CssClass="texto form-select">
															<asp:ListItem Value="NE">NE</asp:ListItem>
															<asp:ListItem Value="-10">-10</asp:ListItem>
															<asp:ListItem Value="-5">-5</asp:ListItem>
															<asp:ListItem Value="0">0</asp:ListItem>
															<asp:ListItem Value="5">5</asp:ListItem>
															<asp:ListItem Value="10">10</asp:ListItem>
															<asp:ListItem Value="15">15</asp:ListItem>
															<asp:ListItem Value="20">20</asp:ListItem>
															<asp:ListItem Value="25">25</asp:ListItem>
															<asp:ListItem Value="30">30</asp:ListItem>
															<asp:ListItem Value="35">35</asp:ListItem>
															<asp:ListItem Value="40">40</asp:ListItem>
															<asp:ListItem Value="45">45</asp:ListItem>
															<asp:ListItem Value="50">50</asp:ListItem>
															<asp:ListItem Value="55">55</asp:ListItem>
															<asp:ListItem Value="60">60</asp:ListItem>
															<asp:ListItem Value="65">65</asp:ListItem>
															<asp:ListItem Value="70">70</asp:ListItem>
															<asp:ListItem Value="75">75</asp:ListItem>
															<asp:ListItem Value="80">80</asp:ListItem>
															<asp:ListItem Value="85">85</asp:ListItem>
															<asp:ListItem Value="90">90</asp:ListItem>
															<asp:ListItem Value="95">95</asp:ListItem>
															<asp:ListItem Value="100">100</asp:ListItem>
															<asp:ListItem Value="105">105</asp:ListItem>
															<asp:ListItem Value="110">110</asp:ListItem>
															<asp:ListItem Value="115">115</asp:ListItem>
															<asp:ListItem Value="AR">AR</asp:ListItem>
														</asp:DropDownList>
													</div>
												</div>
											</div>

											<div class="col-1 gx-3 gy-2 ms-3">
												<div class="row">
													<div class="col-1">
														<asp:CheckBox id="ckbMascAA2" runat="server" CssClass="texto form-check-input bg-transparent border-0" ToolTip="2 KHz Aéreo Atual Mascarado" Enabled="False"></asp:CheckBox>
													</div>

													<div class="col-11">
														<asp:label id="Label47" runat="server" CssClass="texto">2 KHz</asp:label>
													</div>

													<div class="col-12">
														<asp:DropDownList id="ddlAA2" runat="server" CssClass="texto form-select">
															<asp:ListItem Value="NE">NE</asp:ListItem>
															<asp:ListItem Value="-10">-10</asp:ListItem>
															<asp:ListItem Value="-5">-5</asp:ListItem>
															<asp:ListItem Value="0">0</asp:ListItem>
															<asp:ListItem Value="5">5</asp:ListItem>
															<asp:ListItem Value="10">10</asp:ListItem>
															<asp:ListItem Value="15">15</asp:ListItem>
															<asp:ListItem Value="20">20</asp:ListItem>
															<asp:ListItem Value="25">25</asp:ListItem>
															<asp:ListItem Value="30">30</asp:ListItem>
															<asp:ListItem Value="35">35</asp:ListItem>
															<asp:ListItem Value="40">40</asp:ListItem>
															<asp:ListItem Value="45">45</asp:ListItem>
															<asp:ListItem Value="50">50</asp:ListItem>
															<asp:ListItem Value="55">55</asp:ListItem>
															<asp:ListItem Value="60">60</asp:ListItem>
															<asp:ListItem Value="65">65</asp:ListItem>
															<asp:ListItem Value="70">70</asp:ListItem>
															<asp:ListItem Value="75">75</asp:ListItem>
															<asp:ListItem Value="80">80</asp:ListItem>
															<asp:ListItem Value="85">85</asp:ListItem>
															<asp:ListItem Value="90">90</asp:ListItem>
															<asp:ListItem Value="95">95</asp:ListItem>
															<asp:ListItem Value="100">100</asp:ListItem>
															<asp:ListItem Value="105">105</asp:ListItem>
															<asp:ListItem Value="110">110</asp:ListItem>
															<asp:ListItem Value="115">115</asp:ListItem>
															<asp:ListItem Value="AR">AR</asp:ListItem>
														</asp:DropDownList>
													</div>
												</div>
											</div>

											<div class="col-1 gx-3 gy-2 ms-3">
												<div class="row">
													<div class="col-1">
														<asp:CheckBox id="ckbMascAA3" runat="server" CssClass="texto form-check-input bg-transparent border-0" ToolTip="3 KHz Aéreo Atual Mascarado" Enabled="False"></asp:CheckBox>
													</div>

													<div class="col-11">
														<asp:label id="Label48" runat="server" CssClass="texto">3 KHz</asp:label>
													</div>

													<div class="col-12">
														<asp:DropDownList id="ddlAA3" runat="server" CssClass="texto form-select">
															<asp:ListItem Value="NE">NE</asp:ListItem>
															<asp:ListItem Value="-10">-10</asp:ListItem>
															<asp:ListItem Value="-5">-5</asp:ListItem>
															<asp:ListItem Value="0">0</asp:ListItem>
															<asp:ListItem Value="5">5</asp:ListItem>
															<asp:ListItem Value="10">10</asp:ListItem>
															<asp:ListItem Value="15">15</asp:ListItem>
															<asp:ListItem Value="20">20</asp:ListItem>
															<asp:ListItem Value="25">25</asp:ListItem>
															<asp:ListItem Value="30">30</asp:ListItem>
															<asp:ListItem Value="35">35</asp:ListItem>
															<asp:ListItem Value="40">40</asp:ListItem>
															<asp:ListItem Value="45">45</asp:ListItem>
															<asp:ListItem Value="50">50</asp:ListItem>
															<asp:ListItem Value="55">55</asp:ListItem>
															<asp:ListItem Value="60">60</asp:ListItem>
															<asp:ListItem Value="65">65</asp:ListItem>
															<asp:ListItem Value="70">70</asp:ListItem>
															<asp:ListItem Value="75">75</asp:ListItem>
															<asp:ListItem Value="80">80</asp:ListItem>
															<asp:ListItem Value="85">85</asp:ListItem>
															<asp:ListItem Value="90">90</asp:ListItem>
															<asp:ListItem Value="95">95</asp:ListItem>
															<asp:ListItem Value="100">100</asp:ListItem>
															<asp:ListItem Value="105">105</asp:ListItem>
															<asp:ListItem Value="110">110</asp:ListItem>
															<asp:ListItem Value="115">115</asp:ListItem>
															<asp:ListItem Value="AR">AR</asp:ListItem>
														</asp:DropDownList>
													</div>
												</div>
											</div>

											<div class="col-1 gx-3 gy-2 ms-3">
												<div class="row">
													<div class="col-1">
														<asp:CheckBox id="ckbMascAA4" runat="server" CssClass="texto form-check-input bg-transparent border-0" ToolTip="4 KHz Aéreo Atual Mascarado" Enabled="False"></asp:CheckBox>
													</div>

													<div class="col-11">
														<asp:label id="Label49" runat="server" CssClass="texto">4 KHz</asp:label>
													</div>

													<div class="col-12">
														<asp:DropDownList id="ddlAA4" runat="server" CssClass="texto form-select">
															<asp:ListItem Value="NE">NE</asp:ListItem>
															<asp:ListItem Value="-10">-10</asp:ListItem>
															<asp:ListItem Value="-5">-5</asp:ListItem>
															<asp:ListItem Value="0">0</asp:ListItem>
															<asp:ListItem Value="5">5</asp:ListItem>
															<asp:ListItem Value="10">10</asp:ListItem>
															<asp:ListItem Value="15">15</asp:ListItem>
															<asp:ListItem Value="20">20</asp:ListItem>
															<asp:ListItem Value="25">25</asp:ListItem>
															<asp:ListItem Value="30">30</asp:ListItem>
															<asp:ListItem Value="35">35</asp:ListItem>
															<asp:ListItem Value="40">40</asp:ListItem>
															<asp:ListItem Value="45">45</asp:ListItem>
															<asp:ListItem Value="50">50</asp:ListItem>
															<asp:ListItem Value="55">55</asp:ListItem>
															<asp:ListItem Value="60">60</asp:ListItem>
															<asp:ListItem Value="65">65</asp:ListItem>
															<asp:ListItem Value="70">70</asp:ListItem>
															<asp:ListItem Value="75">75</asp:ListItem>
															<asp:ListItem Value="80">80</asp:ListItem>
															<asp:ListItem Value="85">85</asp:ListItem>
															<asp:ListItem Value="90">90</asp:ListItem>
															<asp:ListItem Value="95">95</asp:ListItem>
															<asp:ListItem Value="100">100</asp:ListItem>
															<asp:ListItem Value="105">105</asp:ListItem>
															<asp:ListItem Value="110">110</asp:ListItem>
															<asp:ListItem Value="115">115</asp:ListItem>
															<asp:ListItem Value="AR">AR</asp:ListItem>
														</asp:DropDownList>
													</div>
												</div>
											</div>

											<div class="col-1 gx-3 gy-2 ms-3">
												<div class="row">
													<div class="col-1">
														<asp:CheckBox id="ckbMascAA6" runat="server" CssClass="texto form-check-input bg-transparent border-0" ToolTip="6 KHz Aéreo Atual Mascarado" Enabled="False"></asp:CheckBox>
													</div>

													<div class="col-11">
														<asp:label id="Label50" runat="server" CssClass="texto">6 KHz</asp:label>
													</div>

													<div class="col-12">
														<asp:DropDownList id="ddlAA6" runat="server" CssClass="texto form-select">
															<asp:ListItem Value="NE">NE</asp:ListItem>
															<asp:ListItem Value="-10">-10</asp:ListItem>
															<asp:ListItem Value="-5">-5</asp:ListItem>
															<asp:ListItem Value="0">0</asp:ListItem>
															<asp:ListItem Value="5">5</asp:ListItem>
															<asp:ListItem Value="10">10</asp:ListItem>
															<asp:ListItem Value="15">15</asp:ListItem>
															<asp:ListItem Value="20">20</asp:ListItem>
															<asp:ListItem Value="25">25</asp:ListItem>
															<asp:ListItem Value="30">30</asp:ListItem>
															<asp:ListItem Value="35">35</asp:ListItem>
															<asp:ListItem Value="40">40</asp:ListItem>
															<asp:ListItem Value="45">45</asp:ListItem>
															<asp:ListItem Value="50">50</asp:ListItem>
															<asp:ListItem Value="55">55</asp:ListItem>
															<asp:ListItem Value="60">60</asp:ListItem>
															<asp:ListItem Value="65">65</asp:ListItem>
															<asp:ListItem Value="70">70</asp:ListItem>
															<asp:ListItem Value="75">75</asp:ListItem>
															<asp:ListItem Value="80">80</asp:ListItem>
															<asp:ListItem Value="85">85</asp:ListItem>
															<asp:ListItem Value="90">90</asp:ListItem>
															<asp:ListItem Value="95">95</asp:ListItem>
															<asp:ListItem Value="100">100</asp:ListItem>
															<asp:ListItem Value="105">105</asp:ListItem>
															<asp:ListItem Value="110">110</asp:ListItem>
															<asp:ListItem Value="115">115</asp:ListItem>
															<asp:ListItem Value="AR">AR</asp:ListItem>
														</asp:DropDownList>
													</div>
												</div>
											</div>

											<div class="col-1 gx-3 gy-2 ms-3">
												<div class="row">
													<div class="col-1">
														<asp:CheckBox id="ckbMascAA8" runat="server" CssClass="texto form-check-input bg-transparent border-0" ToolTip="8 KHz Aéreo Atual Mascarado" Enabled="False"></asp:CheckBox>
													</div>

													<div class="col-11">
														<asp:label id="Label51" runat="server" CssClass="texto">8 KHz</asp:label>
													</div>

													<div class="col-12">
														<asp:DropDownList id="ddlAA8" runat="server" CssClass="texto form-select">
															<asp:ListItem Value="NE">NE</asp:ListItem>
															<asp:ListItem Value="-10">-10</asp:ListItem>
															<asp:ListItem Value="-5">-5</asp:ListItem>
															<asp:ListItem Value="0">0</asp:ListItem>
															<asp:ListItem Value="5">5</asp:ListItem>
															<asp:ListItem Value="10">10</asp:ListItem>
															<asp:ListItem Value="15">15</asp:ListItem>
															<asp:ListItem Value="20">20</asp:ListItem>
															<asp:ListItem Value="25">25</asp:ListItem>
															<asp:ListItem Value="30">30</asp:ListItem>
															<asp:ListItem Value="35">35</asp:ListItem>
															<asp:ListItem Value="40">40</asp:ListItem>
															<asp:ListItem Value="45">45</asp:ListItem>
															<asp:ListItem Value="50">50</asp:ListItem>
															<asp:ListItem Value="55">55</asp:ListItem>
															<asp:ListItem Value="60">60</asp:ListItem>
															<asp:ListItem Value="65">65</asp:ListItem>
															<asp:ListItem Value="70">70</asp:ListItem>
															<asp:ListItem Value="75">75</asp:ListItem>
															<asp:ListItem Value="80">80</asp:ListItem>
															<asp:ListItem Value="85">85</asp:ListItem>
															<asp:ListItem Value="90">90</asp:ListItem>
															<asp:ListItem Value="95">95</asp:ListItem>
															<asp:ListItem Value="100">100</asp:ListItem>
															<asp:ListItem Value="105">105</asp:ListItem>
															<asp:ListItem Value="110">110</asp:ListItem>
															<asp:ListItem Value="115">115</asp:ListItem>
															<asp:ListItem Value="AR">AR</asp:ListItem>
														</asp:DropDownList>
													</div>
												</div>
											</div>

											<div class="col-1 gx-3" style="margin-top: 30px">
												<asp:label id="Label53" runat="server" CssClass="texto">dBNA</asp:label>
											</div>
										</div>
									</div>

									<div class="col-12 mb-4">
										<div class="row">
											<div class="col-12 gx-3 gy-2">
												<asp:label id="Label100" runat="server" CssClass="tituloLabel">Ósseos Atual</asp:label>
											</div>

											<div class="col-1 gx-3 gy-2 ms-3">
												<div class="col-1">
													<asp:CheckBox id="ckbMascOA05" runat="server" CssClass="texto form-check-input bg-transparent border-0" ToolTip="500 Hz Ósseo Atual Mascarado" Enabled="False"></asp:CheckBox>
												</div>

												<div class="col-11">
													<asp:Label runat="server" CssClass="texto">500Hz</asp:Label>
												</div>

												<div class="col-12">
													<asp:DropDownList id="ddlOA05" runat="server" CssClass="texto form-select">
														<asp:ListItem Value="NE">NE</asp:ListItem>
														<asp:ListItem Value="0">0</asp:ListItem>
														<asp:ListItem Value="5">5</asp:ListItem>
														<asp:ListItem Value="10">10</asp:ListItem>
														<asp:ListItem Value="15">15</asp:ListItem>
														<asp:ListItem Value="20">20</asp:ListItem>
														<asp:ListItem Value="25">25</asp:ListItem>
														<asp:ListItem Value="30">30</asp:ListItem>
														<asp:ListItem Value="35">35</asp:ListItem>
														<asp:ListItem Value="40">40</asp:ListItem>
														<asp:ListItem Value="45">45</asp:ListItem>
														<asp:ListItem Value="50">50</asp:ListItem>
														<asp:ListItem Value="55">55</asp:ListItem>
														<asp:ListItem Value="60">60</asp:ListItem>
														<asp:ListItem Value="65">65</asp:ListItem>
														<asp:ListItem Value="AR">AR</asp:ListItem>
													</asp:DropDownList>
												</div>
											</div>

											<div class="col-1 gx-3 gy-2 ms-3">
												<div class="row">
													<div class="col-1">
														<asp:CheckBox id="ckbMascOA1" runat="server" CssClass="texto form-check-input bg-transparent border-0" ToolTip="1 KHz Ósseo Atual Mascarado" Enabled="False"></asp:CheckBox>
													</div>

													<div class="col-11">
														<asp:Label runat="server" CssClass="texto">1 KHz</asp:Label>
													</div>

													<div class="col-12">
														<asp:DropDownList id="ddlOA1" runat="server" CssClass="texto form-select">
															<asp:ListItem Value="NE">NE</asp:ListItem>
															<asp:ListItem Value="0">0</asp:ListItem>
															<asp:ListItem Value="5">5</asp:ListItem>
															<asp:ListItem Value="10">10</asp:ListItem>
															<asp:ListItem Value="15">15</asp:ListItem>
															<asp:ListItem Value="20">20</asp:ListItem>
															<asp:ListItem Value="25">25</asp:ListItem>
															<asp:ListItem Value="30">30</asp:ListItem>
															<asp:ListItem Value="35">35</asp:ListItem>
															<asp:ListItem Value="40">40</asp:ListItem>
															<asp:ListItem Value="45">45</asp:ListItem>
															<asp:ListItem Value="50">50</asp:ListItem>
															<asp:ListItem Value="55">55</asp:ListItem>
															<asp:ListItem Value="60">60</asp:ListItem>
															<asp:ListItem Value="65">65</asp:ListItem>
															<asp:ListItem Value="AR">AR</asp:ListItem>
														</asp:DropDownList>
													</div>
												</div>
											</div>

											<div class="col-1 gx-3 gy-2 ms-3">
												<div class="row">
													<div class="col-1">
														<asp:CheckBox id="ckbMascOA2" runat="server" CssClass="texto form-check-input bg-transparent border-0" ToolTip="2 KHz Ósseo Atual Mascarado" Enabled="False"></asp:CheckBox>
													</div>

													<div class="col-11">
														<asp:label runat="server" CssClass="texto">2 KHz</asp:label>
													</div>

													<div class="col-12">
														<asp:DropDownList id="ddlOA2" runat="server" CssClass="texto form-select">
															<asp:ListItem Value="NE">NE</asp:ListItem>
															<asp:ListItem Value="0">0</asp:ListItem>
															<asp:ListItem Value="5">5</asp:ListItem>
															<asp:ListItem Value="10">10</asp:ListItem>
															<asp:ListItem Value="15">15</asp:ListItem>
															<asp:ListItem Value="20">20</asp:ListItem>
															<asp:ListItem Value="25">25</asp:ListItem>
															<asp:ListItem Value="30">30</asp:ListItem>
															<asp:ListItem Value="35">35</asp:ListItem>
															<asp:ListItem Value="40">40</asp:ListItem>
															<asp:ListItem Value="45">45</asp:ListItem>
															<asp:ListItem Value="50">50</asp:ListItem>
															<asp:ListItem Value="55">55</asp:ListItem>
															<asp:ListItem Value="60">60</asp:ListItem>
															<asp:ListItem Value="65">65</asp:ListItem>
															<asp:ListItem Value="AR">AR</asp:ListItem>
														</asp:DropDownList>
													</div>
												</div>
											</div>

											<div class="col-1 gx-3 gy-2 ms-3">
												<div class="row">
													<div class="col-1">
														<asp:CheckBox id="ckbMascOA3" runat="server" CssClass="texto form-check-input bg-transparent border-0" ToolTip="3 KHz Ósseo Atual Mascarado" Enabled="False"></asp:CheckBox>
													</div>

													<div class="col-11">
														<asp:Label runat="server" CssClass="texto">3 KHz</asp:Label>
													</div>

													<div class="col-12">
														<asp:DropDownList id="ddlOA3" runat="server" CssClass="texto form-select">
															<asp:ListItem Value="NE">NE</asp:ListItem>
															<asp:ListItem Value="0">0</asp:ListItem>
															<asp:ListItem Value="5">5</asp:ListItem>
															<asp:ListItem Value="10">10</asp:ListItem>
															<asp:ListItem Value="15">15</asp:ListItem>
															<asp:ListItem Value="20">20</asp:ListItem>
															<asp:ListItem Value="25">25</asp:ListItem>
															<asp:ListItem Value="30">30</asp:ListItem>
															<asp:ListItem Value="35">35</asp:ListItem>
															<asp:ListItem Value="40">40</asp:ListItem>
															<asp:ListItem Value="45">45</asp:ListItem>
															<asp:ListItem Value="50">50</asp:ListItem>
															<asp:ListItem Value="55">55</asp:ListItem>
															<asp:ListItem Value="60">60</asp:ListItem>
															<asp:ListItem Value="65">65</asp:ListItem>
															<asp:ListItem Value="AR">AR</asp:ListItem>
														</asp:DropDownList>
													</div>
												</div>
											</div>

											<div class="col-1 gx-3 gy-2 ms-3">
												<div class="row">
													<div class="col-1">
														<asp:CheckBox id="ckbMascOA4" runat="server" CssClass="texto form-check-input bg-transparent border-0" ToolTip="4 KHz Ósseo Atual Mascarado" Enabled="False"></asp:CheckBox>
													</div>

													<div class="col-11">
														<asp:Label runat="server" CssClass="texto">4 KHz</asp:Label>
													</div>

													<div class="col-12">
														<asp:DropDownList id="ddlOA4" runat="server" CssClass="texto form-select">
															<asp:ListItem Value="NE">NE</asp:ListItem>
															<asp:ListItem Value="0">0</asp:ListItem>
															<asp:ListItem Value="5">5</asp:ListItem>
															<asp:ListItem Value="10">10</asp:ListItem>
															<asp:ListItem Value="15">15</asp:ListItem>
															<asp:ListItem Value="20">20</asp:ListItem>
															<asp:ListItem Value="25">25</asp:ListItem>
															<asp:ListItem Value="30">30</asp:ListItem>
															<asp:ListItem Value="35">35</asp:ListItem>
															<asp:ListItem Value="40">40</asp:ListItem>
															<asp:ListItem Value="45">45</asp:ListItem>
															<asp:ListItem Value="50">50</asp:ListItem>
															<asp:ListItem Value="55">55</asp:ListItem>
															<asp:ListItem Value="60">60</asp:ListItem>
															<asp:ListItem Value="65">65</asp:ListItem>
															<asp:ListItem Value="AR">AR</asp:ListItem>
														</asp:DropDownList>
													</div>
												</div>
											</div>

											<div class="col-1 gx-3 gy-2 ms-3">
												<div class="row">
													<div class="col-1">
														<asp:CheckBox id="ckbMascOA6" runat="server" CssClass="texto form-check-input bg-transparent border-0" ToolTip="6 KHz Ósseo Atual Mascarado" Enabled="False"></asp:CheckBox>
													</div>

													<div class="col-11">
														<asp:Label runat="server" CssClass="texto">6 KHz</asp:Label>
													</div>

													<div class="col-12">
														<asp:DropDownList id="ddlOA6" runat="server" CssClass="texto form-select">
															<asp:ListItem Value="NE">NE</asp:ListItem>
															<asp:ListItem Value="0">0</asp:ListItem>
															<asp:ListItem Value="5">5</asp:ListItem>
															<asp:ListItem Value="10">10</asp:ListItem>
															<asp:ListItem Value="15">15</asp:ListItem>
															<asp:ListItem Value="20">20</asp:ListItem>
															<asp:ListItem Value="25">25</asp:ListItem>
															<asp:ListItem Value="30">30</asp:ListItem>
															<asp:ListItem Value="35">35</asp:ListItem>
															<asp:ListItem Value="40">40</asp:ListItem>
															<asp:ListItem Value="45">45</asp:ListItem>
															<asp:ListItem Value="50">50</asp:ListItem>
															<asp:ListItem Value="55">55</asp:ListItem>
															<asp:ListItem Value="60">60</asp:ListItem>
															<asp:ListItem Value="65">65</asp:ListItem>
															<asp:ListItem Value="AR">AR</asp:ListItem>
														</asp:DropDownList>
													</div>
												</div>
											</div>

											<div class="col-1 gx-3" style="margin-top: 30px">
												<asp:label id="Label2" runat="server" CssClass="texto">dBNA</asp:label>
											</div>
										</div>
									</div>
									<div class="col-12 gx-3 gy-2">
									<div class="row">
										<div class="col gx-3 gy-2">
											<asp:label id="Label9" runat="server" CssClass="tituloLabel">Aéreos Referencial</asp:label>
										</div>
									</div>
									<div class="row">
										<div class="col-1 gx-3 gy-2">
											<asp:label id="Label111" runat="server" CssClass="texto gy-2" >250 Hz</asp:label>
											<asp:textbox id="wteAR025" JavaScriptFileName="ig_edit.js" JavaScriptFileNameCommon="ig_shared.js"
												ImageDirectory=" " runat="server" CssClass="texto form-control" HorizontalAlign="Center"
												ReadOnly="True"></asp:textbox>
										</div>
										<div class="col-1 gx-3 gy-2">
											<asp:label id="Label4" runat="server" CssClass="texto gy-2">500 Hz</asp:label>
											<asp:textbox id="wteAR05" JavaScriptFileName="ig_edit.js" JavaScriptFileNameCommon="ig_shared.js"
												ImageDirectory=" " runat="server" CssClass="texto form-control" HorizontalAlign="Center"
												ReadOnly="True"></asp:textbox>
										</div>
										<div class="col-1 gx-3 gy-2">
											<asp:label id="Label5" runat="server" CssClass="texto gy-2">1 KHz</asp:label>
											<asp:textbox id="wteAR1" JavaScriptFileName="ig_edit.js" JavaScriptFileNameCommon="ig_shared.js"
												ImageDirectory=" " runat="server" CssClass="texto form-control" HorizontalAlign="Center"
												ReadOnly="True"></asp:textbox>
										</div>
										<div class="col-1 gx-3 gy-2">
											<asp:label id="Label6" runat="server" CssClass="texto gy-2">2 KHz</asp:label>
											<asp:textbox id="wteAR2" JavaScriptFileName="ig_edit.js" JavaScriptFileNameCommon="ig_shared.js"
												ImageDirectory=" " runat="server" CssClass="texto form-control" HorizontalAlign="Center"
												ReadOnly="True"></asp:textbox>
										</div>
										<div class="col-1 gx-3 gy-2">
											<asp:label id="Label8" runat="server" CssClass="texto gy-2">3 KHz</asp:label>
											<asp:textbox id="wteAR3" JavaScriptFileName="ig_edit.js" JavaScriptFileNameCommon="ig_shared.js"
												ImageDirectory=" " runat="server" CssClass="texto form-control" HorizontalAlign="Center"
												ReadOnly="True"></asp:textbox>
										</div>
										<div class="col-1 gx-3 gy-2">
											<asp:label id="Label11" runat="server" CssClass="texto gy-2">4 KHz</asp:label>
											<asp:textbox id="wteAR4" JavaScriptFileName="ig_edit.js" JavaScriptFileNameCommon="ig_shared.js"
												ImageDirectory=" " runat="server" CssClass="texto form-control" HorizontalAlign="Center"
												ReadOnly="True"></asp:textbox>
										</div>
										<div class="col-1 gx-3 gy-2">
											<asp:label id="Label12" runat="server" CssClass="texto">6 KHz</asp:label>
											<asp:textbox id="wteAR6" JavaScriptFileName="ig_edit.js" JavaScriptFileNameCommon="ig_shared.js"
												ImageDirectory=" " runat="server" CssClass="texto form-control" HorizontalAlign="Center"
												ReadOnly="True"></asp:textbox>
										</div>
										<div class="col-1 gx-3 gy-2">
											<asp:label id="Label13" runat="server" CssClass="texto gy-2">8 KHz</asp:label>
											<asp:textbox id="wteAR8" JavaScriptFileName="ig_edit.js" JavaScriptFileNameCommon="ig_shared.js"
												ImageDirectory=" " runat="server" CssClass="texto form-control" HorizontalAlign="Center"
												ReadOnly="True"></asp:textbox>
										</div>
										<div class="col-1 gx-3 gy-2" style="margin-top: 30px">
											<asp:label id="Label54" runat="server" CssClass="texto">dBNA</asp:label>
										</div>
									</div>
								</div>
								<div class="col-12">
									<div class="row">
										<div class="col gx-3 gy-2">
											<asp:label id="Label43" runat="server" CssClass="tituloLabel">Ósseos Referencial</asp:label>
										</div>
									</div>
									<div class="row">
										<div class="col-1 gx-3 gy-2">
											<asp:label id="Label1" runat="server" CssClass="texto gy-2">8 KHz</asp:label>
											<asp:textbox id="wteOR05" JavaScriptFileName="ig_edit.js" JavaScriptFileNameCommon="ig_shared.js"
												ImageDirectory=" " runat="server" CssClass="texto form-control" HorizontalAlign="Center"
												ReadOnly="True"></asp:textbox>
										</div>
										<div class="col-1 gx-3 gy-2">
											<asp:label id="Label3" runat="server" CssClass="texto gy-2">500 Hz</asp:label>
											<asp:textbox id="wteOR1" JavaScriptFileName="ig_edit.js" JavaScriptFileNameCommon="ig_shared.js"
												ImageDirectory=" " runat="server" CssClass="texto form-control" HorizontalAlign="Center"
												ReadOnly="True"></asp:textbox>
										</div>
										<div class="col-1 gx-3 gy-2">
											<asp:label id="Label10" runat="server" CssClass="texto gy-2">1 KHz</asp:label>
											<asp:textbox id="wteOR2" JavaScriptFileName="ig_edit.js" JavaScriptFileNameCommon="ig_shared.js"
												ImageDirectory=" " runat="server" CssClass="texto form-control" HorizontalAlign="Center"
												ReadOnly="True"></asp:textbox>
										</div>
										<div class="col-1 gx-3 gy-2">
											<asp:label id="Label15" runat="server" CssClass="texto gy-2">2 KHz</asp:label>
											<asp:textbox id="wteOR3" JavaScriptFileName="ig_edit.js" JavaScriptFileNameCommon="ig_shared.js"
												ImageDirectory=" " runat="server" CssClass="texto form-control" HorizontalAlign="Center"
												ReadOnly="True"></asp:textbox>
										</div>
										<div class="col-1 gx-3 gy-2">
											<asp:label id="Label16" runat="server" CssClass="texto gy-2">3 KHz</asp:label>
											<asp:textbox id="wteOR4" JavaScriptFileName="ig_edit.js" JavaScriptFileNameCommon="ig_shared.js"
												ImageDirectory=" " runat="server" CssClass="texto form-control" HorizontalAlign="Center"
												ReadOnly="True"></asp:textbox>
										</div>
										<div class="col-1 gx-3 gy-2">
											<asp:label id="Label17" runat="server" CssClass="texto gy-2">4 KHz</asp:label>
											<asp:textbox id="wteOR6" JavaScriptFileName="ig_edit.js" JavaScriptFileNameCommon="ig_shared.js"
												ImageDirectory=" " runat="server" CssClass="texto form-control" HorizontalAlign="Center"
												ReadOnly="True"></asp:textbox>
										</div>
										<div class="col-1 gx-3 gy-2" style="margin-top: 30px">
											<asp:label id="Label56" runat="server" CssClass="texto" >dBNA</asp:label>											
										</div>
									</div>
								</div>

							<BR>
							<div class="col gx-3 gy-2">
								<div class="row">
									<div class="col-md-6 gx-3 gy-2">
										<asp:label id="Label57" runat="server" CssClass="tituloLabel">Interpretação Atual</asp:label>
										<asp:textbox id="txtIAtual" runat="server" CssClass="texto form-control" ReadOnly="True"></asp:textbox>
									</div>
								</div>
								<div class="row">
									<div class="col-md-6 gx-3 gy-2">
										<asp:label id="Label58" runat="server" CssClass="tituloLabel">Interpretação Ref.</asp:label>
										<asp:textbox id="txtIReferencial" runat="server" CssClass="texto form-control"
											ReadOnly="True"></asp:textbox>
               </eo:PageView>


              <eo:PageView ID="Pageview23" runat="server" Width="1050px">


					<div class="col-12">
                        <div class="row">

					<div class="row">
                      <div class="col-md-3 gx-2 gy-2">
						   <asp:label id="Label59" runat="server" CssClass="tituloLabel form-label">Meatoscopia</asp:label>
					</div>
					 </div>

					<div class="row">
					<div class="col-md-4 gx-2 gy-2 ms-3">
                         <asp:RadioButtonList id="RadioButtonList1" Width="250px" runat="server" RepeatColumns="3" tabIndex="1" CssClass="texto form-check-input bg-transparent border-0">
							  <asp:ListItem Value="0" Selected="True">Sem Alteração</asp:ListItem>
							   <asp:ListItem Value="1">Com Alteração</asp:ListItem>
						</asp:RadioButtonList>
					</div>
					 </div>
					
					<div class="row">
					 <div class="col-md-11 gx-2 gy-2">
                          <asp:Label id="Label14" runat="server" CssClass="tituloLabel form-label">Observações da Meatoscopia</asp:Label>
						  <asp:textbox id="txtDescMeatoscopia" Width="409px" runat="server" CssClass="texto form-control form-control-sm" TextMode="MultiLine" Rows="3"></asp:textbox>
					 </div>
					</div>

				    </div>
					 </div>

				  <div class="col-12">
                        <div class="row">

				
			   <div class="col-md-4 gx-2 gy-2 ms-3">
					<asp:RadioButtonList id="rblMeatoscopia" Width="250px" runat="server" RepeatColumns="3" tabIndex="1" CssClass="texto form-check-input bg-transparent border-0">
							  <asp:ListItem Value="0" Selected="True">Sem Alteração</asp:ListItem>
							   <asp:ListItem Value="1">Com Alteração</asp:ListItem>
						</asp:RadioButtonList>
				</div>

				</div>
					</div>
				

				  <div class="col-12">
                        <div class="row">

				<div class="col-md-6 gx-2 gy-2">
                    <asp:label id="Label60" runat="server" CssClass="tituloLabel form-label">Parecer Fonoaudiológico</asp:label>
					<asp:DropDownList id="ddlDiagnostico" OnSelectedIndexChanged="ddlDiagnostico_SelectedIndexChanged" runat="server" CssClass="texto form-select form-select-sm" AutoPostBack="True"></asp:DropDownList>
				 </div>

				
				<div class="col-md-2 gx-8 gy-4 ms-3">
                    <asp:CheckBox id="ckbIsOcupacional" runat="server" CssClass="texto form-check-input bg-transparent border-0" Text="Ocupacional" Enabled="False"></asp:CheckBox>
				</div>
				

			  <div class="col-md-11 gx-2 gy-2">
                   <asp:label id="Label61" runat="server" CssClass="tituloLabel form-label">Observações do Parecer Fonoaudiológico</asp:label>
				   <asp:textbox id="txtObs" Width="500px" runat="server" CssClass="texto form-control form-control-sm" TextMode="MultiLine" Rows="3"></asp:textbox>
			  </div>

                  </div>
					  </div>
				   


									
										<div vAlign="middle" align="center" width="125">
											<asp:Image id="Image9" runat="server" Visible="false" ImageUrl="img/5pixel.gif"></asp:Image></div>
										<div align="center" width="429">
											<asp:Image id="Image2" runat="server" Visible="false" ImageUrl="img/5pixel.gif"></asp:Image></div>
									
								
									
										<div vAlign="middle" align="center" width="125">
											<asp:Image id="Image1" runat="server" Visible="false" ImageUrl="img/5pixel.gif"></asp:Image></div>
										<div align="center" width="429">
											<asp:Image id="Image3" runat="server" Visible="false" ImageUrl="img/5pixel.gif"></asp:Image></div>
									
							
					
	</eo:PageView>
    </eo:MultiPage>

