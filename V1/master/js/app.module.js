/*!
 * 
 * Angle - Bootstrap Admin App + AngularJS Material
 * 
 * Version: 3.1.0
 * Author: @themicon_co
 * Website: http://themicon.co
 * License: https://wrapbootstrap.com/help/licenses
 * 
 */

// APP START
// ----------------------------------- 

(function() {
    'use strict';

    angular
        .module('angle', [
            'app.core',
            'app.routes',
            'app.sidebar',
            'app.navsearch',
            'app.preloader',
            'app.loadingbar',
            'app.translate',
            'app.settings',
            'app.dashboard',
            'app.icons',
            'app.flatdoc',
            'app.notify',
            'app.bootstrapui',
            'app.elements',
            'app.panels',
            'app.charts',
            'app.forms',
            'app.locale',
            'app.maps',
            'app.pages',
            'app.tables',
            'app.extras',
            'app.mailbox',
            'app.utils',
            'app.material',
            'froala'
        ]).value('froalaConfig', {
            toolbarInline: false,
            documentReady: true,
            toolbarButtons: ['fullscreen', 'bold', 'italic', 'underline', 'strikeThrough', 'subscript', 'superscript', 'fontFamily', 'fontSize', '|', 'color', 'emoticons', 'inlineStyle', 'paragraphStyle', '|', 'paragraphFormat', 'align', 'formatOL', 'formatUL', 'outdent', 'indent', '-', 'insertLink', 'insertImage', 'insertVideo', 'insertFile', 'insertTable', '|', 'quote', 'insertHR', 'undo', 'redo', 'clearFormatting', 'selectAll', 'html'],

            imageAllowedTypes: ["jpeg", "jpeg;base64", "jpg", "jpg;base64", "png", "png;base64", "gif", "gif;base64", "webp", "webp;base64"],
            events: {
                'image.beforePasteUpload': function (files) {

                    // Do something here.
                    // this is files editor instance.
                    console.log(files);
                },
                'image.error': function (error, response) {
                    // Do something here.
                    // this is the editor instance.
                    console.log(error);
                }
            },
            //pastedImagesUploadURL: 'http://localhost/WebApplication1/home/Pasteimage',
            imageUploadURL: 'http://localhost/WebApplication1/home/UploadImage',
            //            imageUploadToS3: {
            //    bucket: 'mailimageuploads',
            //    // Your bucket region.
            //    region: 'us-east-1',
            //    keyStart: 'uploads/test.jpg',
            //    params: {
            //      acl: 'Public-Read', // ACL according to Amazon Documentation.
            //      AWSAccessKeyId: 'AKIATLKVFRS7KDZXJHRN', // Access Key from Amazon.
            //      AWSSecretAccessKey:'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',
            //      policy: '', // Policy string computed in the backend.
            //      signature: '', // Signature computed in the backend.

            //      // If you are using Amazon Signature V4, the followings should be used instead.
            //      // "X-Amz-Credential": "...",
            //      // "X-Amz-Algorithm": "AWS4-HMAC-SHA256",
            //      // "X-Amz-Date": "...",
            //      // Policy: "...", //
            //      // "X-Amz-Signature": "", // computed in backend
            //    }
            //  }
        });

    angular.module('angle').config(['$httpProvider', function ($httpProvider) {
        $httpProvider.interceptors.push(function ($location) {
            return {
                responseError: function (req) {
                    console.log(req);
                    if (req.status == "401")
                    {
                        $location.url('page/401?errno=401');
                        location.reload();
                    }
                    else if (req.status == "500")
                    {
                        unlockUI();
                        $location.url('page/500?errno=500');
                    }
                    else {
                        unlockUI();
                        $location.url('page/404?errno=404');
                    }
                    return req;
               }
            };
        });
    }]);
})();

