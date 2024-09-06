﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Ilitera.Net.Login" %>

<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>

<!DOCTYPE html>

<html lang="pt">
<head runat="server">
    <title>Ilitera.Net</title>
    <link href="css/login.css" rel="stylesheet" type="text/css" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.2.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-rbsA2VBKQhggwzxH7pPCaAqO46MgnOM80zW1RWuH61DGLwZJEdK2Kadq2F9CUG65" crossorigin="anonymous">
    <style type="text/css">
        @font-face{
            font-family: "Univia Pro";
            src: url("css/UniviaPro-Regular.otf");
        }
        @font-face{
            font-family: "Ubuntu";
            src: url("css/Ubuntu-Regular.ttf");
        }
        body, .wp-login {
            background-image: url('../Images/LoginBG.svg') !important;
            width: 100%;
            height: 100vh;
            background-size: cover;
            background-repeat: no-repeat;
        }

        .Logo{
            width: 400px;
        }

     </style>

    <!--<script type="text/javascript">
      var _gaq = _gaq || [];
      _gaq.push(['_setAccount', 'UA-19248250-1']);
      _gaq.push(['_trackPageview']);
      (function() {
        var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;
        ga.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www') + '.google-analytics.com/ga.js';
        var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);
      })();
   </script>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge"><script type="text/javascript">(window.NREUM||(NREUM={})).init={ajax:{deny_list:["bam.nr-data.net"]}};(window.NREUM||(NREUM={})).loader_config={licenseKey:"aece2c08f5",applicationID:"543213780"};window.NREUM||(NREUM={}),__nr_require=function(t,e,n){function r(n){if(!e[n]){var i=e[n]={exports:{}};t[n][0].call(i.exports,function(e){var i=t[n][1][e];return r(i||e)},i,i.exports)}return e[n].exports}if("function"==typeof __nr_require)return __nr_require;for(var i=0;i<n.length;i++)r(n[i]);return r}({1:[function(t,e,n){function r(){}function i(t,e,n,r){return function(){return s.recordSupportability("API/"+e+"/called"),o(t+e,[u.now()].concat(c(arguments)),n?null:this,r),n?void 0:this}}var o=t("handle"),a=t(9),c=t(10),f=t("ee").get("tracer"),u=t("loader"),s=t(4),d=NREUM;"undefined"==typeof window.newrelic&&(newrelic=d);var p=["setPageViewName","setCustomAttribute","setErrorHandler","finished","addToTrace","inlineHit","addRelease"],l="api-",v=l+"ixn-";a(p,function(t,e){d[e]=i(l,e,!0,"api")}),d.addPageAction=i(l,"addPageAction",!0),d.setCurrentRouteName=i(l,"routeName",!0),e.exports=newrelic,d.interaction=function(){return(new r).get()};var m=r.prototype={createTracer:function(t,e){var n={},r=this,i="function"==typeof e;return o(v+"tracer",[u.now(),t,n],r),function(){if(f.emit((i?"":"no-")+"fn-start",[u.now(),r,i],n),i)try{return e.apply(this,arguments)}catch(t){throw f.emit("fn-err",[arguments,this,t],n),t}finally{f.emit("fn-end",[u.now()],n)}}}};a("actionText,setName,setAttribute,save,ignore,onEnd,getContext,end,get".split(","),function(t,e){m[e]=i(v,e)}),newrelic.noticeError=function(t,e){"string"==typeof t&&(t=new Error(t)),s.recordSupportability("API/noticeError/called"),o("err",[t,u.now(),!1,e])}},{}],2:[function(t,e,n){function r(t){if(NREUM.init){for(var e=NREUM.init,n=t.split("."),r=0;r<n.length-1;r++)if(e=e[n[r]],"object"!=typeof e)return;return e=e[n[n.length-1]]}}e.exports={getConfiguration:r}},{}],3:[function(t,e,n){var r=!1;try{var i=Object.defineProperty({},"passive",{get:function(){r=!0}});window.addEventListener("testPassive",null,i),window.removeEventListener("testPassive",null,i)}catch(o){}e.exports=function(t){return r?{passive:!0,capture:!!t}:!!t}},{}],4:[function(t,e,n){function r(t,e){var n=[a,t,{name:t},e];return o("storeMetric",n,null,"api"),n}function i(t,e){var n=[c,t,{name:t},e];return o("storeEventMetrics",n,null,"api"),n}var o=t("handle"),a="sm",c="cm";e.exports={constants:{SUPPORTABILITY_METRIC:a,CUSTOM_METRIC:c},recordSupportability:r,recordCustom:i}},{}],5:[function(t,e,n){function r(){return c.exists&&performance.now?Math.round(performance.now()):(o=Math.max((new Date).getTime(),o))-a}function i(){return o}var o=(new Date).getTime(),a=o,c=t(11);e.exports=r,e.exports.offset=a,e.exports.getLastTimestamp=i},{}],6:[function(t,e,n){function r(t,e){var n=t.getEntries();n.forEach(function(t){"first-paint"===t.name?l("timing",["fp",Math.floor(t.startTime)]):"first-contentful-paint"===t.name&&l("timing",["fcp",Math.floor(t.startTime)])})}function i(t,e){var n=t.getEntries();if(n.length>0){var r=n[n.length-1];if(u&&u<r.startTime)return;var i=[r],o=a({});o&&i.push(o),l("lcp",i)}}function o(t){t.getEntries().forEach(function(t){t.hadRecentInput||l("cls",[t])})}function a(t){var e=navigator.connection||navigator.mozConnection||navigator.webkitConnection;if(e)return e.type&&(t["net-type"]=e.type),e.effectiveType&&(t["net-etype"]=e.effectiveType),e.rtt&&(t["net-rtt"]=e.rtt),e.downlink&&(t["net-dlink"]=e.downlink),t}function c(t){if(t instanceof y&&!w){var e=Math.round(t.timeStamp),n={type:t.type};a(n),e<=v.now()?n.fid=v.now()-e:e>v.offset&&e<=Date.now()?(e-=v.offset,n.fid=v.now()-e):e=v.now(),w=!0,l("timing",["fi",e,n])}}function f(t){"hidden"===t&&(u=v.now(),l("pageHide",[u]))}if(!("init"in NREUM&&"page_view_timing"in NREUM.init&&"enabled"in NREUM.init.page_view_timing&&NREUM.init.page_view_timing.enabled===!1)){var u,s,d,p,l=t("handle"),v=t("loader"),m=t(8),g=t(3),y=NREUM.o.EV;if("PerformanceObserver"in window&&"function"==typeof window.PerformanceObserver){s=new PerformanceObserver(r);try{s.observe({entryTypes:["paint"]})}catch(h){}d=new PerformanceObserver(i);try{d.observe({entryTypes:["largest-contentful-paint"]})}catch(h){}p=new PerformanceObserver(o);try{p.observe({type:"layout-shift",buffered:!0})}catch(h){}}if("addEventListener"in document){var w=!1,b=["click","keydown","mousedown","pointerdown","touchstart"];b.forEach(function(t){document.addEventListener(t,c,g(!1))})}m(f)}},{}],7:[function(t,e,n){function r(t,e){if(!i)return!1;if(t!==i)return!1;if(!e)return!0;if(!o)return!1;for(var n=o.split("."),r=e.split("."),a=0;a<r.length;a++)if(r[a]!==n[a])return!1;return!0}var i=null,o=null,a=/Version\/(\S+)\s+Safari/;if(navigator.userAgent){var c=navigator.userAgent,f=c.match(a);f&&c.indexOf("Chrome")===-1&&c.indexOf("Chromium")===-1&&(i="Safari",o=f[1])}e.exports={agent:i,version:o,match:r}},{}],8:[function(t,e,n){function r(t){function e(){t(c&&document[c]?document[c]:document[o]?"hidden":"visible")}"addEventListener"in document&&a&&document.addEventListener(a,e,i(!1))}var i=t(3);e.exports=r;var o,a,c;"undefined"!=typeof document.hidden?(o="hidden",a="visibilitychange",c="visibilityState"):"undefined"!=typeof document.msHidden?(o="msHidden",a="msvisibilitychange"):"undefined"!=typeof document.webkitHidden&&(o="webkitHidden",a="webkitvisibilitychange",c="webkitVisibilityState")},{}],9:[function(t,e,n){function r(t,e){var n=[],r="",o=0;for(r in t)i.call(t,r)&&(n[o]=e(r,t[r]),o+=1);return n}var i=Object.prototype.hasOwnProperty;e.exports=r},{}],10:[function(t,e,n){function r(t,e,n){e||(e=0),"undefined"==typeof n&&(n=t?t.length:0);for(var r=-1,i=n-e||0,o=Array(i<0?0:i);++r<i;)o[r]=t[e+r];return o}e.exports=r},{}],11:[function(t,e,n){e.exports={exists:"undefined"!=typeof window.performance&&window.performance.timing&&"undefined"!=typeof window.performance.timing.navigationStart}},{}],ee:[function(t,e,n){function r(){}function i(t){function e(t){return t&&t instanceof r?t:t?u(t,f,a):a()}function n(n,r,i,o,a){if(a!==!1&&(a=!0),!l.aborted||o){t&&a&&t(n,r,i);for(var c=e(i),f=m(n),u=f.length,s=0;s<u;s++)f[s].apply(c,r);var p=d[w[n]];return p&&p.push([b,n,r,c]),c}}function o(t,e){h[t]=m(t).concat(e)}function v(t,e){var n=h[t];if(n)for(var r=0;r<n.length;r++)n[r]===e&&n.splice(r,1)}function m(t){return h[t]||[]}function g(t){return p[t]=p[t]||i(n)}function y(t,e){l.aborted||s(t,function(t,n){e=e||"feature",w[n]=e,e in d||(d[e]=[])})}var h={},w={},b={on:o,addEventListener:o,removeEventListener:v,emit:n,get:g,listeners:m,context:e,buffer:y,abort:c,aborted:!1};return b}function o(t){return u(t,f,a)}function a(){return new r}function c(){(d.api||d.feature)&&(l.aborted=!0,d=l.backlog={})}var f="nr@context",u=t("gos"),s=t(9),d={},p={},l=e.exports=i();e.exports.getOrSetContext=o,l.backlog=d},{}],gos:[function(t,e,n){function r(t,e,n){if(i.call(t,e))return t[e];var r=n();if(Object.defineProperty&&Object.keys)try{return Object.defineProperty(t,e,{value:r,writable:!0,enumerable:!1}),r}catch(o){}return t[e]=r,r}var i=Object.prototype.hasOwnProperty;e.exports=r},{}],handle:[function(t,e,n){function r(t,e,n,r){i.buffer([t],r),i.emit(t,e,n)}var i=t("ee").get("handle");e.exports=r,r.ee=i},{}],id:[function(t,e,n){function r(t){var e=typeof t;return!t||"object"!==e&&"function"!==e?-1:t===window?0:a(t,o,function(){return i++})}var i=1,o="nr@id",a=t("gos");e.exports=r},{}],loader:[function(t,e,n){function r(){if(!M++){var t=T.info=NREUM.info,e=m.getElementsByTagName("script")[0];if(setTimeout(u.abort,3e4),!(t&&t.licenseKey&&t.applicationID&&e))return u.abort();f(x,function(e,n){t[e]||(t[e]=n)});var n=a();c("mark",["onload",n+T.offset],null,"api"),c("timing",["load",n]);var r=m.createElement("script");0===t.agent.indexOf("http://")||0===t.agent.indexOf("https://")?r.src=t.agent:r.src=l+"://"+t.agent,e.parentNode.insertBefore(r,e)}}function i(){"complete"===m.readyState&&o()}function o(){c("mark",["domContent",a()+T.offset],null,"api")}var a=t(5),c=t("handle"),f=t(9),u=t("ee"),s=t(7),d=t(2),p=t(3),l=d.getConfiguration("ssl")===!1?"http":"https",v=window,m=v.document,g="addEventListener",y="attachEvent",h=v.XMLHttpRequest,w=h&&h.prototype,b=!1;NREUM.o={ST:setTimeout,SI:v.setImmediate,CT:clearTimeout,XHR:h,REQ:v.Request,EV:v.Event,PR:v.Promise,MO:v.MutationObserver};var E=""+location,x={beacon:"bam.nr-data.net",errorBeacon:"bam.nr-data.net",agent:"js-agent.newrelic.com/nr-1216.min.js"},O=h&&w&&w[g]&&!/CriOS/.test(navigator.userAgent),T=e.exports={offset:a.getLastTimestamp(),now:a,origin:E,features:{},xhrWrappable:O,userAgent:s,disabled:b};if(!b){t(1),t(6),m[g]?(m[g]("DOMContentLoaded",o,p(!1)),v[g]("load",r,p(!1))):(m[y]("onreadystatechange",i),v[y]("onload",r)),c("mark",["firstbyte",a.getLastTimestamp()],null,"api");var M=0}},{}],"wrap-function":[function(t,e,n){function r(t,e){function n(e,n,r,f,u){function nrWrapper(){var o,a,s,p;try{a=this,o=d(arguments),s="function"==typeof r?r(o,a):r||{}}catch(l){i([l,"",[o,a,f],s],t)}c(n+"start",[o,a,f],s,u);try{return p=e.apply(a,o)}catch(v){throw c(n+"err",[o,a,v],s,u),v}finally{c(n+"end",[o,a,p],s,u)}}return a(e)?e:(n||(n=""),nrWrapper[p]=e,o(e,nrWrapper,t),nrWrapper)}function r(t,e,r,i,o){r||(r="");var c,f,u,s="-"===r.charAt(0);for(u=0;u<e.length;u++)f=e[u],c=t[f],a(c)||(t[f]=n(c,s?f+r:r,i,f,o))}function c(n,r,o,a){if(!v||e){var c=v;v=!0;try{t.emit(n,r,o,e,a)}catch(f){i([f,n,r,o],t)}v=c}}return t||(t=s),n.inPlace=r,n.flag=p,n}function i(t,e){e||(e=s);try{e.emit("internal-error",t)}catch(n){}}function o(t,e,n){if(Object.defineProperty&&Object.keys)try{var r=Object.keys(t);return r.forEach(function(n){Object.defineProperty(e,n,{get:function(){return t[n]},set:function(e){return t[n]=e,e}})}),e}catch(o){i([o],n)}for(var a in t)l.call(t,a)&&(e[a]=t[a]);return e}function a(t){return!(t&&t instanceof Function&&t.apply&&!t[p])}function c(t,e){var n=e(t);return n[p]=t,o(t,n,s),n}function f(t,e,n){var r=t[e];t[e]=c(r,n)}function u(){for(var t=arguments.length,e=new Array(t),n=0;n<t;++n)e[n]=arguments[n];return e}var s=t("ee"),d=t(10),p="nr@original",l=Object.prototype.hasOwnProperty,v=!1;e.exports=r,e.exports.wrapFunction=c,e.exports.wrapInPlace=f,e.exports.argsToArray=u},{}]},{},["loader"]);</script>
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <!-- Bootstrap core CSS -->
    <link href='https://fonts.googleapis.com/css?family=Ubuntu:400,400italic,700' rel='stylesheet' type='text/css'>
    <!-- HTML5 shim and Respond.js for IE8 support of HTML5 elements and media queries -->
    <!--[if lt IE 9]>
  <script src="https://oss.maxcdn.com/html5shiv/3.7.2/html5shiv.min.js"></script>
  <script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
<![endif]-->
</head>
<body class="Pagina wp-login h-100">

<%--<iframe width="100%" height="10000"  scrolling="no" seamless="yes" src="http://fabio8933.wixsite.com/ilitera-inteligente"></iframe>
                        <asp:textbox ID="txtUsuario" CssClass="form-control" placeholder="Preencha seu usuário" runat="server" MaxLength="25" Visible="false"></asp:textbox>
                        <asp:textbox ID="txtSenha" CssClass="form-control" runat="server" placeholder="Preencha sua senha" TextMode="Password" MaxLength="12" Visible="false"></asp:textbox>--%>

    <script src='https://www.google.com/recaptcha/api.js' async defer ></script>  


    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <section class="topo-container d-flex justify-content-center">
        <div class="container">
            <div class="row mt-5">
                <div class="col d-flex justify-content-center align-items-center">
                    <img src="Images/logo-ilitera-branco.svg" class="Logo p-5"/>
                </div>
                <div class="col">
                    <%--card--%>
                    <div class="card w-75 d-flex " style="background: #FFFFFF; box-shadow: 0px 8px 24px 0px rgba(0, 0, 0, 0.25); border: none;">
                        <div class="card-body col">
                            <div class="row mb-4">
                                <img src="images/caixinha.png" class="img-fluid" style="padding-left: 0!important; padding-right: 0!important;"/>
                            </div>
                            <div class="row d-flex flex-col align-items-start my-3">
                                <label class="control-label1 text-start mb-1" for="Textbox1">Usuário</label>
                                <asp:textbox ID="txtUsuario" CssClass="form-control" placeholder="Preencha seu usuário" runat="server" MaxLength="25"></asp:textbox>
                            </div>
                            <div class="row d-flex flex-col align-items-start my-3">
                                <label class="control-label1 text-start mb-1" for="Textbox2">Senha</label>
                                <asp:textbox ID="txtSenha" CssClass="form-control" runat="server" placeholder="Preencha sua senha"  TextMode="Password" MaxLength="12"></asp:textbox>
                            </div>
                            <div class="d-flex text-left mt-3 mb-4">
                                <asp:LinkButton CssClass="btn-link" ID="lnkLembrarSenha" runat="server" text="Esqueceu sua Senha? clique aqui" onclick="lnkLembrarSenha_Click"></asp:LinkButton>
                            </div>
                            <div class="row text-left pl-4">
                                <div class="g-recaptcha" data-sitekey="6Ld_cqoUAAAAABhSYhWfqLfnqpTqM1Z32Q7RJHL3" style="transform:scale(0.77);-webkit-transform:scale(0.77);transform-origin:0 0;-webkit-transform-origin:0 0;"></div>
                            </div>
                            <div class="row my-2">
                           <asp:Button ID="btnSAML" CssClass="btn1" runat="server" Text="Login - SCJ" OnClick="btnJohnson_Click" />
                            </div>
                            <div class="row mb-3">
                            <asp:Button ID="btnLogar" runat="server" CssClass="btn1" Text="Entrar" OnClick="btnLogar_Click"  />   
                                <button type="button" class="btn btn-link"></button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!--
        <div class="container d-flex flex-column flex-sm-row my-0 justify-content-center">
            <div class="row topo mt-5">
                <div class="col-12 col-md-6 align-self-center text-left justify-content-center">
                    <div class="row ">
                        <img src="Images/logo.svg" class="Logo pb-3"/>
                        <h1 class="Titulo1">Gestão Inteligente</h1>
                        <h2 class="Subtitulo">Segurança e Saúde no Trabalho</h2>
                    </div>
                </div>

                <%--card--%>

                <div class="col-12 col-md-6">
                    <div class="card w-75 d-flex " style="background: #f1f1f1; box-shadow: 0px 8px 24px 0px rgba(0, 0, 0, 0.25); border: none;">
                        <div class="card-body col">
                            <div class="row mb-4">
                                <img src="images/caixinha.png" class="img-fluid" style="padding-left: 0!important; padding-right: 0!important;"/>
                            </div>
                            <div class="row d-flex flex-col align-items-start my-3">
                                <label class="control-label1 text-start" for="Textbox1">Usuário</label>
                                <asp:textbox ID="txtUsuario1" CssClass="form-control" placeholder="Preencha seu usuário" runat="server" MaxLength="25"></asp:textbox>
                            </div>
                            <div class="row d-flex flex-col align-items-start my-3">
                                <label class="control-label1 text-start" for="Textbox2">Senha</label>
                                <asp:textbox ID="txtSenha1" CssClass="form-control" runat="server" placeholder="Preencha sua senha"  TextMode="Password" MaxLength="12"></asp:textbox>
                            </div>
                            <div class="d-flex text-left mt-3 mb-4">
                                <asp:LinkButton CssClass="btn-link" ID="lnkLembrarSenha1" runat="server" text="Esqueceu sua Senha? clique aqui"></asp:LinkButton>
                            </div>
                            <div class="row text-left pl-4">
                                <div class="g-recaptcha" data-sitekey="6Ld_cqoUAAAAABhSYhWfqLfnqpTqM1Z32Q7RJHL3" style="transform:scale(0.77);-webkit-transform:scale(0.77);transform-origin:0 0;-webkit-transform-origin:0 0;"></div>
                            </div>
                            <div class="row my-2">
                                <asp:Button ID="btnSAML1" CssClass="btn1" runat="server" Text="Login - SCJ"/>
                            </div>
                            <div class="row">
                                <asp:Button ID="btnLogar1" runat="server" CssClass="btn1" Text="Entrar" /> 
                                <button type="button" class="btn btn-link"></button>
                            </div>
                        </div>
                    </div>
                  </div>
             </div>
        </div>
        -->
    </section>
   
        <%--<div class="row row-titulo">
            <div class="col-md-4 col-sm-offset-2 col-sm-8 col-md-offset-4">
                <div class="contem-titulo-acesso-login">
                    <h2 class="titulo-secundario titulo-contato">&nbsp;</h2>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-4">
                <div class="balao">
                    <h3 class="balao-titulo"><span></span> </h3>
                    <p></p>
                    <h3 class="balao-titulo"><span></span> </h3>
                    <p></p>
                    <h3 class="balao-titulo"><span></span> </h3>
                    <p></p>
                </div>
            </div>
            <div class="col-md-4">
                <div class="balao">
                    <h3 class="balao-titulo"><span></span> </h3>
                    <p></p>
                    <h3 class="balao-titulo"><span></span> </h3>
                    <p></p>
                    <h3 class="balao-titulo"><span></span> </h3>
                    <p></p>
                </div>
            </div>
            <div class="col-md-4">
                <div class="balao">
                    <h3 class="balao-titulo"><span></span></h3>
                    <p></p>
                    <h3 class="balao-titulo"><span></span></h3>
                    <p></p>
                    <h3 class="balao-titulo"><span></span></h3>
                    <p></p>
                </div>
            </div>
        </div>--%>
        <div class="row">
            <div class="col-md-12">
                <asp:Label ID="lblScriptErro" runat="server"></asp:Label>
                <%--<asp:Label ID="lblAviso" runat="server" Font-Bold="True" Font-Size="Medium" ForeColor="Red">INFORMATIVO - ATENÇÃO AOS AGENDAMENTOS DE ATENDIMENTO MÉDICO (ESTADO DE SÃO PAULO) NOS DIA 08 E 09/07 EM RAZÃO DO FERIADO ESTADUAL EM 09/07 - REVOLUÇÃO CONSTITUCIONALISTA</asp:Label>--%>
            </div>
        </div>
    </div>
    <!-- Bootstrap core JavaScript
================================================== -->
    <!-- Placed at the end of the document so the pages load faster -->
    <script src="assets/js/jquery.min.js"></script>
    <script src="bootstrap/js/bootstrap.min.js"></script>
    <!-- IE10 viewport hack for Surface/desktop Windows 8 bug -->
    <script src="assets/js/ie10-viewport-bug-workaround.js"></script>

        <!--<div class="g-recaptcha" data-sitekey="6Ld_cqoUAAAAABhSYhWfqLfnqpTqM1Z32Q7RJHL3" data-callback="onSubmit" data-size="invisible"></div> -->

<%--        <eo:MsgBox ID="MsgBox1" runat="server" BackColor="#47729F" ControlSkinID="None"
        HeaderHtml="Dialog Title" Height="100px" Width="250px"  >
        <HeaderStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #ffbf00 3px solid; padding-left: 4px; font-weight: bold; font-size: 11px; padding-bottom: 2px; color: white; padding-top: 2px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <ContentStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <FooterStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
        </eo:MsgBox>--%>

        </form>


    

<script type="text/javascript">window.NREUM || (NREUM = {}); NREUM.info = { "beacon": "bam.nr-data.net", "licenseKey": "aece2c08f5", "applicationID": "543213780", "transactionName": "ZgMBMkBYDRcCARVQC19JNhRbFgoKBwcZFxRZFg==", "queueTime": 0, "applicationTime": 159, "atts": "SkQCRAhCHhk=", "errorBeacon": "bam.nr-data.net", "agent": "" }</script></body>
</html>