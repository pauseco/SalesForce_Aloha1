<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SF_A1._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        var Command = {};
        Command.OcrTimer = null;
        Command.OcrId = 0;
        Command.CurrentFileName = null;
        Command.OcrStaedOn = null;
        Command.OcrMaxSecondToWait = 35;
        Command.DocumentTypeId = 0;

        btnSend = function () {
            alert(2);

        };
    </script>
    <div>
        <h1>Aloha1</h1>
        <p class="lead">blabla</p>
    </div>

    <div class="row">
        <div class="col-md-4">
            <h2>Input</h2>
            <div style="width:50%">
                <textarea id="inputArea" ></textarea>
                <br />
                <input type="button" id="btnSend" value="Send" onclick="btnSend();" />
            </div>
            <div style="width:50%">
              <h2>Output</h2>
                <br />
                <div id="divOutput" style="display: none;"></div>
            </div>
        </div>
    </div>
</asp:Content>
