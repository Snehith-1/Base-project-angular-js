(function ($) {
    $.setReportView = function (option) {
        var setting = $.extend({
            urlmrt: '',
            displayshow: '',
            setreport: function (r, v) { }
        }, option);


        var options = new Stimulsoft.Viewer.StiViewerOptions();
        options.height = "100%";
        options.appearance.scrollbarsMode = true;
        options.toolbar.showDesignButton = false;
        options.toolbar.showFullScreenButton = false;
        options.toolbar.showAboutButton = false;
        options.toolbar.printDestination = Stimulsoft.Viewer.StiPrintDestination.Direct;
        options.appearance.htmlRenderMode = Stimulsoft.Report.Export.StiHtmlExportMode.Table;
        //options.toolbar.zoom = '90';
        options.toolbar.zoom = Stimulsoft.Viewer.StiZoomMode.PageWidth;

        var viewer = new Stimulsoft.Viewer.StiViewer(options, "StiViewer", false);
        viewer.renderHtml(setting.displayshow);
        viewer.showProcessIndicator();

        var report = new Stimulsoft.Report.StiReport();
        report.loadFile(setting.urlmrt);
        report.dictionary.databases.clear();

        setting.setreport(report, viewer);
    };

}(jQuery));