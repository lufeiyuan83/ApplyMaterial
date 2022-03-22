<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WorkFlow.aspx.cs" Inherits="ERP.WorkFlow.WorkFlow" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>工作流设计器</title>
    <script src="../Scripts/konva.min.js"></script>
    <script src="../Scripts/ace.js"></script>
    <script src="../Scripts/semantic.min.js"></script>
    <script src="../Scripts/workflowdesigner.min.js"></script>
    <script src="../Scripts/json5.js"></script>
    <script src="../Scripts/jquery.auto-complete.min.js"></script></head>
<body>
    <form id="form1" runat="server">
        <div id="wfdesigner" style="min-height:600px"></div>
    </form>
</body>
    <script>
        var QueryString = function () {
            // This function is anonymous, is executed immediately and
            // the return value is assigned to QueryString!
            var query_string = {};
            var query = window.location.search.substring(1);
            var vars = query.split("&");
            for (var i = 0; i < vars.length; i++) {
                var pair = vars[i].split("=");
                // If first entry with this name
                if (typeof query_string[pair[0]] === "undefined") {
                    query_string[pair[0]] = pair[1];
                    // If second entry with this name
                } else if (typeof query_string[pair[0]] === "string") {
                    var arr = [query_string[pair[0]], pair[1]];
                    query_string[pair[0]] = arr;
                    // If third or later entry with this name
                } else {
                    query_string[pair[0]].push(pair[1]);
                }
            }
            return query_string;
        }();

        var schemecode = 'SimpleWF';
        var processid = QueryString.processid;
        var graphwidth = 1200;
        var graphminheight = 600;
        var graphheight = graphminheight;

        var wfdesigner = undefined;
        function wfdesignerRedraw() {
            var data;

            if (wfdesigner != undefined) {
                data = wfdesigner.data;
                wfdesigner.destroy();
            }

            WorkflowDesignerConstants.FormMaxHeight = 600;
            wfdesigner = new WorkflowDesigner({
                name: 'simpledesigner',
                apiurl: '<%= Page.ResolveUrl("~/Designer/API") %>',
                renderTo: 'wfdesigner',
                imagefolder: '<%= Page.ResolveUrl("~/Images/") %>',
                graphwidth: graphwidth,
                graphheight: graphheight
            });

            if (data == undefined) {
                var isreadonly = false;
                if (processid != undefined && processid != '')
                    isreadonly = true;

                var p = { schemecode: schemecode, processid: processid, readonly: isreadonly };
                if (wfdesigner.exists(p))
                    wfdesigner.load(p);
                else
                    wfdesigner.create(schemecode);
            }
            else {
                wfdesigner.data = data;
                wfdesigner.render();
            }
        }

       $(window).resize(function () {
            if (window.wfResizeTimer) {
                clearTimeout(window.wfResizeTimer);
                window.wfResizeTimer = undefined;
            }
            window.wfResizeTimer = setTimeout(function () {
                var w = $(window).width();
                var h = $(window).height();

                if (w > 300)
                    graphwidth = w - 40;

                if (h > 300)
                    graphheight = h - 250;

                if (graphheight < graphminheight)
                    graphheight = graphminheight;

                wfdesignerRedraw();
            }, 150);

        });

        $(window).resize();

    function DownloadScheme(){
        wfdesigner.downloadscheme();
    }

    function DownloadSchemeBPMN() {
        wfdesigner.downloadschemeBPMN();
    }

    var selectSchemeType;
    function SelectScheme(type) {
        if (type)
            selectSchemeType = type;

        var file = $('#uploadFile');
        file.trigger('click');
    }

    function UploadScheme() {

        if (selectSchemeType == "bpmn") {
            wfdesigner.uploadschemeBPMN($('form')[0], function () {
                wfdesigner.autoarrangement();
                alert('The file is uploaded!');
            });
        }
        else {
            wfdesigner.uploadscheme($('form')[0], function () {
                alert('The file is uploaded!');
            });
        }
    }

    function OnSave() {
        wfdesigner.schemecode = schemecode;

        var err = wfdesigner.validate();
        if (err != undefined && err.length > 0) {
            alert(err);
        }
        else {
            wfdesigner.save(function () {
                alert('The scheme is saved!');
            });
        }
    }
    function OnNew() {
        wfdesigner.create();
    }
    </script>
</html>
