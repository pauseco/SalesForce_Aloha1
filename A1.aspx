<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="A1.aspx.cs" Inherits="SF_A1.A1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        #TextArea1 {
            height: 45px;
            width: 300px;
        }
    </style>
</head>
<body>
<form id="form1" runat="server">

    <div>
        <h1>Aloha1</h1>
        <p class="lead">Exercise - Aloha 1</p>
    </div>

    <div class="row">
        <div class="col-md-4">
            <h2>Input</h2>
            <div style="width:50%">
                &nbsp;<textarea id="InputTextArea" name="S1" runat="server"></textarea><br />
                &nbsp;<asp:Button ID="SendBtn" runat="server" OnClick="SendBtn_Click" Text="Send!" />
            </div>
            <div style="width:50%">
              <h2>Output</h2>
                <br />
                <div id="divOutput" style="display: none;"></div>
                <textarea id="OutputTextArea" cols="20" name="S2" rows="2" runat="server"></textarea></div>
        </div>
    </div>
</form>
</body>
</html>
