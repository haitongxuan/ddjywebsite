<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="lbs_shop_edit.aspx.cs" Inherits="DTcms.Web.admin.weixin.lbs_shop_edit" ValidateRequest="false" %>
<%@ Import namespace="DTcms.Common" %>

<!DOCTYPE html>
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta name="viewport" content="width=device-width,minimum-scale=1.0,maximum-scale=1.0,initial-scale=1.0,user-scalable=no" />
<meta name="apple-mobile-web-app-capable" content="yes" />
<title>编辑商店位置</title>
<link href="../../scripts/artdialog/ui-dialog.css" rel="stylesheet" type="text/css" />
<link href="../skin/default/style.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" charset="utf-8" src="../../scripts/jquery/jquery-1.11.2.min.js"></script>
<script type="text/javascript" charset="utf-8" src="../../scripts/jquery/Validform_v5.3.2_min.js"></script>
<script type="text/javascript" charset="utf-8" src="../../scripts/artdialog/dialog-plus-min.js"></script>
<script type="text/javascript" charset="utf-8" src="../../scripts/webuploader/webuploader.min.js"></script>
<script type="text/javascript" charset="utf-8" src="../js/uploader.js"></script>
<script type="text/javascript" charset="utf-8" src="../js/laymain.js"></script>
<script type="text/javascript" charset="utf-8" src="../js/common.js"></script>
<script type="text/javascript">
    $(function () {
        //初始化表单验证
        $("#form1").initValidform();
        //初始化上传控件
        $(".upload-img").InitUploader({ filesize: "<%=siteConfig.imgsize %>", sendurl: "../../tools/upload_ajax.ashx", swf: "../../scripts/webuploader/uploader.swf", filetypes: "<%=siteConfig.fileextension %>" });
        //初始化地图按钮
        $("#itemAddButton").click(function () {
            var d = top.dialog({
                width: 550,
                height: 350,
                title: '标注位置',
                url: 'dialog/dialog_map.aspx'
            }).showModal();
            d.data = $(this);
        });
    });
</script>
</head>

<body class="mainbody">
<form id="form1" runat="server">
<!--导航栏-->
<div class="location">
  <a href="lbs_shop_list.aspx" class="back"><i></i><span>返回列表页</span></a>
  <a href="../center.aspx" class="home"><i></i><span>首页</span></a>
  <i class="arrow"></i>
  <span>应用管理</span>
  <i class="arrow"></i>
  <span>微信管理</span>
  <i class="arrow"></i>
  <span>编辑店铺位置数据</span>
</div>
<div class="line10"></div>
<!--/导航栏-->

<!--内容-->
<div id="floatHead" class="content-tab-wrap">
  <div class="content-tab">
    <div class="content-tab-ul-wrap">
      <ul>
        <li><a class="selected" href="javascript:;">店铺位置数据</a></li>
      </ul>
    </div>
  </div>
</div>

<div class="tab-content">
  <dl>
    <dt>公众账户</dt>
    <dd>
      <div class="rule-single-select">
        <asp:DropDownList ID="ddlAccountId" runat="server" datatype="*" errormsg="请选择公众账户！" sucmsg=" " />
       </div>
      <span class="Validform_checktip">*当前所属的公众账户。</span>
    </dd>
  </dl>
  <dl>
    <dt>排序数字</dt>
    <dd>
      <asp:TextBox ID="txtSortId" runat="server" CssClass="input small" datatype="n" sucmsg=" ">99</asp:TextBox>
      <span class="Validform_checktip">*数字，越小越向前</span>
    </dd>
  </dl>
  <dl>
    <dt>店铺名称</dt>
    <dd>
      <asp:TextBox ID="txtName" runat="server" CssClass="input normal" datatype="*1-255" sucmsg=" "></asp:TextBox>
      <span class="Validform_checktip">*店铺名称，最多255个字符</span>
    </dd>
  </dl>
  <dl>
    <dt>联系电话</dt>
    <dd>
      <asp:TextBox ID="txtTelphone" runat="server" CssClass="input normal" datatype="*1-30" sucmsg=" "></asp:TextBox>
      <span class="Validform_checktip">*</span>
    </dd>
  </dl>
  <dl>
    <dt>店铺简介</dt>
    <dd>
      <asp:TextBox ID="txtBrief" runat="server" CssClass="input" TextMode="MultiLine" datatype="*0-500" sucmsg=" "></asp:TextBox>
      <span class="Validform_checktip">*非必填，500字符以内</span>
    </dd>
  </dl>
  <dl>
    <dt>封面图片</dt>
    <dd>
      <asp:TextBox ID="txtLogo" runat="server" CssClass="input normal upload-path" />
      <div class="upload-box upload-img"></div>
      <span class="Validform_checktip">(尺寸:720×400像素)小于500KB</span>
    </dd>
  </dl>
  <dl>
    <dt>详细地址</dt>
    <dd>
      <asp:TextBox ID="txtAddress" runat="server" CssClass="input normal" datatype="*0-500" sucmsg=" "></asp:TextBox>
      <span class="Validform_checktip">这只是模糊定位，准确定位在地图上标注</span>
    </dd>
  </dl>
  <dl>
    <dt>地图标签</dt>
    <dd>
       <a id="itemAddButton" class="icon-btn add"><i></i><span>打开地图</span></a>
    </dd>
  </dl>
  <dl>
    <dt>纬度(x)</dt>
    <dd>
      <asp:TextBox ID="txtXPoint" runat="server"  CssClass="input" datatype="*1-20" sucmsg=" " nullmsg="请在地图上标注纬度"></asp:TextBox>
      <span class="Validform_checktip">*请在地图上标注纬度(x)值</span>
    </dd>
  </dl>
  <dl>
    <dt>经度(y)</dt>
    <dd>
      <asp:TextBox ID="txtYPoint" runat="server"  CssClass="input" datatype="*1-20" sucmsg=" " nullmsg="请在地图上标注经度"></asp:TextBox>
      <span class="Validform_checktip">*请在地图上标注经度(y)值</span>
    </dd>
  </dl>
  <dl>
    <dt>外部链接</dt>
    <dd>
      <asp:TextBox ID="txtLinkUrl" runat="server" CssClass="input normal " datatype="*0-255" sucmsg=" "></asp:TextBox>
      <span class="Validform_checktip">*请填写网址链接</span>
    </dd>
  </dl>
</div>
<!--/内容-->

<!--工具栏-->
<div class="page-footer">
  <div class="btn-wrap">
    <asp:Button ID="btnSubmit" runat="server" Text="提交保存" CssClass="btn" onclick="btnSubmit_Click" />
    <input name="btnReturn" type="button" value="返回上一页" class="btn yellow" onclick="javascript:history.back(-1);" />
  </div>
</div>
<!--/工具栏-->

</form>
</body>
</html>
