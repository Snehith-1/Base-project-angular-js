/**=========================================================
 * Module: config.js
 * App routes and resources configuration
 =========================================================*/


 (function () {
    'use strict';

    angular
        .module('app.routes')
        .config(routesConfig);

    routesConfig.$inject = ['$stateProvider', '$locationProvider', '$urlRouterProvider', 'RouteHelpersProvider'];
    function routesConfig($stateProvider, $locationProvider, $urlRouterProvider, helper) {

        // Set the following to true to enable the HTML5 Mode
        // You may have to set <base> tag in index and a routing configuration in your server
        $locationProvider.html5Mode(false);

        // defaults to dashboard
        $urlRouterProvider.otherwise('/page/user_login');

        // version update
        var version = '1.0';

        // 
        // Application Routes
        // -----------------------------------   
        $stateProvider
        
        .state('app.MstCadDeferralStatus', {
            url: '/MstCadDeferralStatus',
            title: 'MstCadDeferralStatus',
            templateUrl: helper.basepath('ems.master/MstCadDeferralStatus.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.MstPostCcActivitiesRMView', {
            url: '/MstPostCcActivitiesRMView',
            title: 'MstPostCcActivitiesRMView',
            templateUrl: helper.basepath('ems.master/MstPostCcActivitiesRMView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.MstCadDeferralQuery', {
            url: '/MstCadDeferralQuery',
            title: 'MstCadDeferralQuery',
            templateUrl: helper.basepath('ems.master/MstCadDeferralQuery.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.MstCadDeferralGuarantorDtls', {
            url: '/MstCadDeferralGuarantorDtls',
            title: 'MstCadDeferralGuarantorDtls',
            templateUrl: helper.basepath('ems.master/MstCadDeferralGuarantorDtls.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.MstCadDeferralDochecklist', {
            url: '/MstCadDeferralDochecklist',
            title: 'MstCadDeferralDochecklist',
            templateUrl: helper.basepath('ems.master/MstCadDeferralDochecklist.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.MstRMSanctionSummary', {
            url: '/MstRMSanctionSummary',
            title: 'MstRMSanctionSummary',
            templateUrl: helper.basepath('ems.master/MstRMSanctionSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.MstRMDocChecklistDtls', {
            url: '/MstRMDocChecklistDtls',
            title: 'MstRMDocChecklistDtls',
            templateUrl: helper.basepath('ems.master/MstRMDocChecklistDtls.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.MstRMDeferralDtls', {
            url: '/MstRMDeferralDtls',
            title: 'MstRMDeferralDtls',
            templateUrl: helper.basepath('ems.master/MstRMDeferralDtls.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.MstRMDeferralDtlsView', {
            url: '/MstRMDeferralDtlsView',
            title: 'MstRMDeferralDtlsView',
            templateUrl: helper.basepath('ems.master/MstRMDeferralDtlsView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.MstRMDeferralCloseQuery', {
            url: '/MstRMDeferralCloseQuery',
            title: 'MstRMDeferralCloseQuery',
            templateUrl: helper.basepath('ems.master/MstRMDeferralCloseQuery.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.MstCadLSADtlSummary', {
            url: '/MstCadLSADtlSummary',
            title: 'MstCadLSADtlSummary',
            templateUrl: helper.basepath('ems.master/MstCadLSADtlSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.MstCadGenerateLSA', {
            url: '/MstCadGenerateLSA',
            title: 'MstCadGenerateLSA',
            templateUrl: helper.basepath('ems.master/MstCadGenerateLSA.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        }) 

        .state('app.MstCadPhysicalDocSummary', {
            url: '/MstCadPhysicalDocSummary',
            title: 'MstCadPhysicalDocSummary',
            templateUrl: helper.basepath('ems.master/MstCadPhysicalDocSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.MstCadPhysicalDocCheckerSummary', {
            url: '/MstCadPhysicalDocCheckerSummary',
            title: 'MstCadPhysicalDocCheckerSummary',
            templateUrl: helper.basepath('ems.master/MstCadPhysicalDocCheckerSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.MstCadPhysicalDocApprovalSummary', {
            url: '/MstCadPhysicalDocApprovalSummary',
            title: 'MstCadPhysicalDocApprovalSummary',
            templateUrl: helper.basepath('ems.master/MstCadPhysicalDocApprovalSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.MstCadPhysicalDocGuarantorDtls', {
            url: '/MstCadPhysicalDocGuarantorDtls',
            title: 'MstCadPhysicalDocGuarantorDtls',
            templateUrl: helper.basepath('ems.master/MstCadPhysicalDocGuarantorDtls.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.MstCadPhysicalDochecklist', {
            url: '/MstCadPhysicalDochecklist',
            title: 'MstCadPhysicalDochecklist',
            templateUrl: helper.basepath('ems.master/MstCadPhysicalDochecklist.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.MstCadPhysicalDocQuery', {
            url: '/MstCadPhysicalDocQuery',
            title: 'MstCadPhysicalDocQuery',
            templateUrl: helper.basepath('ems.master/MstCadPhysicalDocQuery.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.MstCadPhysicalDocStatus', {
            url: '/MstCadPhysicalDocStatus',
            title: 'MstCadPhysicalDocStatus',
            templateUrl: helper.basepath('ems.master/MstCadPhysicalDocStatus.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.MstRMInitiateWaiverSummary', {
            url: '/MstRMInitiateWaiverSummary',
            title: 'MstRMInitiateWaiverSummary',
            templateUrl: helper.basepath('ems.master/MstRMInitiateWaiverSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.MstCreateWaiver', {
            url: '/MstCreateWaiver',
            title: 'MstCreateWaiver',
            templateUrl: helper.basepath('ems.master/MstCreateWaiver.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.MstRMInitiateWaiverView', {
            url: '/MstRMInitiateWaiverView',
            title: 'MstRMInitiateWaiverView',
            templateUrl: helper.basepath('ems.master/MstRMInitiateWaiverView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.MstRMWaiverApprovalView', {
            url: '/MstRMWaiverApprovalView',
            title: 'MstRMWaiverApprovalView',
            templateUrl: helper.basepath('ems.master/MstRMWaiverApprovalView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.MstRMWaiverApprovalHistoryView', {
            url: '/MstRMWaiverApprovalHistoryView',
            title: 'MstRMWaiverApprovalHistoryView',
            templateUrl: helper.basepath('ems.master/MstRMWaiverApprovalHistoryView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.MstDeferralMyApproval', {
            url: '/MstDeferralMyApproval',
            title: 'MstDeferralMyApproval',
            templateUrl: helper.basepath('ems.master/MstDeferralMyApproval.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.MstDeferralMyApprovalHistory', {
            url: '/MstDeferralMyApprovalHistory',
            title: 'MstDeferralMyApprovalHistory',
            templateUrl: helper.basepath('ems.master/MstDeferralMyApprovalHistory.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.MstScannedDeferralHistory', {
            url: '/MstScannedDeferralHistory',
            title: 'MstScannedDeferralHistory',
            templateUrl: helper.basepath('ems.master/MstScannedDeferralHistory.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.MstScannedCompletedSummary', {
            url: '/MstScannedCompletedSummary',
            title: 'MstScannedCompletedSummary',
            templateUrl: helper.basepath('ems.master/MstScannedCompletedSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.ScannedCompleted', {
            url: '/ScannedCompleted',
            title: 'ScannedCompleted',
            templateUrl: helper.basepath('ems.master/ScannedCompleted.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })



        .state('app.MstPSLCSAManagement', {
            url: '/MstPSLCSAManagement',
            title: 'MstPSLCSAManagement',
            templateUrl: helper.basepath('ems.master/MstPSLCSAManagement.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')
        })  

        .state('app.MstPSLCSAGuarantorDetails', {
            url: '/MstPSLCSAGuarantorDetails',
            title: 'MstPSLCSAGuarantorDetails',
            templateUrl: helper.basepath('ems.master/MstPSLCSAGuarantorDetails.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')
        })

        .state('app.MstPSLCSADataFlaggingAdd', {
            url: '/MstPSLCSADataFlaggingAdd',
            title: 'MstPSLCSADataFlaggingAdd',
            templateUrl: helper.basepath('ems.master/MstPSLCSADataFlaggingAdd.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')
        })

        .state('app.MstPSLCSAIndividualPSLDataFlagAdd', {
            url: '/MstPSLCSAIndividualPSLDataFlagAdd',
            title: 'MstPSLCSAIndividualPSLDataFlagAdd',
            templateUrl: helper.basepath('ems.master/MstPSLCSAIndividualPSLDataFlagAdd.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')
        })

        .state('app.MstPSLCSAGroupPSLDataFlagAdd', {
            url: '/MstPSLCSAGroupPSLDataFlagAdd',
            title: 'MstPSLCSAGroupPSLDataFlagAdd',
            templateUrl: helper.basepath('ems.master/MstPSLCSAGroupPSLDataFlagAdd.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')
        })

        .state('app.MstPSLCSADataFlaggingEdit', {
            url: '/MstPSLCSADataFlaggingEdit',
            title: 'MstPSLCSADataFlaggingEdit',
            templateUrl: helper.basepath('ems.master/MstPSLCSADataFlaggingEdit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')
        })

        .state('app.MstPSLCSAIndividualPSLDataFlagEdit', {
            url: '/MstPSLCSAIndividualPSLDataFlagEdit',
            title: 'MstPSLCSAIndividualPSLDataFlagEdit',
            templateUrl: helper.basepath('ems.master/MstPSLCSAIndividualPSLDataFlagEdit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')
        })

        .state('app.MstPSLCSAGroupPSLDataFlagEdit', {
            url: '/MstPSLCSAGroupPSLDataFlagEdit',
            title: 'MstPSLCSAGroupPSLDataFlagEdit',
            templateUrl: helper.basepath('ems.master/MstPSLCSAGroupPSLDataFlagEdit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')
        })

        .state('app.MstPSLCSAComplete', {
            url: '/MstPSLCSAComplete',
            title: 'MstPSLCSAComplete',
            templateUrl: helper.basepath('ems.master/MstPSLCSAComplete.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')
        })       
     
        .state('app.AtmMstCheckpointAdd', {
            url: '/AtmMstCheckpointAdd',
            title: 'AtmMstCheckpointAdd',
            templateUrl: helper.basepath('ems.audit/AtmMstCheckpointAdd.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.AtmMstCheckpointEdit', {
            url: '/AtmMstCheckpointEdit',
            title: 'AtmMstCheckpointEdit',
            templateUrl: helper.basepath('ems.audit/AtmMstCheckpointEdit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')

        })

        .state('app.AtmMstCheckpointView', {
            url: '/AtmMstCheckpointView',
            title: 'AtmMstCheckpointView',
            templateUrl: helper.basepath('ems.audit/AtmMstCheckpointView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.AtmTrnAuditorApproverSummary', {
            url: '/AtmTrnAuditorApproverSummary',
            title: 'AtmTrnAuditorApproverSummary',
            templateUrl: helper.basepath('ems.audit/AtmTrnAuditorApproverSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')

        })

        .state('app.AtmTrnAuditorCheckerSummary', {
            url: '/AtmTrnAuditorCheckerSummary',
            title: 'AtmTrnAuditorCheckerSummary',
            templateUrl: helper.basepath('ems.audit/AtmTrnAuditorCheckerSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.AtmTrnAuditeeCheckerSummary', {
            url: '/AtmTrnAuditeeCheckerSummary',
            title: 'AtmTrnAuditeeCheckerSummary',
            templateUrl: helper.basepath('ems.audit/AtmTrnAuditeeCheckerSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.AtmTrnAuditeeMakerSummary', {
            url: '/AtmTrnAuditeeMakerSummary',
            title: 'AtmTrnAuditeeMakerSummary',
            templateUrl: helper.basepath('ems.audit/AtmTrnAuditeeMakerSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.AtmTrnMyAuditTaskAuditeeCheckerView', {
            url: '/AtmTrnMyAuditTaskAuditeeCheckerView',
            title: 'AtmTrnMyAuditTaskAuditeeCheckerView',
            templateUrl: helper.basepath('ems.audit/AtmTrnMyAuditTaskAuditeeCheckerView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.AtmTrnAuditorApproverView', {
            url: '/AtmTrnAuditorApproverView',
            title: 'AtmTrnAuditorApproverView',
            templateUrl: helper.basepath('ems.audit/AtmTrnAuditorApproverView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.AtmTrnApproverCheckpointObservation', {
            url: '/AtmTrnApproverCheckpointObservation',
            title: 'AtmTrnApproverCheckpointObservation',
            templateUrl: helper.basepath('ems.audit/AtmTrnApproverCheckpointObservation.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.AtmTrnCheckerCheckpointObservation', {
            url: '/AtmTrnCheckerCheckpointObservation',
            title: 'AtmTrnCheckerCheckpointObservation',
            templateUrl: helper.basepath('ems.audit/AtmTrnCheckerCheckpointObservation.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.AtmTrnAuditorCheckerView', {
            url: '/AtmTrnAuditorCheckerView',
            title: 'AtmTrnAuditorCheckerView',
            templateUrl: helper.basepath('ems.audit/AtmTrnAuditorCheckerView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.AtmTrnAuditeeCheckerRaiseQuery', {
            url: '/AtmTrnAuditeeCheckerRaiseQuery',
            title: 'AtmTrnAuditeeCheckerRaiseQuery',
            templateUrl: helper.basepath('ems.audit/AtmTrnAuditeeCheckerRaiseQuery.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')

        })

        .state('app.AtmTrnAuditeeMakerRaiseQuery', {
            url: '/AtmTrnAuditeeMakerRaiseQuery',
            title: 'AtmTrnAuditeeMakerRaiseQuery',
            templateUrl: helper.basepath('ems.audit/AtmTrnAuditeeMakerRaiseQuery.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')

        })

        .state('app.AtmTrnAuditorApproverRaiseQuery', {
            url: '/AtmTrnAuditorApproverRaiseQuery',
            title: 'AtmTrnAuditorApproverRaiseQuery',
            templateUrl: helper.basepath('ems.audit/AtmTrnAuditorApproverRaiseQuery.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.AtmTrnAuditorCheckerRaiseQuery', {
            url: '/AtmTrnAuditorCheckerRaiseQuery',
            title: 'AtmTrnAuditorCheckerRaiseQuery',
            templateUrl: helper.basepath('ems.audit/AtmTrnAuditorCheckerRaiseQuery.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.AtmTrnAuditorCheckerApproval', {
            url: '/AtmTrnAuditorCheckerApproval',
            title: 'AtmTrnAuditorCheckerApproval',
            templateUrl: helper.basepath('ems.audit/AtmTrnAuditorCheckerApproval.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.AtmTrnAuditeeCheckerApproval', {
            url: '/AtmTrnAuditeeCheckerApproval',
            title: 'AtmTrnAuditeeCheckerApproval',
            templateUrl: helper.basepath('ems.audit/AtmTrnAuditeeCheckerApproval.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

	    .state('app.FndMstQuestionBankMaster', {
            url: '/FndMstQuestionBankMaster',
            title: 'FndMstQuestionBankMaster',
            templateUrl: helper.basepath('ems.foundation/FndMstQuestionBankMaster.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.FndAddQuestion', {
            url: '/FndAddQuestion',
            title: 'FndAddQuestion',
            templateUrl: helper.basepath('ems.foundation/FndAddQuestion.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

 	    .state('app.FndMstCustomerApprovingMaster', {
            url: '/FndMstCustomerApprovingMaster',
            title: 'FndMstCustomerApprovingMaster',
            templateUrl: helper.basepath('ems.foundation/FndMstCustomerApprovingMaster.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

         .state('app.FndMstCampaignTypeMaster', {
            url: '/FndMstCampaignTypeMaster',
            title: 'FndMstCampaignTypeMaster',
            templateUrl: helper.basepath('ems.foundation/FndMstCampaignTypeMaster.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

	    .state('app.FndMstCustomerMaster', {
            url: '/FndMstCustomerMaster',
            title: 'FndMstCustomerMaster',
            templateUrl: helper.basepath('ems.foundation/FndMstCustomerMaster.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

	    .state('app.FndMstCustomerMasterAdd', {
            url: '/FndMstCustomerMasterAdd',
            title: 'FndMstCustomerMasterAdd',
            templateUrl: helper.basepath('ems.foundation/FndMstCustomerMasterAdd.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

	    .state('app.FndMstCustomerMasterEdit', {
            url: '/FndMstCustomerMasterEdit',
            title: 'FndMstCustomerMasterEdit',
            templateUrl: helper.basepath('ems.foundation/FndMstCustomerMasterEdit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

	    .state('app.FndMstCustomerMasterView', {
            url: '/FndMstCustomerMasterView',
            title: 'FndMstCustomerMasterView',
            templateUrl: helper.basepath('ems.foundation/FndMstCustomerMasterView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

	    .state('app.FndMstCustomerChequeView', {
            url: '/FndMstCustomerChequeView',
            title: 'FndMstCustomerChequeView',
            templateUrl: helper.basepath('ems.foundation/FndMstCustomerChequeView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.FndEditQuestion', {
            url: '/FndEditQuestion',
            title: 'FndEditQuestion',
            templateUrl: helper.basepath('ems.foundation/FndEditQuestion.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.AtmTrnAuditorApproval', {
            url: '/AtmTrnAuditorApproval',
            title: 'AtmTrnAuditorApproval',
            templateUrl: helper.basepath('ems.audit/AtmTrnAuditorApproval.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.AtmTrnHoldAuditeeSummary', {
            url: '/AtmTrnHoldAuditeeSummary',
            title: 'AtmTrnHoldAuditeeSummary',
            templateUrl: helper.basepath('ems.audit/AtmTrnHoldAuditeeSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.AtmTrnClosedAuditeeSummary', {
            url: '/AtmTrnClosedAuditeeSummary',
            title: 'AtmTrnClosedAuditeeSummary',
            templateUrl: helper.basepath('ems.audit/AtmTrnClosedAuditeeSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.AtmTrnTaggedAuditeeSummary', {
            url: '/AtmTrnTaggedAuditeeSummary',
            title: 'AtmTrnTaggedAuditeeSummary',
            templateUrl: helper.basepath('ems.audit/AtmTrnTaggedAuditeeSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.AtmTrnCompletedAuditeeSummary', {
            url: '/AtmTrnCompletedAuditeeSummary',
            title: 'AtmTrnCompletedAuditeeSummary',
            templateUrl: helper.basepath('ems.audit/AtmTrnCompletedAuditeeSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.AtmTrnMyAuditeeCheckerSummary', {
            url: '/AtmTrnMyAuditeeCheckerSummary',
            title: 'AtmTrnMyAuditeeCheckerSummary',
            templateUrl: helper.basepath('ems.audit/AtmTrnMyAuditeeCheckerSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.AtmMstCheckpointSummary', {
            url: '/AtmMstCheckpointSummary',
            title: 'AtmMstCheckpointSummary',
            templateUrl: helper.basepath('ems.audit/AtmMstCheckpointSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.AtmTrnCheckerHoldAuditee', {
            url: '/AtmTrnCheckerHoldAuditee',
            title: 'AtmTrnCheckerHoldAuditee',
            templateUrl: helper.basepath('ems.audit/AtmTrnCheckerHoldAuditee.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.AtmTrnCheckerClosedAuditee', {
            url: '/AtmTrnCheckerClosedAuditee',
            title: 'AtmTrnCheckerClosedAuditee',
            templateUrl: helper.basepath('ems.audit/AtmTrnCheckerClosedAuditee.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.AtmTrnCheckerTaggedAuditee', {
            url: '/AtmTrnCheckerTaggedAuditee',
            title: 'AtmTrnCheckerTaggedAuditee',
            templateUrl: helper.basepath('ems.audit/AtmTrnCheckerTaggedAuditee.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.AtmTrnCheckerCompletedAuditee', {
            url: '/AtmTrnCheckerCompletedAuditee',
            title: 'AtmTrnCheckerCompletedAuditee',
            templateUrl: helper.basepath('ems.audit/AtmTrnCheckerCompletedAuditee.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.AtmTrnCheckerPendingApproval', {
            url: '/AtmTrnCheckerPendingApproval',
            title: 'AtmTrnCheckerPendingApproval',
            templateUrl: helper.basepath('ems.audit/AtmTrnCheckerPendingApproval.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.AtmTrnAuditorMakerHoldAudit', {
            url: '/AtmTrnAuditorMakerHoldAudit',
            title: 'AtmTrnAuditorMakerHoldAudit',
            templateUrl: helper.basepath('ems.audit/AtmTrnAuditorMakerHoldAudit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.AtmTrnAuditorMakerClosedAudit', {
            url: '/AtmTrnAuditorMakerClosedAudit',
            title: 'AtmTrnAuditorMakerClosedAudit',
            templateUrl: helper.basepath('ems.audit/AtmTrnAuditorMakerClosedAudit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.AtmTrnAuditorMakerTaggedAudit', {
            url: '/AtmTrnAuditorMakerTaggedAudit',
            title: 'AtmTrnAuditorMakerTaggedAudit',
            templateUrl: helper.basepath('ems.audit/AtmTrnAuditorMakerTaggedAudit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.AtmTrnAuditorMakerCompletedAudit', {
            url: '/AtmTrnAuditorMakerCompletedAudit',
            title: 'AtmTrnAuditorMakerCompletedAudit',
            templateUrl: helper.basepath('ems.audit/AtmTrnAuditorMakerCompletedAudit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.AtmTrnAuditorCheckerPendingApproval', {
            url: '/AtmTrnAuditorCheckerPendingApproval',
            title: 'AtmTrnAuditorCheckerPendingApproval',
            templateUrl: helper.basepath('ems.audit/AtmTrnAuditorCheckerPendingApproval.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.AtmTrnAuditorCheckerHoldAudit', {
            url: '/AtmTrnAuditorCheckerHoldAudit',
            title: 'AtmTrnAuditorCheckerHoldAudit',
            templateUrl: helper.basepath('ems.audit/AtmTrnAuditorCheckerHoldAudit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.AtmTrnAuditorCheckerClosedAudit', {
            url: '/AtmTrnAuditorCheckerClosedAudit',
            title: 'AtmTrnAuditorCheckerClosedAudit',
            templateUrl: helper.basepath('ems.audit/AtmTrnAuditorCheckerClosedAudit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.AtmTrnAuditorCheckerTaggedItems', {
            url: '/AtmTrnAuditorCheckerTaggedItems',
            title: 'AtmTrnAuditorCheckerTaggedItems',
            templateUrl: helper.basepath('ems.audit/AtmTrnAuditorCheckerTaggedItems.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.AtmTrnAuditorCheckerCompletedAudit', {
            url: '/AtmTrnAuditorCheckerCompletedAudit',
            title: 'AtmTrnAuditorCheckerCompletedAudit',
            templateUrl: helper.basepath('ems.audit/AtmTrnAuditorCheckerCompletedAudit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.AtmTrnAuditorApproverPendingApproval', {
            url: '/AtmTrnAuditorApproverPendingApproval',
            title: 'AtmTrnAuditorApproverPendingApproval',
            templateUrl: helper.basepath('ems.audit/AtmTrnAuditorApproverPendingApproval.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.AtmTrnAuditorApproverHoldAudit', {
            url: '/AtmTrnAuditorApproverHoldAudit',
            title: 'AtmTrnAuditorApproverHoldAudit',
            templateUrl: helper.basepath('ems.audit/AtmTrnAuditorApproverHoldAudit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.AtmTrnAuditorApproverClosedAudit', {
            url: '/AtmTrnAuditorApproverClosedAudit',
            title: 'AtmTrnAuditorApproverClosedAudit',
            templateUrl: helper.basepath('ems.audit/AtmTrnAuditorApproverClosedAudit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.AtmTrnAuditorApproverTaggedItems', {
            url: '/AtmTrnAuditorApproverTaggedItems',
            title: 'AtmTrnAuditorApproverTaggedItems',
            templateUrl: helper.basepath('ems.audit/AtmTrnAuditorApproverTaggedItems.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.AtmTrnAuditorApproverCompletedAudit', {
            url: '/AtmTrnAuditorApproverCompletedAudit',
            title: 'AtmTrnAuditorApproverCompletedAudit',
            templateUrl: helper.basepath('ems.audit/AtmTrnAuditorApproverCompletedAudit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.AtmMstCheckpointAddSummary', {
            url: '/AtmMstCheckpointAddSummary',
            title: 'AtmMstCheckpointAddSummary',
            templateUrl: helper.basepath('ems.audit/AtmMstCheckpointAddSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

	    .state('app.FndTrnCustomerApproval', {
            url: '/FndTrnCustomerApproval',
            title: 'FndTrnCustomerApproval',
            templateUrl: helper.basepath('ems.foundation/FndTrnCustomerApproval.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

	    .state('app.FndTrnCustomerApprovalEdit', {
            url: '/FndTrnCustomerApprovalEdit',
            title: 'FndTrnCustomerApprovalEdit',
            templateUrl: helper.basepath('ems.foundation/FndTrnCustomerApproval.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

	    .state('app.FndMstcustomerAddCheque', {
            url: '/FndMstcustomerAddCheque',
            title: 'FndMstcustomerAddCheque',
            templateUrl: helper.basepath('ems.foundation/FndMstcustomerAddCheque.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

	    .state('app.FndMstQuestionnarieCategory', {
            url: '/FndMstQuestionnarieCategory',
            title: 'FndMstQuestionnarieCategory',
            templateUrl: helper.basepath('ems.foundation/FndMstQuestionnarieCategory.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

	    .state('app.FndTrnApprovalView', {
            url: '/FndTrnApprovalView',
            title: 'FndTrnApprovalView',
            templateUrl: helper.basepath('ems.foundation/FndTrnApprovalView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

    	.state('app.FndTrnApprovalEdit', {
            url: '/FndTrnApprovalEdit',
            title: 'FndTrnApprovalEdit',
            templateUrl: helper.basepath('ems.foundation/FndTrnApprovalEdit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

	    .state('app.FndTrnCustomerApproved', {
            url: '/FndTrnCustomerApproved',
            title: 'FndTrnCustomerApproved',
            templateUrl: helper.basepath('ems.foundation/FndTrnCustomerApproved.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

	    .state('app.FndTrnCustomerRejected', {
            url: '/FndTrnCustomerRejected',
            title: 'FndTrnCustomerRejected',
            templateUrl: helper.basepath('ems.foundation/FndTrnCustomerRejected.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

	    .state('app.FndTrnCustomerApprovedView', {
            url: '/FndTrnCustomerApprovedView',
            title: 'FndTrnCustomerApprovedView',
            templateUrl: helper.basepath('ems.foundation/FndTrnCustomerApprovedView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

	    .state('app.FndTrnCustomerRejectedView', {
            url: '/FndTrnCustomerRejectedView',
            title: 'FndTrnCustomerRejectedView',
            templateUrl: helper.basepath('ems.foundation/FndTrnCustomerRejectedView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.AtmTrnMyAuditApproverSummary', {
            url: '/AtmTrnMyAuditApproverSummary',
            title: 'AtmTrnMyAuditApproverSummary',
            templateUrl: helper.basepath('ems.audit/AtmTrnMyAuditApproverSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.AtmTrnMyAuditApprover360View', {
            url: '/AtmTrnMyAuditApprover360View',
            title: 'AtmTrnMyAuditApprover360View',
            templateUrl: helper.basepath('ems.audit/AtmTrnMyAuditApprover360View.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')

        })

		.state('app.SysMstEmployeeInactiveSummary', {
            url: '/SysMstEmployeeInactiveSummary',
            title: 'SysMstEmployeeInactiveSummary',
            templateUrl: helper.basepath('ems.system/SysMstEmployeeInactiveSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'ngWig')
        })

        .state('app.SysMstEmployeePendingSummary', {
            url: '/SysMstEmployeePendingSummary',
            title: 'SysMstEmployeePendingSummary',
            templateUrl: helper.basepath('ems.system/SysMstEmployeePendingSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'ngWig')
        })

        .state('app.SysMstEmployeeActiveUserSummary', {
            url: '/SysMstEmployeeActiveUserSummary',
            title: 'SysMstEmployeeActiveUserSummary',
            templateUrl: helper.basepath('ems.system/SysMstEmployeeActiveUserSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'ngWig')
        })

        .state('app.SysMstEmployeeRelievingSummary', {
            url: '/SysMstEmployeeRelievingSummary',
            title: 'SysMstEmployeeRelievingSummary',
            templateUrl: helper.basepath('ems.system/SysMstEmployeeRelievingSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'ngWig')
        })

        .state('app.SysMstTaskInitiate', {
            url: '/SysMstTaskInitiate',
            title: 'SysMstTaskInitiate',
            templateUrl: helper.basepath('ems.system/SysMstTaskInitiate.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')
        })

        .state('app.SysMstTask', {
            url: '/SysMstTask',
            title: 'SysMstTask',
            templateUrl: helper.basepath('ems.system/SysMstTask.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.SysMstTaskAdd', {
            url: '/SysMstTaskAdd',
            title: 'SysMstTaskAdd',
            templateUrl: helper.basepath('ems.system/SysMstTaskAdd.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.SysMstTaskEdit', {
            url: '/SysMstTaskEdit',
            title: 'SysMstTaskEdit',
            templateUrl: helper.basepath('ems.system/SysMstTaskEdit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        
        .state('app.SysMstMyOnboardingProcess', {
            url: '/SysMstMyOnboardingProcess',
            title: 'SysMstMyOnboardingProcess',
            templateUrl: helper.basepath('ems.system/SysMstMyOnboardingProcess.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')
        })

        .state('app.SysMstMyOnboardingTaskCompleted', {
            url: '/SysMstMyOnboardingTaskCompleted',
            title: 'SysMstMyOnboardingTaskCompleted',
            templateUrl: helper.basepath('ems.system/SysMstMyOnboardingTaskCompleted.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')
        })

        .state('app.SysMstMyOnboardingTaskPending', {
            url: '/SysMstMyOnboardingTaskPending',
            title: 'SysMstMyOnboardingTaskPending',
            templateUrl: helper.basepath('ems.system/SysMstMyOnboardingTaskPending.html?ver=' + version + '"'),            
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')        
        })

        .state('app.AtmTrnMyApprovedAuditSummary', {
            url: '/AtmTrnMyApprovedAuditSummary',
            title: 'AtmTrnMyApprovedAuditSummary',
            templateUrl: helper.basepath('ems.audit/AtmTrnMyApprovedAuditSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.FndTrnCampaignSummary', {
            url: '/FndTrnCampaignSummary',
            title: 'FndTrnCampaignSummary',
            templateUrl: helper.basepath('ems.foundation/FndTrnCampaignSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.FndTrnCampaignAdd', {
            url: '/FndTrnCampaignAdd',
            title: 'FndTrnCampaignAdd',
            templateUrl: helper.basepath('ems.foundation/FndTrnCampaignAdd.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.FndTrnMyCampaignOpen', {
            url: '/FndTrnMyCampaignOpen',
            title: 'FndTrnMyCampaignOpen',
            templateUrl: helper.basepath('ems.foundation/FndTrnMyCampaignOpen.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.FndTrnMyCampaignPending', {
            url: '/FndTrnMyCampaignPending',
            title: 'FndTrnMyCampaignPending',
            templateUrl: helper.basepath('ems.foundation/FndTrnMyCampaignPending.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.FndTrnMyCampaignApproved', {
            url: '/FndTrnMyCampaignApproved',
            title: 'FndTrnMyCampaignApproved',
            templateUrl: helper.basepath('ems.foundation/FndTrnMyCampaignApproved.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.FndTrnMyCampaignEdit', {
            url: '/FndTrnMyCampaignEdit',
            title: 'FndTrnMyCampaignEdit',
            templateUrl: helper.basepath('ems.foundation/FndTrnMyCampaignEdit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
        
        .state('app.FndTrnMyCampaignView', {
            url: '/FndTrnMyCampaignView',
            title: 'FndTrnMyCampaignView',
            templateUrl: helper.basepath('ems.foundation/FndTrnMyCampaignView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.FndTrnCampaignEdit', {
            url: '/FndTrnCampaignEdit',
            title: 'FndTrnCampaignEdit',
            templateUrl: helper.basepath('ems.foundation/FndTrnCampaignEdit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.FndTrnCampaignWork', {
            url: '/FndTrnCampaignWork',
            title: 'FndTrnCampaignWork',
            templateUrl: helper.basepath('ems.foundation/FndTrnCampaignWorkSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.FndTrnCampaignClosed', {
            url: '/FndTrnCampaignClosed',
            title: 'FndTrnCampaignClosed',
            templateUrl: helper.basepath('ems.foundation/FndTrnCampaignClosedSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.FndTrnCampaignReject', {
            url: '/FndTrnCampaignReject',
            title: 'FndTrnCampaignReject',
            templateUrl: helper.basepath('ems.foundation/FndTrnCampaignRejectSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
        
        .state('app.AtmTrnTaggedAuditeeView', {
            url: '/AtmTrnTaggedAuditeeView',
            title: 'AtmTrnTaggedAuditeeView',
            templateUrl: helper.basepath('ems.audit/AtmTrnTaggedAuditeeView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.FndTrnMyCampaignApprovalPending', {
	 	    url: '/FndTrnMyCampaignApprovalPending',
		    title: 'FndTrnMyCampaignApprovalPending',
		    templateUrl: helper.basepath('ems.foundation/FndTrnMyCampaignApprovalPending.html?ver=' + version + '"'),
		    resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
    	})

        .state('app.FndTrnMyCampaignApprovalClosed', {
            url: '/FndTrnMyCampaignApprovalClosed',
            title: 'FndTrnMyCampaignApprovalClosed',
            templateUrl: helper.basepath('ems.foundation/FndTrnMyCampaignApprovalClosed.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.FndTrnCampaignApprovalWork', {
            url: '/FndTrnCampaignApprovalWork',
            title: 'FndTrnCampaignApprovalWork',
            templateUrl: helper.basepath('ems.foundation/FndTrnCampaignApprovalWork.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.FndTrnCampaignApprovalReject', {
            url: '/FndTrnCampaignApprovalReject',
            title: 'FndTrnCampaignApprovalReject',
            templateUrl: helper.basepath('ems.foundation/FndTrnCampaignApprovalReject.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.FndTrnCampaignApproval', {
            url: '/FndTrnCampaignApproval',
            title: 'FndTrnCampaignApproval',
            templateUrl: helper.basepath('ems.foundation/FndTrnCampaignApproval.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.FndTrnCampaignApprovalEdit', {
            url: '/FndTrnCampaignApprovalEdit',
            title: 'FndTrnCampaignApprovalEdit',
            templateUrl: helper.basepath('ems.foundation/FndTrnCampaignApprovalEdit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.SysMstTaskView', {
            url: '/SysMstTaskView',
            title: 'SysMstTaskView',
            templateUrl: helper.basepath('ems.system/SysMstTaskView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.MstCADReassignApplication', {
            url: '/MstCADReassignApplication',
            title: 'MstCADReassignApplication',
            templateUrl: helper.basepath('ems.master/MstCADReassignApplication.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')
        })
	
	    .state('app.MstSanctionApprovalCompleted', {
		    url: '/MstSanctionApprovalCompleted',
		    title: 'MstSanctionApprovalCompleted',
		    templateUrl: helper.basepath('ems.master/MstSanctionApprovalCompleted.html?ver=' + version + '"'),
		    resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
	    })

        .state('app.MstRMPenaltyDtls', {
            url: '/MstRMPenaltyDtls',
            title: 'MstRMPenaltyDtls',
            templateUrl: helper.basepath('ems.master/MstRMPenaltyDtls.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
        
        .state('app.MstRMLoanDetailsDtls', {
            url: '/MstRMLoanDetailsDtls',
            title: 'MstRMLoanDetailsDtls',
            templateUrl: helper.basepath('ems.master/MstRMLoanDetailsDtls.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.MstRMTDSDtls', {
            url: '/MstRMTDSDtls',
            title: 'MstRMTDSDtls',
            templateUrl: helper.basepath('ems.master/MstRMTDSDtls.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.MstCreditCrimeCheckRecordAPI', {
            url: '/MstCreditCrimeCheckRecordAPI',
            title: 'MstCreditCrimeCheckRecordAPI',
            templateUrl: helper.basepath('ems.master/MstCreditCrimeCheckRecordAPI.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.MstCompanyCrimeCheckRecordAPI', {
            url: '/MstCompanyCrimeCheckRecordAPI',
            title: 'MstCompanyCrimeCheckRecordAPI',
            templateUrl: helper.basepath('ems.master/MstCompanyCrimeCheckRecordAPI.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.MstCrimeReportCompanyView', {
            url: '/MstCrimeReportCompanyView',
            title: 'MstCrimeReportCompanyView',
            templateUrl: helper.basepath('ems.master/MstCrimeReportCompanyView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.MstCrimeReportIndividualView', {
            url: '/MstCrimeReportIndividualView',
            title: 'MstCrimeReportIndividualView',
            templateUrl: helper.basepath('ems.master/MstCrimeReportIndividualView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        
        .state('app.MstSAOnboardingIndividualEdit', {
            url: '/MstSAOnboardingIndividualEdit',
            title: 'MstSAOnboardingIndividualEdit',
            templateUrl: helper.basepath('ems.master/MstSAOnboardingIndividualEdit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.MstSAOnboardingIndividualSummary', {
            url: '/MstSAOnboardingIndividualSummary',
            title: 'MstSAOnboardingIndividualSummary',
            templateUrl: helper.basepath('ems.master/MstSAOnboardingIndividualSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.MstSAOnboardingInstitutionEdit', {
            url: '/MstSAOnboardingInstitutionEdit',
            title: 'MstSAOnboardingInstitutionEdit',
            templateUrl: helper.basepath('ems.master/MstSAOnboardingInstitutionEdit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.MstSAOnboardingInstitutionView', {
            url: '/MstSAOnboardingInstitutionView',
            title: 'MstSAOnboardingInstitutionView',
            templateUrl: helper.basepath('ems.master/MstSAOnboardingInstitutionView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.MstSAVerificationPendingInstitutionView', {
            url: '/MstSAVerificationPendingInstitutionView',
            title: 'MstSAVerificationPendingInstitutionView',
            templateUrl: helper.basepath('ems.master/MstSAVerificationPendingInstitutionView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.MstSAVerificationIndividualMappingPending', {
            url: '/MstSAVerificationIndividualMappingPending',
            title: 'MstSAVerificationIndividualMappingPending',
            templateUrl: helper.basepath('ems.master/MstSAVerificationIndividualMappingPending.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstSAVerificationPendingIndividualView', {
            url: '/MstSAVerificationPendingIndividualView',
            title: 'MstSAVerificationPendingIndividualView',
            templateUrl: helper.basepath('ems.master/MstSAVerificationPendingIndividualView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstSAVerificationAssignedInstitutionView', {
            url: '/MstSAVerificationAssignedInstitutionView',
            title: 'MstSAVerificationAssignedInstitutionView',
            templateUrl: helper.basepath('ems.master/MstSAVerificationAssignedInstitutionView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstSAVerificationAssignedIndividualView', {
            url: '/MstSAVerificationAssignedIndividualView',
            title: 'MstSAVerificationAssignedIndividualView',
            templateUrl: helper.basepath('ems.master/MstSAVerificationAssignedIndividualView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstSAOnboardingIndividualView', {
            url: '/MstSAOnboardingIndividualView',
            title: 'MstSAOnboardingIndividualView',
            templateUrl: helper.basepath('ems.master/MstSAOnboardingIndividualView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.MstSAVerificationMappingPending', {
            url: '/MstSAVerificationMappingPending',
            title: 'MstSAVerificationMappingPending',
            templateUrl: helper.basepath('ems.master/MstSAVerificationMappingPending.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.MstRMNDCDtls', {
            url: '/MstRMNDCDtls',
            title: 'MstRMNDCDtls',
            templateUrl: helper.basepath('ems.master/MstRMNDCDtls.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.MstRMNOCDtls', {
            url: '/MstRMNOCDtls',
            title: 'MstRMNOCDtls',
            templateUrl: helper.basepath('ems.master/MstRMNOCDtls.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.MstRMMoratoriumDtls', {
            url: '/MstRMMoratoriumDtls',
            title: 'MstRMMoratoriumDtls',
            templateUrl: helper.basepath('ems.master/MstRMMoratoriumDtls.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
        
        .state('app.FndTrnMyCampaignApproval', {
            url: '/FndTrnMyCampaignApproval',
            title: 'FndTrnMyCampaignApproval',
            templateUrl: helper.basepath('ems.foundation/FndTrnMyCampaignApproval.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.MstRMBankAccountDetails', {
            url: '/MstRMBankAccountDetails',
            title: 'MstRMBankAccountDetails',
            templateUrl: helper.basepath('ems.master/MstRMBankAccountDetails.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.OsdBamRMCompletedSummary', {
            url: '/OsdBamRMCompletedSummary',
            title: 'OsdBamRMCompletedSummary',
            templateUrl: helper.basepath('ems.osd/OsdBamRMCompletedSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.MstRMDeviationDtls', {
            url: '/MstRMDeviationDtls',
            title: 'MstRMDeviationDtls',
            templateUrl: helper.basepath('ems.master/MstRMDeviationDtls.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.MstCadPhysicalDocCompletedSummary', {
            url: '/MstCadPhysicalDocCompletedSummary',
            title: 'MstCadPhysicalDocCompletedSummary',
            templateUrl: helper.basepath('ems.master/MstCadPhysicalDocCompletedSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        
        .state('app.MstPhysicalDeferralHistory', {
            url: '/MstPhysicalDeferralHistory',
            title: 'MstPhysicalDeferralHistory',
            templateUrl: helper.basepath('ems.master/MstPhysicalDeferralHistory.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.MstSAOnboardingInstitutionVerification', {
            url: '/MstSAOnboardingInstitutionVerification',
            title: 'MstSAOnboardingInstitutionVerification',
            templateUrl: helper.basepath('ems.master/MstSAOnboardingInstitutionVerification.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.MstSAOnboardingIndividualVerification', {
            url: '/MstSAOnboardingIndividualVerification',
            title: 'MstSAOnboardingIndividualVerification',
            templateUrl: helper.basepath('ems.master/MstSAOnboardingIndividualVerification.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.MstSAOnboardingVerificationSummary', {
            url: '/MstSAOnboardingVerificationSummary',
            title: 'MstSAOnboardingVerificationSummary',
            templateUrl: helper.basepath('ems.master/MstSAOnboardingVerificationSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.MstSAOnboardingIndividualVerificationSummary', {
            url: '/MstSAOnboardingIndividualVerificationSummary',
            title: 'MstSAOnboardingIndividualVerificationSummary',
            templateUrl: helper.basepath('ems.master/MstSAOnboardingIndividualVerificationSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.MstSAonboardingBureauView', {
            url: '/MstSAonboardingBureauView',
            title: 'MstSAonboardingBureauView',
            templateUrl: helper.basepath('ems.master/MstSAonboardingBureauView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.MstSAonboardingIndBureauView', {
            url: '/MstSAonboardingIndBureauView',
            title: 'MstSAonboardingIndBureauView',
            templateUrl: helper.basepath('ems.master/MstSAonboardingIndBureauView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.MstChequeApprovalCompleted', {
            url: '/MstChequeApprovalCompleted',
            title: 'MstChequeApprovalCompleted',
            templateUrl: helper.basepath('ems.master/MstChequeApprovalCompleted.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.MstChequeMakerFollowDtls', {
            url: '/MstChequeMakerFollowDtls',
            title: 'MstChequeMakerFollowDtls',
            templateUrl: helper.basepath('ems.master/MstChequeMakerFollowDtls.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.MstChequeCheckerDtls', {
            url: '/MstChequeCheckerDtls',
            title: 'MstChequeCheckerDtls',
            templateUrl: helper.basepath('ems.master/MstChequeCheckerDtls.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.MstChequeApprovalDtls', {
            url: '/MstChequeApprovalDtls',
            title: 'MstChequeApprovalDtls',
            templateUrl: helper.basepath('ems.master/MstChequeApprovalDtls.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.SysMstMenuMapping', {
            url: '/SysMstMenuMapping',
            title: 'SysMstMenuMapping',
            templateUrl: helper.basepath('ems.system/SysMstMenuMapping.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'textAngular', 'ngWig')
        })
        
        .state('app.MstLSAApprovalCompleted', {
            url: '/MstLSAApprovalCompleted',
            title: 'MstLSAApprovalCompleted',
            templateUrl: helper.basepath('ems.master/MstLSAApprovalCompleted.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        
        .state('app.MstCADlsa360View', {
            url: '/MstCADlsa360View',
            title: 'MstCADlsa360View',
            templateUrl: helper.basepath('ems.master/MstCADlsa360View.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

         .state('app.FndTrnMyCampaignApprovalView', {
            url: '/FndTrnMyCampaignApprovalView',
            title: 'FndTrnMyCampaignApprovalView',
            templateUrl: helper.basepath('ems.foundation/FndTrnMyCampaignApprovalView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.FndTrnMyCampaignApprovalClosedView', {
            url: '/FndTrnMyCampaignApprovalClosedView',
            title: 'FndTrnMyCampaignApprovalClosedView',
            templateUrl: helper.basepath('ems.foundation/FndTrnMyCampaignApprovalClosedView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.MstSAOnboardingApprovalInsSummary', {
            url: '/MstSAOnboardingApprovalInsSummary',
            title: 'MstSAOnboardingApprovalInsSummary',
            templateUrl: helper.basepath('ems.master/MstSAOnboardingApprovalInsSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
              
        .state('app.MstSAOnboardingApprovalIndSummary', {
            url: '/MstSAOnboardingApprovalIndSummary',
            title: 'MstSAOnboardingApprovalIndSummary',
            templateUrl: helper.basepath('ems.master/MstSAOnboardingApprovalIndSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })            
            
        .state('app.MstSAOnboardingInstitutionApproval', {
            url: '/MstSAOnboardingInstitutionApproval',
            title: 'MstSAOnboardingInstitutionApproval',
            templateUrl: helper.basepath('ems.master/MstSAOnboardingInstitutionApproval.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })            
            
        .state('app.MstSAOnboardingIndividualApproval', {
            url: '/MstSAOnboardingIndividualApproval',
            title: 'MstSAOnboardingIndividualApproval',
            templateUrl: helper.basepath('ems.master/MstSAOnboardingIndividualApproval.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.MstCreditOpsMapping', {
            url: '/MstCreditOpsMapping',
            title: 'MstCreditOpsMapping',
            templateUrl: helper.basepath('ems.master/MstCreditOpsMapping.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.MstRMDisbursementRequest', {
            url: '/MstRMDisbursementRequest',
            title: 'MstRMDisbursementRequest',
            templateUrl: helper.basepath('ems.master/MstRMDisbursementRequest.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.MstRMInitiateDisbursement', {
            url: '/MstRMInitiateDisbursement',
            title: 'MstRMInitiateDisbursement',
            templateUrl: helper.basepath('ems.master/MstRMInitiateDisbursement.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.MstRMDisbursementRequestEdit', {
            url: '/MstRMDisbursementRequestEdit',
            title: 'MstRMDisbursementRequestEdit',
            templateUrl: helper.basepath('ems.master/MstRMDisbursementRequestEdit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.MstRMDisbursementRequestView', {
            url: '/MstRMDisbursementRequestView',
            title: 'MstRMDisbursementRequestView',
            templateUrl: helper.basepath('ems.master/MstRMDisbursementRequestView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.FndTrnCampaignApprovalView', {
            url: '/FndTrnCampaignApprovalView',
            title: 'FndTrnCampaignApprovalView',
            templateUrl: helper.basepath('ems.foundation/FndTrnCampaignApprovalView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.FndTrnCampaignApprovalRejectedView', {
            url: '/FndTrnCampaignApprovalRejectedView',
            title: 'FndTrnCampaignApprovalRejectedView',
            templateUrl: helper.basepath('ems.foundation/FndTrnCampaignApprovalRejectedView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.FndTrnCampaignApprovalWorkView', {
            url: '/FndTrnCampaignApprovalWorkView',
            title: 'FndTrnCampaignApprovalWorkView',
            templateUrl: helper.basepath('ems.foundation/FndTrnCampaignApprovalWorkView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.FndTrnCampaignRejectedView', {
            url: '/FndTrnCampaignRejectedView',
            title: 'FndTrnCampaignRejectedView',
            templateUrl: helper.basepath('ems.foundation/FndTrnCampaignRejectedView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.FndTrnCampaignWorkView', {
            url: '/FndTrnCampaignWorkView',
            title: 'FndTrnCampaignWorkView',
            templateUrl: helper.basepath('ems.foundation/FndTrnCampaignWorkView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.FndTrnCampaignClosedView', {
            url: '/FndTrnCampaignClosedView',
            title: 'FndTrnCampaignClosedView',
            templateUrl: helper.basepath('ems.foundation/FndTrnCampaignClosedView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.FndTrnMyCampaignApprovedView', {
            url: '/FndTrnMyCampaignApprovedView',
            title: 'FndTrnMyCampaignApprovedView',
            templateUrl: helper.basepath('ems.foundation/FndTrnMyCampaignApprovedView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.FndTrnMyCampaignRejectedView', {
            url: '/FndTrnMyCampaignRejectedView',
            title: 'FndTrnMyCampaignRejectedView',
            templateUrl: helper.basepath('ems.foundation/FndTrnMyCampaignRejectedView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.FndTrnCampaignView', {
            url: '/FndTrnCampaignView',
            title: 'FndTrnCampaignView',
            templateUrl: helper.basepath('ems.foundation/FndTrnCampaignView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.MstDisbursementAssignmentSummary', {
            url: '/MstDisbursementAssignmentSummary',
            title: 'MstDisbursementAssignmentSummary',
            templateUrl: helper.basepath('ems.master/MstDisbursementAssignmentSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.MstAssignedDisbursementSummary', {
            url: '/MstAssignedDisbursementSummary',
            title: 'MstAssignedDisbursementSummary',
            templateUrl: helper.basepath('ems.master/MstAssignedDisbursementSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.MstMyDisbursementSummary', {
            url: '/MstMyDisbursementSummary',
            title: 'MstMyDisbursementSummary',
            templateUrl: helper.basepath('ems.master/MstMyDisbursementSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.MstMyDisbursementCheckerSummary', {
            url: '/MstMyDisbursementCheckerSummary',
            title: 'MstMyDisbursementCheckerSummary',
            templateUrl: helper.basepath('ems.master/MstMyDisbursementCheckerSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.MstMyDisbursementCompletedSummary', {
            url: '/MstMyDisbursementCompletedSummary',
            title: 'MstMyDisbursementCompletedSummary',
            templateUrl: helper.basepath('ems.master/MstMyDisbursementCompletedSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.MstDisbursementMaker', {
            url: '/MstDisbursementMaker',
            title: 'MstDisbursementMaker',
            templateUrl: helper.basepath('ems.master/MstDisbursementMaker.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        
        .state('app.MstDisbursementChecker', {
            url: '/MstDisbursementChecker',
            title: 'MstDisbursementChecker',
            templateUrl: helper.basepath('ems.master/MstDisbursementChecker.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.MstEditWaiver', {
            url: '/MstEditWaiver',
            title: 'MstEditWaiver',
            templateUrl: helper.basepath('ems.master/MstEditWaiver.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.MstAddWaiver', {
            url: '/MstAddWaiver',
            title: 'MstAddWaiver',
            templateUrl: helper.basepath('ems.master/MstAddWaiver.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.MstApprovedDisbursementSummary', {
            url: '/MstApprovedDisbursementSummary',
            title: 'MstApprovedDisbursementSummary',
            templateUrl: helper.basepath('ems.master/MstApprovedDisbursementSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.MstApprovedDisbursementView', {
            url: '/MstApprovedDisbursementView',
            title: 'MstApprovedDisbursementView',
            templateUrl: helper.basepath('ems.master/MstApprovedDisbursementView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.MstSAVerificationIndPendingSummary', {
            url: '/MstSAVerificationIndPendingSummary',
            title: 'MstSAVerificationIndPendingSummary',
            templateUrl: helper.basepath('ems.master/MstSAVerificationIndPendingSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.MstSAVerificationInstPendingSummary', {
            url: '/MstSAVerificationInstPendingSummary',
            title: 'MstSAVerificationInstPendingSummary',
            templateUrl: helper.basepath('ems.master/MstSAVerificationInstPendingSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.MstSAApprovalInsPendingSummary', {
            url: '/MstSAApprovalInsPendingSummary',
            title: 'MstSAApprovalInsPendingSummary',
            templateUrl: helper.basepath('ems.master/MstSAApprovalInsPendingSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.MstSAApprovalIndPendingSummary', {
            url: '/MstSAApprovalIndPendingSummary',
            title: 'MstSAApprovalIndPendingSummary',
            templateUrl: helper.basepath('ems.master/MstSAApprovalIndPendingSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.AtmMstAuditReportSummary', {
            url: '/AtmMstAuditReportSummary',
            title: 'AtmMstAuditReportSummary',
            templateUrl: helper.basepath('ems.audit/AtmMstAuditReportSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
        .state('app.AtmRptAuditReportView', {
            url: '/AtmRptAuditReportView',
            title: 'AtmRptAuditReportView',
            templateUrl: helper.basepath('ems.audit/AtmRptAuditReportView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
        .state('app.MstTeleCallingReportView', {
            url: '/MstTeleCallingReportView',
            title: 'MstTeleCallingReportView',
            templateUrl: helper.basepath('ems.master/MstTeleCallingReportView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.MstSanctionDtlViewSummary', {
            url: '/MstSanctionDtlViewSummary',
            title: 'MstSanctionDtlViewSummary',
            templateUrl: helper.basepath('ems.master/MstSanctionDtlViewSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

       
        
        .state('app.MstDocChecklistApprovalCompleted', {
            url: '/MstDocChecklistApprovalCompleted',
            title: 'MstDocChecklistApprovalCompleted',
            templateUrl: helper.basepath('ems.master/MstDocChecklistApprovalCompleted.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.rpthighmark', {
            url: '/rpthighmark',
            title: 'rpthighmark',
            templateUrl: helper.basepath('ems.mis/rpthighmark.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.osdBamAllocatedToAssignedView', {
            url: '/osdBamAllocatedToAssignedView',
            title: 'osdBamAllocatedToAssignedView',
            templateUrl: helper.basepath('ems.osd/osdBamAllocatedToAssignedView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.MstPhysicalStatusSummary', {
            url: '/MstPhysicalStatusSummary',
            title: 'MstPhysicalStatusSummary',
            templateUrl: helper.basepath('ems.master/MstPhysicalStatusSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.MstSalutation', {
            url: '/MstSalutation',
            title: 'MstSalutation',
            templateUrl: helper.basepath('ems.master/MstSalutation.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.osdMstBusinessUnitAdd', {
            url: '/osdMstBusinessUnitAdd',
            title: 'osdMstBusinessUnitAdd',
            templateUrl: helper.basepath('ems.osd/osdMstBusinessUnitAdd.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
        
	    .state('app.osdMstBusinessUnitEdit', {
            url: '/osdMstBusinessUnitEdit',
            title: 'osdMstBusinessUnitEdit',
            templateUrl: helper.basepath('ems.osd/osdMstBusinessUnitEdit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.ExcelDataMigration', {
            url: '/Migration',
            title: 'ExcelDataMigration',
            templateUrl: helper.basepath('ems.master/ExcelDataMigration.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.MstCADGeneralDtlEdit', {
            url: '/MstCADGeneralDtlEdit',
            title: 'MstCADGeneralDtlEdit',
            templateUrl: helper.basepath('ems.master/MstCADGeneralDtlEdit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.MstCADInstitutionDtlEdit', {
            url: '/MstCADInstitutionDtlEdit',
            title: 'MstCADInstitutionDtlEdit',
            templateUrl: helper.basepath('ems.master/MstCADInstitutionDtlEdit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

		.state('app.MstCADIndividualDtlEdit', {
		    url: '/MstCADIndividualDtlEdit',
		    title: 'MstCADIndividualDtlEdit',
		    templateUrl: helper.basepath('ems.master/MstCADIndividualDtlEdit.html?ver=' + version + '"'),
		    resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
		})

		.state('app.MstCADGroupDtlEdit', {
		    url: '/MstCADGroupDtlEdit',
		    title: 'MstCADGroupDtlEdit',
		    templateUrl: helper.basepath('ems.master/MstCADGroupDtlEdit.html?ver=' + version + '"'),
		    resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
		})

		.state('app.MstCADProductChargesDtlEdit', {
		    url: '/MstCADProductChargesDtlEdit',
		    title: 'MstCADProductChargesDtlEdit',
		    templateUrl: helper.basepath('ems.master/MstCADProductChargesDtlEdit.html?ver=' + version + '"'),
		    resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
		})

		 .state('app.MstCADLoanDtlEdit', {
		     url: '/MstCADLoanDtlEdit',
		     title: 'MstCADLoanDtlEdit',
		     templateUrl: helper.basepath('ems.master/MstCADLoanDtlEdit.html?ver=' + version + '"'),
		     resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
		 })

        .state('app.MstCADHypothecationEdit', {
            url: '/MstCADHypothecationEdit',
            title: 'MstCADHypothecationEdit',
            templateUrl: helper.basepath('ems.master/MstCADHypothecationEdit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

		.state('app.MstCADServicesDtlEdit', {
		    url: '/MstCADServicesDtlEdit',
		    title: 'MstCADServicesDtlEdit',
		    templateUrl: helper.basepath('ems.master/MstCADServicesDtlEdit.html?ver=' + version + '"'),
		    resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
		})

         	.state('app.MstCadUrnAcceptedCustomers', {
         	    url: '/MstCadUrnAcceptedCustomers',
         	    title: 'MstCadUrnAcceptedCustomers',
         	    templateUrl: helper.basepath('ems.master/MstCadUrnAcceptedCustomers.html?ver=' + version + '"'),
         	    resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
         	})
        .state('app.MstCadUrnAcceptedCustomerDtls', {
            url: '/MstCadUrnAcceptedCustomerDtls',
            title: 'MstCadUrnAcceptedCustomerDtls',
            templateUrl: helper.basepath('ems.master/MstCadUrnAcceptedCustomerDtls.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstCadInstitutionView', {
            url: '/MstCadInstitutionView',
            title: 'MstCadInstitutionView',
            templateUrl: helper.basepath('ems.master/MstCadInstitutionView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstCadIndividualView', {
            url: '/MstCadIndividualView',
            title: 'MstCadIndividualView',
            templateUrl: helper.basepath('ems.master/MstCadIndividualView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
         .state('app.MstCadGroupView', {
             url: '/MstCadGroupView',
             title: 'MstCadGroupView',
             templateUrl: helper.basepath('ems.master/MstCadGroupView.html?ver=' + version + '"'),
             resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
         })
        .state('app.MstCadVisitReportView', {
            url: '/MstCadVisitReportView',
            title: 'MstCadVisitReportView',
            templateUrl: helper.basepath('ems.master/MstCadVisitReportView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
         .state('app.MstCadGradingToolView', {
             url: '/MstCadGradingToolView',
             title: 'MstCadGradingToolView',
             templateUrl: helper.basepath('ems.master/MstCadGradingToolView.html?ver=' + version + '"'),
             resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
         })
	.state('app.MstCADDocumentCheckList', {
	    url: '/MstCADDocumentCheckList',
	    title: 'MstCADDocumentCheckList',
	    templateUrl: helper.basepath('ems.master/MstCADDocumentCheckList.html?ver=' + version + '"'),
	    resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
	})

        .state('app.MstCADCreditAddCovenantCheckList', {
            url: '/MstCADCreditAddCovenantCheckList',
            title: 'MstCADCreditAddCovenantCheckList',
            templateUrl: helper.basepath('ems.master/MstCADCreditAddCovenantCheckList.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.MstCADCreditInstitutionDtlAdd', {
            url: '/MstCADCreditInstitutionDtlAdd',
            title: 'MstCADCreditInstitutionDtlAdd',
            templateUrl: helper.basepath('ems.master/MstCADCreditInstitutionDtlAdd.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.MstCADCreditInstitutionBureauEdit', {
            url: '/MstCADCreditInstitutionBureauEdit',
            title: 'MstCADCreditInstitutionBureauEdit',
            templateUrl: helper.basepath('ems.master/MstCADCreditInstitutionBureauEdit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.MstCADCreditEconomicCapitalAdd', {
            url: '/MstCADCreditEconomicCapitalAdd',
            title: 'MstCADCreditEconomicCapitalAdd',
            templateUrl: helper.basepath('ems.master/MstCADCreditEconomicCapitalAdd.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.MstCADCreditEconomicCapitalEdit', {
            url: '/MstCADCreditEconomicCapitalEdit',
            title: 'MstCADCreditEconomicCapitalEdit',
            templateUrl: helper.basepath('ems.master/MstCADCreditEconomicCapitalEdit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.MstCADCreditPSLDataFlaggingAdd', {
            url: '/MstCADCreditPSLDataFlaggingAdd',
            title: 'MstCADCreditPSLDataFlaggingAdd',
            templateUrl: helper.basepath('ems.master/MstCADCreditPSLDataFlaggingAdd.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.MstCADCreditPSLDataFlaggingEdit', {
            url: '/MstCADCreditPSLDataFlaggingEdit',
            title: 'MstCADCreditPSLDataFlaggingEdit',
            templateUrl: helper.basepath('ems.master/MstCADCreditPSLDataFlaggingEdit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.MstCADCreditSuppliersDtlAdd', {
            url: '/MstCADCreditSuppliersDtlAdd',
            title: 'MstCADCreditSuppliersDtlAdd',
            templateUrl: helper.basepath('ems.master/MstCADCreditSuppliersDtlAdd.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.MstCADCreditSuppliersDtlEdit', {
            url: '/MstCADCreditSuppliersDtlEdit',
            title: 'MstCADCreditSuppliersDtlEdit',
            templateUrl: helper.basepath('ems.master/MstCADCreditSuppliersDtlEdit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })


        .state('app.MstCADCreditBuyerDtlAdd', {
            url: '/MstCADCreditBuyerDtlAdd',
            title: 'MstCADCreditBuyerDtlAdd',
            templateUrl: helper.basepath('ems.master/MstCADCreditBuyerDtlAdd.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.MstCADCreditBuyerDtlEdit', {
            url: '/MstCADCreditBuyerDtlEdit',
            title: 'MstCADCreditBuyerDtlEdit',
            templateUrl: helper.basepath('ems.master/MstCADCreditBuyerDtlEdit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.MstCADCreditBankAccountDtlAdd', {
            url: '/MstCADCreditBankAccountDtlAdd',
            title: 'MstCADCreditBankAccountDtlAdd',
            templateUrl: helper.basepath('ems.master/MstCADCreditBankAccountDtlAdd.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.MstCADCreditExistingBankDtlAdd', {
            url: '/MstCADCreditExistingBankDtlAdd',
            title: 'MstCADCreditExistingBankDtlAdd',
            templateUrl: helper.basepath('ems.master/MstCADCreditExistingBankDtlAdd.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.MstCADCreditRepaymentDtlAdd', {
            url: '/MstCADCreditRepaymentDtlAdd',
            title: 'MstCADCreditRepaymentDtlAdd',
            templateUrl: helper.basepath('ems.master/MstCADCreditRepaymentDtlAdd.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.MstCADCreditBankStatementAnalysisAdd', {
            url: '/MstCADCreditBankStatementAnalysisAdd',
            title: 'MstCADCreditBankStatementAnalysisAdd',
            templateUrl: helper.basepath('ems.master/MstCADCreditBankStatementAnalysisAdd.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.MstCADCreditFsaDetailAdd', {
            url: '/MstCADCreditFsaDetailAdd',
            title: 'MstCADCreditFsaDetailAdd',
            templateUrl: helper.basepath('ems.master/MstCADCreditFsaDetailAdd.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.MstCADCreditObservationAdd', {
            url: '/MstCADCreditObservationAdd',
            title: 'MstCADCreditObservationAdd',
            templateUrl: helper.basepath('ems.master/MstCADCreditObservationAdd.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.MstCADCreditCompanyDtlAdd', {
            url: '/MstCADCreditCompanyDtlAdd',
            title: 'MstCADCreditCompanyDtlAdd',
            templateUrl: helper.basepath('ems.master/MstCADCreditCompanyDtlAdd.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.MstCADIndividualDocCheckList', {
            url: '/MstCADIndividualDocCheckList',
            title: 'MstCADIndividualDocCheckList',
            templateUrl: helper.basepath('ems.master/MstCADIndividualDocCheckList.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.MstCADIndividualCovenantDocChecklist', {
            url: '/MstCADIndividualCovenantDocChecklist',
            title: 'MstCADIndividualCovenantDocChecklist',
            templateUrl: helper.basepath('ems.master/MstCADIndividualCovenantDocChecklist.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.MstCADCreditIndividualDtlAdd', {
            url: '/MstCADCreditIndividualDtlAdd',
            title: 'MstCADCreditIndividualDtlAdd',
            templateUrl: helper.basepath('ems.master/MstCADCreditIndividualDtlAdd.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.MstCADCreditIndividualBureauEdit', {
            url: '/MstCADCreditIndividualBureauEdit',
            title: 'MstCADCreditIndividualBureauEdit',
            templateUrl: helper.basepath('ems.master/MstCADCreditIndividualBureauEdit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.MstCADCreditIndividualBankAcctAdd', {
            url: '/MstCADCreditIndividualBankAcctAdd',
            title: 'MstCADCreditIndividualBankAcctAdd',
            templateUrl: helper.basepath('ems.master/MstCADCreditIndividualBankAcctAdd.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.MstCADCreditIndividualBankAcctEdit', {
            url: '/MstCADCreditIndividualBankAcctEdit',
            title: 'MstCADCreditIndividualBankAcctEdit',
            templateUrl: helper.basepath('ems.master/MstCADCreditIndividualBankAcctEdit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.MstCADCreditIndividualExistingBankAdd', {
            url: '/MstCADCreditIndividualExistingBankAdd',
            title: 'MstCADCreditIndividualExistingBankAdd',
            templateUrl: helper.basepath('ems.master/MstCADCreditIndividualExistingBankAdd.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.MstCADCreditIndividualExistingBankEdit', {
            url: '/MstCADCreditIndividualExistingBankEdit',
            title: 'MstCADCreditIndividualExistingBankEdit',
            templateUrl: helper.basepath('ems.master/MstCADCreditIndividualExistingBankEdit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.MstCADCreditIndividualRepaymentAdd', {
            url: '/MstCADCreditIndividualRepaymentAdd',
            title: 'MstCADCreditIndividualRepaymentAdd',
            templateUrl: helper.basepath('ems.master/MstCADCreditIndividualRepaymentAdd.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.MstCADCreditIndividualRepaymentEdit', {
            url: '/MstCADCreditIndividualRepaymentEdit',
            title: 'MstCADCreditIndividualRepaymentEdit',
            templateUrl: helper.basepath('ems.master/MstCADCreditIndividualRepaymentEdit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.MstCADCreditIndividualPSLDataFlagAdd', {
            url: '/MstCADCreditIndividualPSLDataFlagAdd',
            title: 'MstCADCreditIndividualPSLDataFlagAdd',
            templateUrl: helper.basepath('ems.master/MstCADCreditIndividualPSLDataFlagAdd.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.MstCADCreditIndividualPSLDataFlagEdit', {
            url: '/MstCADCreditIndividualPSLDataFlagEdit',
            title: 'MstCADCreditIndividualPSLDataFlagEdit',
            templateUrl: helper.basepath('ems.master/MstCADCreditIndividualPSLDataFlagEdit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.MstCADCreditIndividualBankStatementAnalysisAdd', {
            url: '/MstCADCreditIndividualBankStatementAnalysisAdd',
            title: 'MstCADCreditIndividualBankStatementAnalysisAdd',
            templateUrl: helper.basepath('ems.master/MstCADCreditIndividualBankStatementAnalysisAdd.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.MstCADCreditIndividualObservationAdd', {
            url: '/MstCADCreditIndividualObservationAdd',
            title: 'MstCADCreditIndividualObservationAdd',
            templateUrl: helper.basepath('ems.master/MstCADCreditIndividualObservationAdd.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.MstCADGroupDocCheckList', {
            url: '/MstCADGroupDocCheckList',
            title: 'MstCADGroupDocCheckList',
            templateUrl: helper.basepath('ems.master/MstCADGroupDocCheckList.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.MstCADGroupCovenantDocChecklist', {
            url: '/MstCADGroupCovenantDocChecklist',
            title: 'MstCADGroupCovenantDocChecklist',
            templateUrl: helper.basepath('ems.master/MstCADGroupCovenantDocChecklist.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.MstCADCreditGroupBankAcctAdd', {
            url: '/MstCADCreditGroupBankAcctAdd',
            title: 'MstCADCreditGroupBankAcctAdd',
            templateUrl: helper.basepath('ems.master/MstCADCreditGroupBankAcctAdd.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.MstCADCreditGroupBankAcctEdit', {
            url: '/MstCADCreditGroupBankAcctEdit',
            title: 'MstCADCreditGroupBankAcctEdit',
            templateUrl: helper.basepath('ems.master/MstCADCreditGroupBankAcctEdit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.MstCADCreditGroupExistingBankAdd', {
            url: '/MstCADCreditGroupExistingBankAdd',
            title: 'MstCADCreditGroupExistingBankAdd',
            templateUrl: helper.basepath('ems.master/MstCADCreditGroupExistingBankAdd.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.MstCADCreditGroupExistingBankEdit', {
            url: '/MstCADCreditGroupExistingBankEdit',
            title: 'MstCADCreditGroupExistingBankEdit',
            templateUrl: helper.basepath('ems.master/MstCADCreditGroupExistingBankEdit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.MstCADCreditGroupRepaymentAdd', {
            url: '/MstCADCreditGroupRepaymentAdd',
            title: 'MstCADCreditGroupRepaymentAdd',
            templateUrl: helper.basepath('ems.master/MstCADCreditGroupRepaymentAdd.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.MstCADCreditGroupRepaymentEdit', {
            url: '/MstCADCreditGroupRepaymentEdit',
            title: 'MstCADCreditGroupRepaymentEdit',
            templateUrl: helper.basepath('ems.master/MstCADCreditGroupRepaymentEdit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.MstCADCreditGroupPSLDataFlagAdd', {
            url: '/MstCADCreditGroupPSLDataFlagAdd',
            title: 'MstCADCreditGroupPSLDataFlagAdd',
            templateUrl: helper.basepath('ems.master/MstCADCreditGroupPSLDataFlagAdd.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.MstCADCreditGroupPSLDataFlagEdit', {
            url: '/MstCADCreditGroupPSLDataFlagEdit',
            title: 'MstCADCreditGroupPSLDataFlagEdit',
            templateUrl: helper.basepath('ems.master/MstCADCreditGroupPSLDataFlagEdit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.MstCADCreditGroupBankStatementAnalysisAdd', {
            url: '/MstCADCreditGroupBankStatementAnalysisAdd',
            title: 'MstCADCreditGroupBankStatementAnalysisAdd',
            templateUrl: helper.basepath('ems.master/MstCADCreditGroupBankStatementAnalysisAdd.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.MstCADCreditGroupObservationAdd', {
            url: '/MstCADCreditGroupObservationAdd',
            title: 'MstCADCreditGroupObservationAdd',
            templateUrl: helper.basepath('ems.master/MstCADCreditGroupObservationAdd.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.MstCADCreditInstitutionBureauView', {
            url: '/MstCADCreditInstitutionBureauView',
            title: 'MstCADCreditInstitutionBureauView',
            templateUrl: helper.basepath('ems.master/MstCADCreditInstitutionBureauView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

	.state('app.MstCADCreditIndividualBureauView', {
	    url: '/MstCADCreditIndividualBureauView',
	    title: 'MstCADCreditIndividualBureauView',
	    templateUrl: helper.basepath('ems.master/MstCADCreditIndividualBureauView.html?ver=' + version + '"'),
	    resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
	})

	.state('app.MstCADPendingApplicationEdit', {
	    url: '/MstCADPendingApplicationEdit',
	    title: 'MstCADPendingApplicationEdit',
	    templateUrl: helper.basepath('ems.master/MstCADPendingApplicationEdit.html?ver=' + version + '"'),
	    resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
	})

	
	.state('app.MstCADCreditCompanyAPIAdd', {
	    url: '/MstCADCreditCompanyAPIAdd',
	    title: 'MstCADCreditCompanyAPIAdd',
	    templateUrl: helper.basepath('ems.master/MstCADCreditCompanyAPIAdd.html?ver=' + version + '"'),
	    resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
	})

        .state('app.CADGSTAuthenticationView', {
            url: '/CADGSTAuthenticationView',
            title: 'CADGSTAuthenticationView',
            templateUrl: helper.basepath('ems.master/CADGSTAuthenticationView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.CADGSPGSTINAuthenticationView', {
            url: '/CADGSPGSTINAuthenticationView',
            title: 'CADGSPGSTINAuthenticationView',
            templateUrl: helper.basepath('ems.master/CADGSPGSTINAuthenticationView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.CADGSPGSTReturnFilingView', {
            url: '/CADGSPGSTReturnFilingView',
            title: 'CADGSPGSTReturnFilingView',
            templateUrl: helper.basepath('ems.master/CADGSPGSTReturnFilingView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.CADIECDetailedProfileView', {
            url: '/CADIECDetailedProfileView',
            title: 'CADIECDetailedProfileView',
            templateUrl: helper.basepath('ems.master/CADIECDetailedProfileView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.CADPropertyTaxView', {
            url: '/CADPropertyTaxView',
            title: 'CADPropertyTaxView',
            templateUrl: helper.basepath('ems.master/CADPropertyTaxView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.CADBaseDetailsView', {
            url: '/CADBaseDetailsView',
            title: 'CADBaseDetailsView',
            templateUrl: helper.basepath('ems.master/CADBaseDetailsView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.CADComprehensiveDetailsView', {
            url: '/CADComprehensiveDetailsView',
            title: 'CADComprehensiveDetailsView',
            templateUrl: helper.basepath('ems.master/CADComprehensiveDetailsView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.MstCADCrimeReportCompanyView', {
            url: '/MstCADCrimeReportCompanyView',
            title: 'MstCADCrimeReportCompanyView',
            templateUrl: helper.basepath('ems.master/MstCADCrimeReportCompanyView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.MstCADCompanyCrimeCheckRecordAPI', {
            url: '/MstCADCompanyCrimeCheckRecordAPI',
            title: 'MstCADCompanyCrimeCheckRecordAPI',
            templateUrl: helper.basepath('ems.master/MstCADCompanyCrimeCheckRecordAPI.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

	.state('app.MstCADCreditCrimeCheckRecordAPI', {
	    url: '/MstCADCreditCrimeCheckRecordAPI',
	    title: 'MstCADCreditCrimeCheckRecordAPI',
	    templateUrl: helper.basepath('ems.master/MstCADCreditCrimeCheckRecordAPI.html?ver=' + version + '"'),
	    resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
	})

        .state('app.MstCADCrimeReportIndividualView', {
            url: '/MstCADCrimeReportIndividualView',
            title: 'MstCADCrimeReportIndividualView',
            templateUrl: helper.basepath('ems.master/MstCADCrimeReportIndividualView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

	.state('app.MstCADRMInstitutionView', {
	    url: '/MstCADRMInstitutionView',
	    title: 'MstCADRMInstitutionView',
	    templateUrl: helper.basepath('ems.master/MstCADRMInstitutionView.html?ver=' + version + '"'),
	    resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
	})

        .state('app.MstCADRMIndividualView', {
            url: '/MstCADRMIndividualView',
            title: 'MstCADRMIndividualView',
            templateUrl: helper.basepath('ems.master/MstCADRMIndividualView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.MstCADCreditCompanyDtlView', {
            url: '/MstCADCreditCompanyDtlView',
            title: 'MstCADCreditCompanyDtlView',
            templateUrl: helper.basepath('ems.master/MstCADCreditCompanyDtlView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.MstCADCreditIndividualDtlView', {
            url: '/MstCADCreditIndividualDtlView',
            title: 'MstCADCreditIndividualDtlView',
            templateUrl: helper.basepath('ems.master/MstCADCreditIndividualDtlView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.MstCADCreditGroupDtlView', {
            url: '/MstCADCreditGroupDtlView',
            title: 'MstCADCreditGroupDtlView',
            templateUrl: helper.basepath('ems.master/MstCADCreditGroupDtlView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.MstCadPendingApplicationView', {
            url: '/MstCadPendingApplicationView',
            title: 'MstCadPendingApplicationView',
            templateUrl: helper.basepath('ems.master/MstCadPendingApplicationView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        		.state('app.MstCADPendingInstitutionDtlAdd', {
        		    url: '/MstCADPendingInstitutionDtlAdd',
        		    title: 'MstCADPendingInstitutionDtlAdd',
        		    templateUrl: helper.basepath('ems.master/MstCADPendingInstitutionDtlAdd.html?ver=' + version + '"'),
        		    resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        		})

        .state('app.MstCADPendingIndividualDtlAdd', {
            url: '/MstCADPendingIndividualDtlAdd',
            title: 'MstCADPendingIndividualDtlAdd',
            templateUrl: helper.basepath('ems.master/MstCADPendingIndividualDtlAdd.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.MstCADPendingGroupDtlAdd', {
            url: '/MstCADPendingGroupDtlAdd',
            title: 'MstCADPendingGroupDtlAdd',
            templateUrl: helper.basepath('ems.master/MstCADPendingGroupDtlAdd.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.MstCADCreditIndividualAPI', {
            url: '/MstCADCreditIndividualAPI',
            title: 'MstCADCreditIndividualAPI',
            templateUrl: helper.basepath('ems.master/MstCADCreditIndividualAPI.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

          .state('app.MstCADCreditBankAccountDtlEdit', {
              url: '/MstCADCreditBankAccountDtlEdit',
              title: 'MstCADCreditBankAccountDtlEdit',
              templateUrl: helper.basepath('ems.master/MstCADCreditBankAccountDtlEdit.html?ver=' + version + '"'),
              resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
          })


        .state('app.MstCADCreditExistingBankDtlEdit', {
            url: '/MstCADCreditExistingBankDtlEdit',
            title: 'MstCADCreditExistingBankDtlEdit',
            templateUrl: helper.basepath('ems.master/MstCADCreditExistingBankDtlEdit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.MstCADCreditRepaymentDtlEdit', {
            url: '/MstCADCreditRepaymentDtlEdit',
            title: 'MstCADCreditRepaymentDtlEdit',
            templateUrl: helper.basepath('ems.master/MstCADCreditRepaymentDtlEdit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

         .state('app.MstCADCreditFSAView', {
             url: '/MstCADCreditFSAView',
             title: 'MstCADCreditFSAView',
             templateUrl: helper.basepath('ems.master/MstCADCreditFSAView.html?ver=' + version + '"'),
             resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
         })

        .state('app.MstRMCadUrnAcceptedCustomerDtls', {
            url: '/MstRMCadUrnAcceptedCustomerDtls',
            title: 'MstRMCadUrnAcceptedCustomerDtls',
            templateUrl: helper.basepath('ems.master/MstRMCadUrnAcceptedCustomerDtls.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

          .state('app.MstCADPSLCSADataFlaggingAdd', {
              url: '/MstCADPSLCSADataFlaggingAdd',
              title: 'MstCADPSLCSADataFlaggingAdd',
              templateUrl: helper.basepath('ems.master/MstCADPSLCSADataFlaggingAdd.html?ver=' + version + '"'),
              resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
          })

        .state('app.MstCADPSLCSAIndividualPSLDataFlagAdd', {
            url: '/MstCADPSLCSAIndividualPSLDataFlagAdd',
            title: 'MstCADPSLCSAIndividualPSLDataFlagAdd',
            templateUrl: helper.basepath('ems.master/MstCADPSLCSAIndividualPSLDataFlagAdd.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.MstCADPSLCSAGroupPSLDataFlagAdd', {
            url: '/MstCADPSLCSAGroupPSLDataFlagAdd',
            title: 'MstCADPSLCSAGroupPSLDataFlagAdd',
            templateUrl: helper.basepath('ems.master/MstCADPSLCSAGroupPSLDataFlagAdd.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.MstCADPSLCSADataFlaggingEdit', {
            url: '/MstCADPSLCSADataFlaggingEdit',
            title: 'MstCADPSLCSADataFlaggingEdit',
            templateUrl: helper.basepath('ems.master/MstCADPSLCSADataFlaggingEdit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.MstCADPSLCSAIndividualPSLDataFlagEdit', {
            url: '/MstCADPSLCSAIndividualPSLDataFlagEdit',
            title: 'MstCADPSLCSAIndividualPSLDataFlagEdit',
            templateUrl: helper.basepath('ems.master/MstCADPSLCSAIndividualPSLDataFlagEdit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.MstCADPSLCSAGroupPSLDataFlagEdit', {
            url: '/MstCADPSLCSAGroupPSLDataFlagEdit',
            title: 'MstCADPSLCSAGroupPSLDataFlagEdit',
            templateUrl: helper.basepath('ems.master/MstCADPSLCSAGroupPSLDataFlagEdit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.SysMstHRNotification', {
            url: '/SysMstHRNotification',
            title: 'SysMstHRNotification',
            templateUrl: helper.basepath('ems.system/SysMstHRNotification.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.Mstinternalrating', {
            url: '/MstInternalrating',
            title: 'MstInternalrating',
            templateUrl: helper.basepath('ems.master/MstInternalRating.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.Mstlivestock', {
            url: '/MstLiveStock',
            title: 'MstLiveStock',
            templateUrl: helper.basepath('ems.master/MstLiveStock.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.MstSAVerificationCheckerSummary', {
             url: '/MstSAVerificationCheckerSummary',
             title: 'MstSAVerificationCheckerSummary',
             templateUrl: helper.basepath('ems.master/MstSAVerificationCheckerSummary.html?ver=' + version + '"'),
             resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.MstSAVerificationMakerPendingSummary', {
            url: '/MstSAVerificationMakerPendingSummary',
            title: 'MstSAVerificationMakerPendingSummary',
            templateUrl: helper.basepath('ems.master/MstSAVerificationMakerPendingSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.MstSAVerificationMakerIndividualSummary', {
            url: '/MstSAVerificationMakerIndividualSummary',
            title: 'MstSAVerificationMakerIndividualSummary',
            templateUrl: helper.basepath('ems.master/MstSAVerificationMakerIndividualSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.MstSAVerificationMakerSummary', {
            url: '/MstSAVerificationMakerSummary',
            title: 'MstSAVerificationMakerSummary',
            templateUrl: helper.basepath('ems.master/MstSAVerificationMakerSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.MstSAVerificationMakerInitiatedPending', {
            url: '/MstSAVerificationMakerInitiatedPending',
            title: 'MstSAVerificationMakerInitiatedPending',
            templateUrl: helper.basepath('ems.master/MstSAVerificationMakerInitiatedPending.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.MstSAVerificationMakerInitiatedSummary', {
            url: '/MstSAVerificationMakerInitiatedSummary',
            title: 'MstSAVerificationMakerInitiatedSummary',
            templateUrl: helper.basepath('ems.master/MstSAVerificationMakerInitiatedSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.MstSAVerificationMakerInstitutionInitiated', {
            url: '/MstSAVerificationMakerInstitutionInitiated',
            title: 'MstSAVerificationMakerInstitutionInitiated',
            templateUrl: helper.basepath('ems.master/MstSAVerificationMakerInstitutionInitiated.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.MstSAVerificationMakerIndividualPending', {
            url: '/MstSAVerificationMakerIndividualPending',
            title: 'MstSAVerificationMakerIndividualPending',
            templateUrl: helper.basepath('ems.master/MstSAVerificationMakerIndividualPending.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.MstSAVerificationMakerIndInstSummary', {
            url: '/MstSAVerificationMakerIndInstSummary',
            title: 'MstSAVerificationMakerIndInstSummary',
            templateUrl: helper.basepath('ems.master/MstSAVerificationMakerIndInstSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.MstSAVerificationMakerIndividualInitiated', {
            url: '/MstSAVerificationMakerIndividualInitiated',
            title: 'MstSAVerificationMakerIndividualInitiated',
            templateUrl: helper.basepath('ems.master/MstSAVerificationMakerIndividualInitiated.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.MstSAVerificationPending', {
            url: '/MstSAVerificationPending',
            title: 'MstSAVerificationPending',
            templateUrl: helper.basepath('ems.master/MstSAVerificationPending.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.MstSAVerificationIndividualPending', {
            url: '/MstSAVerificationIndividualPending',
            title: 'MstSAVerificationIndividualPending',
            templateUrl: helper.basepath('ems.master/MstSAVerificationIndividualPending.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.MstSAVerificationInstitutionEdit', {
            url: '/MstSAVerificationInstitutionEdit',
            title: 'MstSAVerificationInstitutionEdit',
            templateUrl: helper.basepath('ems.master/MstSAVerificationInstitutionEdit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.MstRmMyTeamCustomerSummary', {
            url: '/MstRmMyTeamCustomerSummary',
            title: 'MstRmMyTeamCustomerSummary',
            templateUrl: helper.basepath('ems.master/MstRmMyTeamCustomerSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

            .state('app.MstMarketingSourceofContact', {
                 url: '/MstMarketingSourceofContact',
                 title: 'MstMarketingSourceofContact',
                 templateUrl: helper.basepath('ems.businessteam/MstMarketingSourceofContact.html?ver=' + version + '"'),
                 resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })

            .state('app.MstMarketingCallType', {
                url: '/MstMarketingCallType',
                title: 'MstMarketingCallType',
                templateUrl: helper.basepath('ems.businessteam/MstMarketingCallType.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })

            .state('app.MstMarketingTelecallingFunction', {
                url: '/MstMarketingTelecallingFunction',
                title: 'MstMarketingTelecallingFunction',
                templateUrl: helper.basepath('ems.businessteam/MstMarketingTelecallingFunction.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })

            .state('app.MstMarketingCallReceivedNumber', {
                url: '/MstMarketingCallReceivedNumber',
                title: 'MstMarketingCallReceivedNumber',
                templateUrl: helper.basepath('ems.businessteam/MstMarketingCallReceivedNumber.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })

            .state('app.MstMarketingSummary', {
                url: '/MstMarketingSummary',
                title: 'MstMarketingSummary',
                templateUrl: helper.basepath('ems.businessteam/MstMarketingSummary.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })

            .state('app.MstMarketingAdd', {
                url: '/MstMarketingAdd',
                title: 'MstMarketingAdd',
                templateUrl: helper.basepath('ems.businessteam/MstMarketingAdd.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })

            .state('app.MstAssignedMarketingSummary', {
                url: '/MstAssignedMarketingSummary',
                title: 'MstAssignedMarketingSummary',
                templateUrl: helper.basepath('ems.businessteam/MstAssignedMarketingSummary.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })

            .state('app.MstAssignedMarketingView', {
                url: '/MstAssignedMarketingView',
                title: 'MstAssignedMarketingView',
                templateUrl: helper.basepath('ems.businessteam/MstAssignedMarketingView.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })

            .state('app.MstClosedMarketingSummary', {
                url: '/MstClosedMarketingSummary',
                title: 'MstClosedMarketingSummary',
                templateUrl: helper.basepath('ems.businessteam/MstClosedMarketingSummary.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })

            .state('app.MstCompletedMarketingSummary', {
                url: '/MstCompletedMarketingSummary',
                title: 'MstCompletedMarketingSummary',
                templateUrl: helper.basepath('ems.businessteam/MstCompletedMarketingSummary.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })

            .state('app.MstFollowUpMarketingSummary', {
                url: '/MstFollowUpMarketingSummary',
                title: 'MstFollowUpMarketingSummary',
                templateUrl: helper.basepath('ems.businessteam/MstFollowUpMarketingSummary.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })

            .state('app.MstMarketingAssignedCalls', {
                url: '/MstMarketingAssignedCalls',
                title: 'MstMarketingAssignedCalls',
                templateUrl: helper.basepath('ems.businessteam/MstMarketingAssignedCalls.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })

            .state('app.MstMarketingAssignedCallView', {
                url: '/MstMarketingAssignedCallView',
                title: 'MstMarketingAssignedCallView',
                templateUrl: helper.basepath('ems.businessteam/MstMarketingAssignedCallView.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })

            .state('app.MstMarketingAssignView', {
                url: '/MstMarketingAssignView',
                title: 'MstMarketingAssignView',
                templateUrl: helper.basepath('ems.businessteam/MstMarketingAssignView.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })

            .state('app.MstMarketingCallResponse', {
                url: '/MstMarketingCallResponse',
                title: 'MstMarketingCallResponse',
                templateUrl: helper.basepath('ems.businessteam/MstMarketingCallResponse.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })

            .state('app.MstMarketingClose', {
                url: '/MstMarketingClose',
                title: 'MstMarketingClose',
                templateUrl: helper.basepath('ems.businessteam/MstMarketingClose.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })

            .state('app.MstMarketingClosedCall', {
                url: '/MstMarketingClosedCall',
                title: 'MstMarketingClosedCall',
                templateUrl: helper.basepath('ems.businessteam/MstMarketingClosedCall.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })

            .state('app.MstMarketingClosedCalls', {
                url: '/MstMarketingClosedCalls',
                title: 'MstMarketingClosedCalls',
                templateUrl: helper.basepath('ems.businessteam/MstMarketingClosedCalls.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })

            .state('app.MstMarketingClosedView', {
                url: '/MstMarketingClosedView',
                title: 'MstMarketingClosedView',
                templateUrl: helper.basepath('ems.businessteam/MstMarketingClosedView.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })

            .state('app.MstMarketingCompletedCall', {
                url: '/MstMarketingCompletedCall',
                title: 'MstMarketingCompletedCall',
                templateUrl: helper.basepath('ems.businessteam/MstMarketingCompletedCall.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })

            .state('app.MstMarketingCompletedCallSummary', {
                url: '/MstMarketingCompletedCallSummary',
                title: 'MstMarketingCompletedCallSummary',
                templateUrl: helper.basepath('ems.businessteam/MstMarketingCompletedCallSummary.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })

            .state('app.MstMarketingCompletedView', {
                url: '/MstMarketingCompletedView',
                title: 'MstMarketingCompletedView',
                templateUrl: helper.basepath('ems.businessteam/MstMarketingCompletedView.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })

            .state('app.MstMarketingCompleteView', {
                url: '/MstMarketingCompleteView',
                title: 'MstMarketingCompleteView',
                templateUrl: helper.basepath('ems.businessteam/MstMarketingCompleteView.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })

            .state('app.MstMarketingEdit', {
                url: '/MstMarketingEdit',
                title: 'MstMarketingEdit',
                templateUrl: helper.basepath('ems.businessteam/MstMarketingEdit.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })

            .state('app.MstMarketingFollowupCalls', {
                url: '/MstMarketingFollowupCalls',
                title: 'MstMarketingFollowupCalls',
                templateUrl: helper.basepath('ems.businessteam/MstMarketingFollowupCalls.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })

            .state('app.MstMarketingFollowupCall', {
                url: '/MstMarketingFollowupCall',
                title: 'MstMarketingFollowupCall',
                templateUrl: helper.basepath('ems.businessteam/MstMarketingFollowupCall.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })

            .state('app.MstMarketingFollowUpCallSummary', {
                url: '/MstMarketingFollowUpCallSummary',
                title: 'MstMarketingFollowUpCallSummary',
                templateUrl: helper.basepath('ems.businessteam/MstMarketingFollowUpCallSummary.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })

            .state('app.MstMarketingFollowupCallView', {
                url: '/MstMarketingFollowupCallView',
                title: 'MstMarketingFollowupCallView',
                templateUrl: helper.basepath('ems.businessteam/MstMarketingFollowupCallView.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })

            .state('app.MstMarketingMyAssignedCallSummary', {
                url: '/MstMarketingMyAssignedCallSummary',
                title: 'MstMarketingMyAssignedCallSummary',
                templateUrl: helper.basepath('ems.businessteam/MstMarketingMyAssignedCallSummary.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })

            .state('app.MstMarketingRejectedCallSummary', {
                url: '/MstMarketingRejectedCallSummary',
                title: 'MstMarketingRejectedCallSummary',
                templateUrl: helper.basepath('ems.businessteam/MstMarketingRejectedCallSummary.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })

            .state('app.MstMarketingReport', {
                url: '/MstMarketingReport',
                title: 'MstMarketingReport',
                templateUrl: helper.basepath('ems.businessteam/MstMarketingReport.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })

            .state('app.MstMarketingTaggedMemberSummary', {
                url: '/MstMarketingTaggedMemberSummary',
                title: 'MstMarketingTaggedMemberSummary',
                templateUrl: helper.basepath('ems.businessteam/MstMarketingTaggedMemberSummary.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })

            .state('app.MstMarketingTaggedMemberView', {
                url: '/MstMarketingTaggedMemberView',
                title: 'MstMarketingTaggedMemberView',
                templateUrl: helper.basepath('ems.businessteam/MstMarketingTaggedMemberView.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })

            .state('app.MstMarketingTransferCallSummary', {
                url: '/MstMarketingTransferCallSummary',
                title: 'MstMarketingTransferCallSummary',
                templateUrl: helper.basepath('ems.businessteam/MstMarketingTransferCallSummary.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })

            .state('app.MstMarketingWorkInprogressCallSummary', {
                url: '/MstMarketingWorkInprogressCallSummary',
                title: 'MstMarketingWorkInprogressCallSummary',
                templateUrl: helper.basepath('ems.businessteam/MstMarketingWorkInprogressCallSummary.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })

            .state('app.MstRejectedMarketingSummary', {
                url: '/MstRejectedMarketingSummary',
                title: 'MstRejectedMarketingSummary',
                templateUrl: helper.basepath('ems.businessteam/MstRejectedMarketingSummary.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })

            .state('app.MstMarketingReportView', {
                url: '/MstMarketingReportView',
                title: 'MstMarketingReportView',
                templateUrl: helper.basepath('ems.businessteam/MstMarketingReportView.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })

            .state('app.MstMarketingRejectedCallView', {
                url: '/MstMarketingRejectedCallView',
                title: 'MstMarketingRejectedCallView',
                templateUrl: helper.basepath('ems.businessteam/MstMarketingRejectedCallView.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })
           
            .state('app.MstEquipment', {
                url: '/MstEquipment',
                title: 'MstEquipment',
                templateUrl: helper.basepath('ems.master/MstEquipment.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })

             //Moved to test

            .state('app.MstApplCCApproved', {
                url: '/MstApplCCApproved',
                title: 'MstApplCCApproved',
                templateUrl: helper.basepath('ems.master/MstApplCCApproved.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })

            .state('app.MstUpcomingBusinessApprovalSummary', {
                url: '/MstUpcomingBusinessApprovalSummary',
                title: 'MstUpcomingBusinessApprovalSummary',
                templateUrl: helper.basepath('ems.master/MstUpcomingBusinessApprovalSummary.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })

            .state('app.MstUpcomingCreditApprovalSummary', {
                url: '/MstUpcomingCreditApprovalSummary',
                title: 'MstUpcomingCreditApprovalSummary',
                templateUrl: helper.basepath('ems.master/MstUpcomingCreditApprovalSummary.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })
            
            //not moved to test
            .state('app.samforms', {
                url: '/samforms',
                title: 'samforms',
                templateUrl: helper.basepath('ems.master/samforms.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })

             .state('app.MstSAOnboardingBussDevtVerificationSummary', {
                 url: '/MstSAOnboardingBussDevtVerificationSummary',
                 title: 'MstSAOnboardingBussDevtVerificationSummary',
                 templateUrl: helper.basepath('ems.master/MstSAOnboardingBussDevtVerificationSummary.html?ver=' + version + '"'),
                 resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
             })

            .state('app.MstSAonboardingSBAVerifyindividual', {
                url: '/MstSAonboardingSBAVerifyindividual',
                title: 'MstSAonboardingSBAVerifyindividual',
                templateUrl: helper.basepath('ems.master/MstSAonboardingSBAVerifyindividual.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })

             .state('app.MstSAOnboardingBussDevtVerification', {
                 url: '/MstSAOnboardingBussDevtVerification',
                 title: 'MstSAOnboardingBussDevtVerification',
                 templateUrl: helper.basepath('ems.master/MstSAOnboardingBussDevtVerification.html?ver=' + version + '"'),
                 resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
             })

            .state('app.MstSAOnboardingBussDevtVerificationIndividual', {
                url: '/MstSAOnboardingBussDevtVerificationIndividual',
                title: 'MstSAOnboardingBussDevtVerificationIndividual',
                templateUrl: helper.basepath('ems.master/MstSAOnboardingBussDevtVerificationIndividual.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })

            .state('app.MstSAOnboardingBussDevtVerifcationSummary', {
                url: '/MstSAOnboardingBussDevtVerifcationSummary',
                title: 'MstSAOnboardingBussDevtVerifcationSummary',
                templateUrl: helper.basepath('ems.master/MstSAOnboardingBussDevtVerifcationSummary.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })

            .state('app.MstSAOnboardingBussDevtVerificationIndividualSummary', {
                url: '/MstSAOnboardingBussDevtVerificationIndividualSummary',
                title: 'MstSAOnboardingBussDevtVerificationIndividualSummary',
                templateUrl: helper.basepath('ems.master/MstSAOnboardingBussDevtVerificationIndividualSummary.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })

            .state('app.sbaportalcompany', {
                url: '/sbaportalcompany',
                title: 'sbaportalcompany',
                templateUrl: helper.basepath('ems.master/sbaportalcompany.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })

            .state('app.MstSAOnboardingSBAVerification', {
                url: '/MstSAOnboardingSBAVerification',
                title: 'MstSAOnboardingSBAVerification',
                templateUrl: helper.basepath('ems.master/MstSAOnboardingSBAVerification.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })

            .state('app.MstSAonboardingSBAindividualverification', {
                url: '/MstSAonboardingSBAindividualverification',
                title: 'MstSAonboardingSBAindividualverification',
                templateUrl: helper.basepath('ems.master/MstSAonboardingSBAindividualverification.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })

             .state('app.MstSAVerificationCheckerInstitutionInitiatedView', {
                 url: '/MstSAVerificationCheckerInstitutionInitiatedView',
                 title: 'MstSAVerificationCheckerInstitutionInitiatedView',
                 templateUrl: helper.basepath('ems.master/MstSAVerificationCheckerInstitutionInitiatedView.html?ver=' + version + '"'),
                 resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
             })

            .state('app.MstSAVerificationCheckerIndividualInitiatedView', {
                url: '/MstSAVerificationCheckerIndividualInitiatedView',
                title: 'MstSAVerificationCheckerIndividualInitiatedView',
                templateUrl: helper.basepath('ems.master/MstSAVerificationCheckerIndividualInitiatedView.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })

            .state('app.MstSAOnboardingIndividualApprovalView', {
                url: '/MstSAOnboardingIndividualApprovalView',
                title: 'MstSAOnboardingIndividualApprovalView',
                templateUrl: helper.basepath('ems.master/MstSAOnboardingIndividualApprovalView.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })

            .state('app.MstSAVerificationApprovedSummary', {
                url: '/MstSAVerificationApprovedSummary',
                title: 'MstSAVerificationApprovedSummary',
                templateUrl: helper.basepath('ems.master/MstSAVerificationApprovedSummary.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })

            .state('app.MstSAVerificationApprovedIndSummary', {
                url: '/MstSAVerificationApprovedIndSummary',
                title: 'MstSAVerificationApprovedIndSummary',
                templateUrl: helper.basepath('ems.master/MstSAVerificationApprovedIndSummary.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })

            .state('app.MstSAVerificationApprovedView', {
                url: '/MstSAVerificationApprovedView',
                title: 'MstSAVerificationApprovedView',
                templateUrl: helper.basepath('ems.master/MstSAVerificationApprovedView.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })

            .state('app.MstSAVerificationApprovedIndView', {
                url: '/MstSAVerificationApprovedIndView',
                title: 'MstSAVerificationApprovedIndView',
                templateUrl: helper.basepath('ems.master/MstSAVerificationApprovedIndView.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })

            .state('app.MstSAVerificationRejectedSummary', {
                url: '/MstSAVerificationRejectedSummary',
                title: 'MstSAVerificationRejectedSummary',
                templateUrl: helper.basepath('ems.master/MstSAVerificationRejectedSummary.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })

            .state('app.MstSAVerificationRejectedIndSummary', {
                url: '/MstSAVerificationRejectedIndSummary',
                title: 'MstSAVerificationRejectedIndSummary',
                templateUrl: helper.basepath('ems.master/MstSAVerificationRejectedIndSummary.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })

            .state('app.MstSAVerificationRejectedView', {
                url: '/MstSAVerificationRejectedView',
                title: 'MstSAVerificationRejectedView',
                templateUrl: helper.basepath('ems.master/MstSAVerificationRejectedView.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })

            .state('app.MstSAVerificationRejectedIndView', {
                url: '/MstSAVerificationRejectedIndView',
                title: 'MstSAVerificationRejectedIndView',
                templateUrl: helper.basepath('ems.master/MstSAVerificationRejectedIndView.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })
            //moved to test
            .state('app.MstApplGroupdtlView', {
                url: '/MstApplGroupdtlView',
                title: 'MstApplGroupdtlView',
                templateUrl: helper.basepath('ems.master/MstApplGroupdtlView.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })

             .state('app.MstCADGroupdtlView', {
                 url: '/MstCADGroupdtlView',
                 title: 'MstCADGroupdtlView',
                 templateUrl: helper.basepath('ems.master/MstCADGroupdtlView.html?ver=' + version + '"'),
                 resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
             })
            //not moved to test

            .state('app.MstSAonboardingSBAinstitutionverification', {
                url: '/MstSAonboardingSBAinstitutionverification',
                title: 'MstSAonboardingSBAinstitutionverification',
                templateUrl: helper.basepath('ems.master/MstSAonboardingSBAinstitutionverification.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })
            //moved to test
            .state('app.MstRERule', {
                url: '/MstRERule',
                title: 'MstRERule',
                templateUrl: helper.basepath('ems.master/MstRERule.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })

          .state('app.MstRETemplate', {
              url: '/MstRETemplate',
              title: 'MstRETemplate',
              templateUrl: helper.basepath('ems.master/MstRETemplate.html?ver=' + version + '"'),
              resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
          })

        .state('app.MstREAddTemplate', {
            url: '/MstREAddTemplate',
            title: 'MstREAddTemplate',
            templateUrl: helper.basepath('ems.master/MstREAddTemplate.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

         .state('app.MstREDoConfiguration', {
             url: '/MstREDoConfiguration',
             title: 'MstREDoConfiguration',
             templateUrl: helper.basepath('ems.master/MstREDoConfiguration.html?ver=' + version + '"'),
             resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
         })

        .state('app.MstBREExecution', {
            url: '/MstBREExecution',
            title: 'MstBREExecution',
            templateUrl: helper.basepath('ems.master/MstBREExecution.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

          .state('app.MstBREExecutionView', {
              url: '/MstBREExecutionView',
              title: 'MstBREExecutionView',
              templateUrl: helper.basepath('ems.master/MstBREExecutionView.html?ver=' + version + '"'),
              resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular', 'flot-chart', 'flot-chart-plugins', 'weather-icons')
          })

        .state('app.MstSanctionMISReport', {
            url: '/MstSanctionMISReport',
            title: 'MstSanctionMISReport',
            templateUrl: helper.basepath('ems.master/MstSanctionMISReport.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.MstSanctionMISReportView', {
            url: '/MstSanctionMISReportView',
            title: 'MstSanctionMISReportView',
            templateUrl: helper.basepath('ems.master/MstSanctionMISReportView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        //------- Hr loan menus starts -------
          .state('app.MstHRLoanTypeofFinancialAssistance', {
              url: '/MstHRLoanTypeofFinancialAssistance',
              title: 'MstHRLoanTypeofFinancialAssistance',
              templateUrl: helper.basepath('ems.hrloan/MstHRLoanTypeofFinancialAssistance.html?ver=' + version + '"'),
              resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
          })

       .state('app.MstHRLoanPurpose', {
           url: '/MstHRLoanPurpose',
           title: 'MstHRLoanPurpose',
           templateUrl: helper.basepath('ems.hrloan/MstHRLoanPurpose.html?ver=' + version + '"'),
           resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
       })

       .state('app.MstHRLoanSeverity', {
           url: '/MstHRLoanSeverity',
           title: 'MstHRLoanSeverity',
           templateUrl: helper.basepath('ems.hrloan/MstHRLoanSeverity.html?ver=' + version + '"'),
           resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
       })

        .state('app.MstHRLoanTenure', {
            url: '/MstHRLoanTenure',
            title: 'MstHRLoanTenure',
            templateUrl: helper.basepath('ems.hrloan/MstHRLoanTenure.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.MstHRLoanTenureAdd', {
            url: '/MstHRLoanTenureAdd',
            title: 'MstHRLoanTenureAdd',
            templateUrl: helper.basepath('ems.hrloan/MstHRLoanTenureAdd.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })


        .state('app.MstHRLoanTenureEdit', {
            url: '/MstHRLoanTenureEdit',
            title: 'MstHRLoanTenureEdit',
            templateUrl: helper.basepath('ems.hrloan/MstHRLoanTenureEdit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })


        .state('app.MstHRLoanTenureView', {
            url: '/MstHRLoanTenureView',
            title: 'MstHRLoanTenureView',
            templateUrl: helper.basepath('ems.hrloan/MstHRLoanTenureView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })


        .state('app.MstHRLoanTermsandConditions', {
            url: '/MstHRLoanTermsandConditions',
            title: 'MstHRLoanTermsandConditions',
            templateUrl: helper.basepath('ems.hrloan/MstHRLoanTermsandConditions.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.MstHRLoanHRDocument', {
            url: '/MstHRLoanHRDocument',
            title: 'MstHRLoanHRDocument',
            templateUrl: helper.basepath('ems.hrloan/MstHRLoanHRDocument.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.MstHRLoanHRDocumentAdd', {
            url: '/MstHRLoanHRDocumentAdd',
            title: 'MstHRLoanHRDocumentAdd',
            templateUrl: helper.basepath('ems.hrloan/MstHRLoanHRDocumentAdd.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstHRLoanHRDocumentEdit', {
            url: '/MstHRLoanHRDocumentEdit',
            title: 'MstHRLoanHRDocumentEdit',
            templateUrl: helper.basepath('ems.hrloan/MstHRLoanHRDocumentEdit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstHRLoanHRDocumentView', {
            url: '/MstHRLoanHRDocumentView',
            title: 'MstHRLoanHRDocumentView',
            templateUrl: helper.basepath('ems.hrloan/MstHRLoanHRDocumentView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.MstHRLoanHRMappingApprovals', {
            url: '/MstHRLoanHRMappingApprovals',
            title: 'MstHRLoanHRMappingApprovals',
            templateUrl: helper.basepath('ems.hrloan/MstHRLoanHRMappingApprovals.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.AprHRLoanHRAdvanceApprovals', {
            url: '/AprHRLoanHRAdvanceApprovals',
            title: 'AprHRLoanHRAdvanceApprovals',
            templateUrl: helper.basepath('ems.hrloan/AprHRLoanHRAdvanceApprovals.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.AprHRLoanHRAdvanceApprovalsSummary', {
            url: '/AprHRLoanHRAdvanceApprovalsSummary',
            title: 'AprHRLoanHRAdvanceApprovalsSummary',
            templateUrl: helper.basepath('ems.hrloan/AprHRLoanHRAdvanceApprovalsSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.AprHRLoanHRAdvanceApprovedSummary', {
            url: '/AprHRLoanHRAdvanceApprovedSummary',
            title: 'AprHRLoanHRAdvanceApprovedSummary',
            templateUrl: helper.basepath('ems.hrloan/AprHRLoanHRAdvanceApprovedSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.AprHRLoanHRAdvanceRejectedSummary', {
            url: '/AprHRLoanHRAdvanceRejectedSummary',
            title: 'AprHRLoanHRAdvanceRejectedSummary',
            templateUrl: helper.basepath('ems.hrloan/AprHRLoanHRAdvanceRejectedSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.TrnHRLoanHRVerifications', {
            url: '/TrnHRLoanHRVerifications',
            title: 'TrnHRLoanHRVerifications',
            templateUrl: helper.basepath('ems.hrloan/TrnHRLoanHRVerifications.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.TrnHRLoanHRVerificationsSummary', {
            url: '/TrnHRLoanHRVerificationsSummary',
            title: 'TrnHRLoanHRVerificationsSummary',
            templateUrl: helper.basepath('ems.hrloan/TrnHRLoanHRVerificationsSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.TrnHRLoanHRVerificationsApprovedSummary', {
            url: '/TrnHRLoanHRVerificationsApprovedSummary',
            title: 'TrnHRLoanHRVerificationsApprovedSummary',
            templateUrl: helper.basepath('ems.hrloan/TrnHRLoanHRVerificationsApprovedSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.TrnHRLoanHRVerificationsRejectedSummary', {
            url: '/TrnHRLoanHRVerificationsRejectedSummary',
            title: 'TrnHRLoanHRVerificationsRejectedSummary',
            templateUrl: helper.basepath('ems.hrloan/TrnHRLoanHRVerificationsRejectedSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.MstHRLoanAddRequest', {
            url: '/MstHRLoanAddRequest',
            title: 'MstHRLoanAddRequest',
            templateUrl: helper.basepath('ems.hrloan/MstHRLoanAddRequest.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstHRLoanViewRequest', {
            url: '/MstHRLoanViewRequest',
            title: 'MstHRLoanViewRequest',
            templateUrl: helper.basepath('ems.hrloan/MstHRLoanViewRequest.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstHRLoanEditRequest', {
            url: '/MstHRLoanEditRequest',
            title: 'MstHRLoanEditRequest',
            templateUrl: helper.basepath('ems.hrloan/MstHRLoanEditRequest.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstHRLoanRaiseRequest', {
            url: '/MstHRLoanRaiseRequest',
            title: 'MstHRLoanRaiseRequest',
            templateUrl: helper.basepath('ems.hrloan/MstHRLoanRaiseRequest.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        // ------ Hr loan menus end -------

        .state('app.MstCovenantPeriod', {
            url: '/MstCovenantPeriod',
            title: 'MstCovenantPeriod',
            templateUrl: helper.basepath('ems.master/MstCovenantPeriod.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.AtmTrnSampleAgainstObservationScore', {
            url: '/AtmTrnSampleAgainstObservationScore',
            title: 'AtmTrnSampleAgainstObservationScore',
            templateUrl: helper.basepath('ems.audit/AtmTrnSampleAgainstObservationScore.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.AtmTrnSampleQueryAgainstObservationScore', {
            url: '/AtmTrnSampleQueryAgainstObservationScore',
            title: 'AtmTrnSampleQueryAgainstObservationScore',
            templateUrl: helper.basepath('ems.audit/AtmTrnSampleQueryAgainstObservationScore.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.MstCreditMappingEdit', {
            url: '/MstCreditMappingEdit',
            title: 'MstCreditMappingEdit',
            templateUrl: helper.basepath('ems.master/MstCreditMappingEdit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.MstCreditMappingAdd', {
            url: '/MstCreditMappingAdd',
            title: 'MstCreditMappingAdd',
            templateUrl: helper.basepath('ems.master/MstCreditMappingAdd.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.MstCreditMappingDetails', {
            url: '/MstCreditMappingDetails',
            title: 'MstCreditMappingDetails',
            templateUrl: helper.basepath('ems.master/MstCreditMappingDetails.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.BaseDetailsView', {
            url: '/BaseDetailsView',
            title: 'BaseDetailsView',
            templateUrl: helper.basepath('ems.master/BaseDetailsView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.ComprehensiveDetailsView', {
            url: '/ComprehensiveDetailsView',
            title: 'ComprehensiveDetailsView',
            templateUrl: helper.basepath('ems.master/ComprehensiveDetailsView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

         .state('app.AtmTrnAuditorMakerObservationView', {
             url: '/AtmTrnAuditorMakerObservationView',
             title: 'AtmTrnAuditorMakerObservationView',
             templateUrl: helper.basepath('ems.audit/AtmTrnAuditorMakerObservationView.html?ver=' + version + '"'),
             resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
         })

        .state('app.AtmTrnAuditorCheckerObservationView', {
            url: '/AtmTrnAuditorCheckerObservationView',
            title: 'AtmTrnAuditorCheckerObservationView',
            templateUrl: helper.basepath('ems.audit/AtmTrnAuditorCheckerObservationView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.AtmTrnSampleAgainstCheckerObservationScore', {
            url: '/AtmTrnSampleAgainstCheckerObservationScore',
            title: 'AtmTrnSampleAgainstCheckerObservationScore',
            templateUrl: helper.basepath('ems.audit/AtmTrnSampleAgainstCheckerObservationScore.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.MstSBACodeCreationSummary', {
            url: '/MstSBACodeCreationSummary',
            title: 'MstSBACodeCreationSummary',
            templateUrl: helper.basepath('ems.master/MstSBACodeCreationSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

         .state('app.MstSAOnboardingInstitutionCodecreation', {
             url: '/MstSAOnboardingInstitutionCodecreation',
             title: 'MstSAOnboardingInstitutionCodecreation',
             templateUrl: helper.basepath('ems.master/MstSAOnboardingInstitutionCodecreation.html?ver=' + version + '"'),
             resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
         })

          .state('app.MstSAOnboardingIndividualCodecreationView', {
              url: '/MstSAOnboardingIndividualCodecreationView',
              title: 'MstSAOnboardingIndividualCodecreationView',
              templateUrl: helper.basepath('ems.master/MstSAOnboardingIndividualCodecreationView.html?ver=' + version + '"'),
              resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
          })

           .state('app.MstSAOnboardingCodeApprovalInsSummary', {
               url: '/MstSAOnboardingCodeApprovalInsSummary',
               title: 'MstSAOnboardingCodeApprovalInsSummary',
               templateUrl: helper.basepath('ems.master/MstSAOnboardingCodeApprovalInsSummary.html?ver=' + version + '"'),
               resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
           })

            .state('app.MstSAOnboardingCodeApprovalIndSummary', {
                url: '/MstSAOnboardingCodeApprovalIndSummary',
                title: 'MstSAOnboardingCodeApprovalIndSummary',
                templateUrl: helper.basepath('ems.master/MstSAOnboardingCodeApprovalIndSummary.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
            })

          .state('app.AprHRLoanHRHeadApprovalsSummary', {
              url: '/AprHRLoanHRHeadApprovalsSummary',
              title: 'AprHRLoanHRHeadApprovalsSummary',
              templateUrl: helper.basepath('ems.hrloan/AprHRLoanHRHeadApprovalsSummary.html?ver=' + version + '"'),
              resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
          })

           .state('app.AprHRLoanHRHeadApprovals', {
               url: '/AprHRLoanHRHeadApprovals',
               title: 'AprHRLoanHRHeadApprovals',
               templateUrl: helper.basepath('ems.hrloan/AprHRLoanHRHeadApprovals.html?ver=' + version + '"'),
               resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
           })

            .state('app.AprHRLoanHRHeadApprovedSummary', {
                url: '/AprHRLoanHRHeadApprovedSummary',
                title: 'AprHRLoanHRHeadApprovedSummary',
                templateUrl: helper.basepath('ems.hrloan/AprHRLoanHRHeadApprovedSummary.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })

           .state('app.AprHRLoanHRHeadRejectedSummary', {
               url: '/AprHRLoanHRHeadRejectedSummary',
               title: 'AprHRLoanHRHeadRejectedSummary',
               templateUrl: helper.basepath('ems.hrloan/AprHRLoanHRHeadRejectedSummary.html?ver=' + version + '"'),
               resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
           })
        
           .state('app.MstSAOnboardingInstitutionBDView', {
              url: '/MstSAOnboardingInstitutionBDView',
              title: 'MstSAOnboardingInstitutionBDView',
              templateUrl: helper.basepath('ems.master/MstSAOnboardingInstitutionBDView.html?ver=' + version + '"'),
              resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
           })

            .state('app.MstSAOnboardingIndividualBDView', {
                url: '/MstSAOnboardingIndividualBDView',
                title: 'MstSAOnboardingIndividualBDView',
                templateUrl: helper.basepath('ems.master/MstSAOnboardingIndividualBDView.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })

            .state('app.AtmTrnSampleAgainstMakerObservationScoreView', {
                url: '/AtmTrnSampleAgainstMakerObservationScoreView',
                title: 'AtmTrnSampleAgainstMakerObservationScoreView',
                templateUrl: helper.basepath('ems.audit/AtmTrnSampleAgainstMakerObservationScoreView.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })

            .state('app.AtmTrnSampleAgainstCheckerObservationScoreView', {
                url: '/AtmTrnSampleAgainstCheckerObservationScoreView',
                title: 'AtmTrnSampleAgainstCheckerObservationScoreView',
                templateUrl: helper.basepath('ems.audit/AtmTrnSampleAgainstCheckerObservationScoreView.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })

            .state('app.MstBuyerReport', {
                url: '/MstBuyerReport',
                title: 'MstBuyerReport',
                templateUrl: helper.basepath('ems.master/MstBuyerReport.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })

            .state('app.AprHRLoanHRAdvanceApprovalsView', {
                url: '/AprHRLoanHRAdvanceApprovalsView',
                title: 'AprHRLoanHRAdvanceApprovalsView',
                templateUrl: helper.basepath('ems.hrloan/AprHRLoanHRAdvanceApprovalsView.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })


            .state('app.AprHRLoanHRAdvanceApprovedView', {
                url: '/AprHRLoanHRAdvanceApprovedView',
                title: 'AprHRLoanHRAdvanceApprovedView',
                templateUrl: helper.basepath('ems.hrloan/AprHRLoanHRAdvanceApprovedView.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })


           .state('app.AprHRLoanHRAdvanceRejectedView', {
               url: '/AprHRLoanHRAdvanceRejectedView',
               title: 'AprHRLoanHRAdvanceRejectedView',
               templateUrl: helper.basepath('ems.hrloan/AprHRLoanHRAdvanceRejectedView.html?ver=' + version + '"'),
               resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
           })


           .state('app.AprHRLoanHRHeadApprovalsView', {
               url: '/AprHRLoanHRHeadApprovalsView',
               title: 'AprHRLoanHRHeadApprovalsView',
               templateUrl: helper.basepath('ems.hrloan/AprHRLoanHRHeadApprovalsView.html?ver=' + version + '"'),
               resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
           })

           .state('app.AprHRLoanHRHeadApprovedView', {
               url: '/AprHRLoanHRHeadApprovedView',
               title: 'AprHRLoanHRHeadApprovedView',
               templateUrl: helper.basepath('ems.hrloan/AprHRLoanHRHeadApprovedView.html?ver=' + version + '"'),
               resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
           })


         .state('app.AprHRLoanHRHeadRejectedView', {
             url: '/AprHRLoanHRHeadRejectedView',
             title: 'AprHRLoanHRHeadRejectedView',
             templateUrl: helper.basepath('ems.hrloan/AprHRLoanHRHeadRejectedView.html?ver=' + version + '"'),
             resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
         })

         .state('app.MstBusinessRevokeSummary', {
             url: '/MstBusinessRevokeSummary',
             title: 'MstBusinessRevokeSummary',
             templateUrl: helper.basepath('ems.master/MstBusinessRevokeSummary.html?ver=' + version + '"'),
             resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
         })
        .state('app.MstBusinessRejectRevoke', {
            url: '/MstBusinessRejectRevoke',
            title: 'MstBusinessRejectRevoke',
            templateUrl: helper.basepath('ems.master/MstBusinessRejectRevoke.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstBusinessHoldRevokeSummary', {
            url: '/MstBusinessHoldRevokeSummary',
            title: 'MstBusinessHoldRevokeSummary',
            templateUrl: helper.basepath('ems.master/MstBusinessHoldRevokeSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstBusinessRevokeHistory', {
            url: '/MstBusinessRevokeHistory',
            title: 'MstBusinessRevokeHistory',
            templateUrl: helper.basepath('ems.master/MstBusinessRevokeHistory.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
         .state('app.MstBusinessRevokedApplSummary', {
             url: '/MstBusinessRevokedApplSummary',
             title: 'MstBusinessRevokedApplSummary',
             templateUrl: helper.basepath('ems.master/MstBusinessRevokedApplSummary.html?ver=' + version + '"'),
             resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
         })
         .state('app.MstCreditRevokeSummary', {
             url: '/MstCreditRevokeSummary',
             title: 'MstCreditRevokeSummary',
             templateUrl: helper.basepath('ems.master/MstCreditRevokeSummary.html?ver=' + version + '"'),
             resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
         })
        .state('app.MstCreditHoldRevokeSummary', {
            url: '/MstCreditHoldRevokeSummary',
            title: 'MstCreditHoldRevokeSummary',
            templateUrl: helper.basepath('ems.master/MstCreditHoldRevokeSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstCreditRevokedApplSummary', {
            url: '/MstCreditRevokedApplSummary',
            title: 'MstCreditRevokedApplSummary',
            templateUrl: helper.basepath('ems.master/MstCreditRevokedApplSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstCreditRejectHoldRevoke', {
            url: '/MstCreditRejectHoldRevoke',
            title: 'MstCreditRejectHoldRevoke',
            templateUrl: helper.basepath('ems.master/MstCreditRejectHoldRevoke.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstCreditRejectHoldRevokeHistory', {
            url: '/MstCreditRejectHoldRevokeHistory',
            title: 'MstCreditRejectHoldRevokeHistory',
            templateUrl: helper.basepath('ems.master/MstCreditRejectHoldRevokeHistory.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        //5th Sep routes committed
        
        .state('app.TrnHRLoanHRPaymentSummary', {
                url: '/TrnHRLoanHRPaymentSummary',
                title: 'TrnHRLoanHRPaymentSummary',
                templateUrl: helper.basepath('ems.hrloan/TrnHRLoanHRPaymentSummary.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })

        .state('app.TrnHRLoanHRPaymentApprovals', {
                url: '/TrnHRLoanHRPaymentApprovals',
                title: 'TrnHRLoanHRPaymentApprovals',
                templateUrl: helper.basepath('ems.hrloan/TrnHRLoanHRPaymentApprovals.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })
        .state('app.TrnHRLoanHRPaymentApprovalsView', {
                url: '/TrnHRLoanHRPaymentApprovalsView',
                title: 'TrnHRLoanHRPaymentApprovalsView',
                templateUrl: helper.basepath('ems.hrloan/TrnHRLoanHRPaymentApprovalsView.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })

        .state('app.TrnHRLoanHRPaymentApprovedSummary', {
                url: '/TrnHRLoanHRPaymentApprovedSummary',
                title: 'TrnHRLoanHRPaymentApprovedSummary',
                templateUrl: helper.basepath('ems.hrloan/TrnHRLoanHRPaymentApprovedSummary.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })
        .state('app.TrnHRLoanHRPaymentApprovedView', {
                url: '/TrnHRLoanHRPaymentApprovedView',
                title: 'TrnHRLoanHRPaymentApprovedView',
                templateUrl: helper.basepath('ems.hrloan/TrnHRLoanHRPaymentApprovedView.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })

        .state('app.TrnHRLoanHRPaymentRejectedSummary', {
                url: '/TrnHRLoanHRPaymentRejectedSummary',
                title: 'TrnHRLoanHRPaymentRejectedSummary',
                templateUrl: helper.basepath('ems.hrloan/TrnHRLoanHRPaymentRejectedSummary.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })

        .state('app.TrnHRLoanHRPaymentRejectedView', {
                url: '/TrnHRLoanHRPaymentRejectedView',
                title: 'TrnHRLoanHRPaymentRejectedView',
                templateUrl: helper.basepath('ems.hrloan/TrnHRLoanHRPaymentRejectedView.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })

            .state('app.AtmTrnAuditorCheckerOpenView', {
                url: '/AtmTrnAuditorCheckerOpenView',
                title: 'AtmTrnAuditorCheckerOpenView',
                templateUrl: helper.basepath('ems.audit/AtmTrnAuditorCheckerOpenView.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })
    
            .state('app.AtmTrnMyAuditTaskAuditeeMakerView', {
                url: '/AtmTrnMyAuditTaskAuditeeMakerView',
                title: 'AtmTrnMyAuditTaskAuditeeMakerView',
                templateUrl: helper.basepath('ems.audit/AtmTrnMyAuditTaskAuditeeMakerView.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            }) 
            
             .state('app.AtmTrnSampleAgainstApproverObservationScore', {
                url: '/AtmTrnSampleAgainstApproverObservationScore',
                title: 'AtmTrnSampleAgainstApproverObservationScore',
                templateUrl: helper.basepath('ems.audit/AtmTrnSampleAgainstApproverObservationScore.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })    
           
            .state('app.MstSAOnboardingBDVerificationTrainingStatus', {
                 url: '/MstSAOnboardingBDVerificationTrainingStatus',
                 title: 'MstSAOnboardingBDVerificationTrainingStatus',
                 templateUrl: helper.basepath('ems.master/MstSAOnboardingBDVerificationTrainingStatus.html?ver=' + version + '"'),
                 resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })    
            .state('app.MstSAOnboardingBDVerificationTrainingStatusindividual', {
                url: '/MstSAOnboardingBDVerificationTrainingStatusindividual',
                title: 'MstSAOnboardingBDVerificationTrainingStatusindividual',
                templateUrl: helper.basepath('ems.master/MstSAOnboardingBDVerificationTrainingStatusindividual.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
   
            })
            .state('app.MstSAOnboardingBussDevRejectedSummary', {
                 url: '/MstSAOnboardingBussDevRejectedSummary',
                 title: 'MstSAOnboardingBussDevRejectedSummary',
                 templateUrl: helper.basepath('ems.master/MstSAOnboardingBussDevRejectedSummary.html?ver=' + version + '"'),
                 resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })
            .state('app.MstSAOnboardingBussDevIndividualRejectedSummary', {
                 url: '/MstSAOnboardingBussDevIndividualRejectedSummary',
                 title: 'MstSAOnboardingBussDevIndividualRejectedSummary',
                 templateUrl: helper.basepath('ems.master/MstSAOnboardingBussDevIndividualRejectedSummary.html?ver=' + version + '"'),
                 resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })
            .state('app.MstSAOnboardingBussDevtInstitutionRejectedView', {
                  url: '/MstSAOnboardingBussDevtInstitutionRejectedView',
                  title: 'MstSAOnboardingBussDevtInstitutionRejectedView',
                  templateUrl: helper.basepath('ems.master/MstSAOnboardingBussDevtInstitutionRejectedView.html?ver=' + version + '"'),
                 resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })
            .state('app.MstSAOnboardingBussDevtIndividualRejectedView', {
                  url: '/MstSAOnboardingBussDevtIndividualRejectedView',
                  title: 'MstSAOnboardingBussDevtIndividualRejectedView',
                 templateUrl: helper.basepath('ems.master/MstSAOnboardingBussDevtIndividualRejectedView.html?ver=' + version + '"'),
                 resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
             })
             .state('app.AtmTrnAuditeeCheckerObservationView', {
                url: '/AtmTrnAuditeeCheckerObservationView',
                title: 'AtmTrnAuditeeCheckerObservationView',
                templateUrl: helper.basepath('ems.audit/AtmTrnAuditeeCheckerObservationView.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })
            .state('app.AtmTrnMyAuditTaskAuditeeMakerObservationScoreView', {
                url: '/AtmTrnMyAuditTaskAuditeeMakerObservationScoreView',
                title: 'AtmTrnMyAuditTaskAuditeeMakerObservationScoreView',
                templateUrl: helper.basepath('ems.audit/AtmTrnMyAuditTaskAuditeeMakerObservationScoreView.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })
            .state('app.AtmTrnSampleAgainstApproverObservationScoreView', {
                url: '/AtmTrnSampleAgainstApproverObservationScoreView',
                title: 'AtmTrnSampleAgainstApproverObservationScoreView',
                templateUrl: helper.basepath('ems.audit/AtmTrnSampleAgainstApproverObservationScoreView.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })
           .state('app.AtmTrnAuditorApproverObservationView', {
                url: '/AtmTrnAuditorApproverObservationView',
                title: 'AtmTrnAuditorApproverObservationView',
                templateUrl: helper.basepath('ems.audit/AtmTrnAuditorApproverObservationView.html?ver=' + version + '"'),
               resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })
            //MstBDLeadRequestType
            .state('app.MstBDLeadRequestType', {
                url: '/MstBDLeadRequestType',
                title: 'MstBDLeadRequestType',
                templateUrl: helper.basepath('ems.businessteam/MstBDLeadRequestType.html?ver=' + version + '"'),
               resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })
            .state('app.MstHRLoanRaiseRequestCompleted', {
                url: '/MstHRLoanRaiseRequestCompleted',
                title: 'MstHRLoanRaiseRequestCompleted',
                templateUrl: helper.basepath('ems.hrloan/MstHRLoanRaiseRequestCompleted.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })
            .state('app.MstHRLoanRaiseRequestWithdrawn', {
                url: '/MstHRLoanRaiseRequestWithdrawn',
                title: 'MstHRLoanRaiseRequestWithdrawn',
                templateUrl: helper.basepath('ems.hrloan/MstHRLoanRaiseRequestWithdrawn.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })
            .state('app.TrnHRLoanHRVerificationsView', {
                url: '/TrnHRLoanHRVerificationsView',
                title: 'TrnHRLoanHRVerificationsView',
                templateUrl: helper.basepath('ems.hrloan/TrnHRLoanHRVerificationsView.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })

            .state('app.MstBusinessHierarchyUpdateSummary', {
                url: '/MstBusinessHierarchyUpdateSummary',
                title: 'MstBusinessHierarchyUpdateSummary',
                templateUrl: helper.basepath('ems.master/MstBusinessHierarchyUpdateSummary.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })

            .state('app.MstCreditStageSummary', {
                url: '/MstCreditStageSummary',
                title: 'MstCreditStageSummary',
                templateUrl: helper.basepath('ems.master/MstCreditStageSummary.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })
            .state('app.MstCcStageSummary', {
                url: '/MstCcStageSummary',
                title: 'MstCcStageSummary',
                templateUrl: helper.basepath('ems.master/MstCcStageSummary.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })

            .state('app.MstCadPendingStageSummary', {
                url: '/MstCadPendingStageSummary',
                title: 'MstCadPendingStageSummary',
                templateUrl: helper.basepath('ems.master/MstCadPendingStageSummary.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })

            .state('app.MstCadAcceptedStageSummary', {
                url: '/MstCadAcceptedStageSummary',
                title: 'MstCadAcceptedStageSummary',
                templateUrl: helper.basepath('ems.master/MstCadAcceptedStageSummary.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })

            .state('app.MstBusinessHierarchyUpdate', {
                url: '/MstBusinessHierarchyUpdate',
                title: 'MstBusinessHierarchyUpdate',
                templateUrl: helper.basepath('ems.master/MstBusinessHierarchyUpdate.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })

            .state('app.MstBusinessHierarchyUpdateHistory', {
                url: '/MstBusinessHierarchyUpdateHistory',
                title: 'MstBusinessHierarchyUpdateHistory',
                templateUrl: helper.basepath('ems.master/MstBusinessHierarchyUpdateHistory.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })
            .state('app.MstAnswerType', {
                url: '/MstAnswerType',
                title: 'MstAnswerType',
                templateUrl: helper.basepath('ems.master/MstAnswerType.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })
            .state('app.MstGroupTitle', {
                url: '/MstGroupTitle',
                title: 'MstGroupTitle',
                templateUrl: helper.basepath('ems.master/MstGroupTitle.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })

            .state('page.TandC', {
                url: '/TandC',
                title: 'TandC',
                templateUrl: 'app/pages/TandC.html?ver=' + version + '"'
            })

           .state('page.PrivacyPolicy', {
               url: '/PrivacyPolicy',
               title: 'PrivacyPolicy',
               templateUrl: 'app/pages/PrivacyPolicy.html?ver=' + version + '"'
           })

           .state('app.MstIncompleteStageSummary', {
               url: '/MstIncompleteStageSummary',
               title: 'MstIncompleteStageSummary',
               templateUrl: helper.basepath('ems.master/MstIncompleteStageSummary.html?ver=' + version + '"'),
               resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
           })
           
            .state('app.MstCreditMappingRule', {
                url: '/MstCreditMappingRule',
                title: 'MstCreditMappingRule',
                templateUrl: helper.basepath('ems.master/MstCreditMappingRule.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })

            .state('app.MstApplicationAssigRejectSummary', {
                url: '/MstApplicationAssigRejectSummary',
                title: 'MstApplicationAssigRejectSummary',
                templateUrl: helper.basepath('ems.master/MstApplicationAssigRejectSummary.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })
            .state('app.MstSAOnboardingBDVerificationPending', {
                url: '/MstSAOnboardingBDVerificationPending',
                title: 'MstSAOnboardingBDVerificationPending',
                templateUrl: helper.basepath('ems.master/MstSAOnboardingBDVerificationPending.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })
            .state('app.MstSAOnboardingBDVerificationIndividualPending', {
                url: '/MstSAOnboardingBDVerificationIndividualPending',
                title: 'MstSAOnboardingBDVerificationIndividualPending',
                templateUrl: helper.basepath('ems.master/MstSAOnboardingBDVerificationIndividualPending.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })
            .state('app.MstSAOnboardingSBAReport', {
                url: '/MstSAOnboardingSBAReport',
                title: 'MstSAOnboardingSBAReport',
                templateUrl: helper.basepath('ems.master/MstSAOnboardingSBAReport.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })          
            
           // MstSAOnboardingSBAReport
            .state('app.MstAppScoreCardViewdtl', {
                url: '/MstAppScoreCardViewdtl',
                title: 'MstAppScoreCardViewdtl',
                templateUrl: helper.basepath('ems.master/MstAppScoreCardViewdtl.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
            })

            .state('app.MstHRLoanRaiseRequestRejected', {
                url: '/MstHRLoanRaiseRequestRejected',
                title: 'MstHRLoanRaiseRequestRejected',
                templateUrl: helper.basepath('ems.hrloan/MstHRLoanRaiseRequestRejected.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })

          .state('app.MstHRLoanEditRequest360', {
              url: '/MstHRLoanEditRequest360',
              title: 'MstHRLoanEditRequest360',
              templateUrl: helper.basepath('ems.hrloan/MstHRLoanEditRequest360.html?ver=' + version + '"'),
              resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
          })
          .state('app.MstCcMeetingSkipSummary', {
            url: '/MstCcMeetingSkipSummary',
            title: 'MstCcMeetingSkipSummary',
            templateUrl: helper.basepath('ems.master/MstCcMeetingSkipSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstCcMeetingSkipHistory', {
            url: '/MstCcMeetingSkipHistory',
            title: 'MstCcMeetingSkipHistory',
            templateUrl: helper.basepath('ems.master/MstCcMeetingSkipHistory.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
       .state('app.MstRejectedSummary', {
        url: '/MstRejectedSummary',
        title: 'MstRejectedSummary',
        templateUrl: helper.basepath('ems.master/MstRejectedSummary.html?ver=' + version + '"'),
        resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
    })
    .state('app.MstMarketingMBDRejectedCallSummary', {
        url: '/MstMarketingMBDRejectedCallSummary',
        title: 'MstMarketingMBDRejectedCallSummary',
        templateUrl: helper.basepath('ems.businessteam/MstMarketingMBDRejectedCallSummary.html?ver=' + version + '"'),
        resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
    })
    .state('app.MstMarketingMBDRejectedCallView', {
        url: '/MstMarketingMBDRejectedCallView',
        title: 'MstMarketingMBDRejectedCallView',
        templateUrl: helper.basepath('ems.businessteam/MstMarketingMBDRejectedCallView.html?ver=' + version + '"'),
        resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
    })
    .state('app.MstMarketingManageRejectedCallView', {
        url: '/MstMarketingManageRejectedCallView',
        title: 'MstMarketingManageRejectedCallView',
        templateUrl: helper.basepath('ems.businessteam/MstMarketingManageRejectedCallView.html?ver=' + version + '"'),
        resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
    })

      .state('app.MstSAOnboardingBussDevtVerificationIndividualPendingBDSummary', {
          url: '/MstSAOnboardingBussDevtVerificationIndividualPendingBDSummary',
          title: 'MstSAOnboardingBussDevtVerificationIndividualPendingBDSummary',
          templateUrl: helper.basepath('ems.master/MstSAOnboardingBussDevtVerificationIndividualPendingBDSummary.html?ver=' + version + '"'),
          resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
      })
      .state('app.MstCreditGuaranteeDetailAdd', {
        url: '/MstCreditGuaranteeDetailAdd',
        title: 'MstCreditGuaranteeDetailAdd',
        templateUrl: helper.basepath('ems.master/MstCreditGuaranteeDetailAdd.html?ver=' + version + '"'),
        resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
    })
     .state('app.MstCreditIndividualGuaranteeDtlAdd', {
         url: '/MstCreditIndividualGuaranteeDtlAdd',
         title: 'MstCreditIndividualGuaranteeDtlAdd',
         templateUrl: helper.basepath('ems.master/MstCreditIndividualGuaranteeDtlAdd.html?ver=' + version + '"'),
         resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
     })
      .state('app.MstCreditGroupGuaranteeDtlAdd', {
          url: '/MstCreditGroupGuaranteeDtlAdd',
          title: 'MstCreditGroupGuaranteeDtlAdd',
          templateUrl: helper.basepath('ems.master/MstCreditGroupGuaranteeDtlAdd.html?ver=' + version + '"'),
          resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
      })
  .state('app.MstUserProfile', {
      url: '/MstUserProfile',
      title: 'MstUserProfile',
      templateUrl: helper.basepath('ems.master/MstUserProfile.html?ver=' + version + '"'),
      resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
  })

      .state('app.MstSAOnboardingBussDevtVerificationInstitutionPendingBDSummary', {
          url: '/MstSAOnboardingBussDevtVerificationInstitutionPendingBDSummary',
          title: 'MstSAOnboardingBussDevtVerificationInstitutionPendingBDSummary',
          templateUrl: helper.basepath('ems.master/MstSAOnboardingBussDevtVerificationInstitutionPendingBDSummary.html?ver=' + version + '"'),
          resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
      })

        .state('app.SysMstHRDocument', {
            url: '/SysMstHRDocument',
            title: 'SysMstHRDocument',
            templateUrl: helper.basepath('ems.system/SysMstHRDocument.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.SysMstEmployeeHRDocument', {
            url: '/SysMstEmployeeHRDocument',
            title: 'SysMstEmployeeHRDocument',
            templateUrl: helper.basepath('ems.system/SysMstEmployeeHRDocument.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
        .state('app.AtmTrnMyAuditeeTaskCheckerAuditeeView', {
            url: '/AtmTrnMyAuditeeTaskCheckerAuditeeView',
             title: 'AtmTrnMyAuditeeTaskCheckerAuditeeView',
             templateUrl: helper.basepath('ems.audit/AtmTrnMyAuditeeTaskCheckerAuditeeView.html?ver=' + version + '"'),
             resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstCsaCategory', {
            url: '/MstCsaCategory',
            title: 'MstCsaCategory',
            templateUrl: helper.basepath('ems.master/MstCsaCategory.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
         .state('app.MstCsaClassification', {
             url: '/MstCsaClassification',
             title: 'MstCsaClassification',
             templateUrl: helper.basepath('ems.master/MstCsaClassification.html?ver=' + version + '"'),
             resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
         })
        .state('app.MstGuaranteePrograms', {
            url: '/MstGuaranteePrograms',
            title: 'MstGuaranteePrograms',
            templateUrl: helper.basepath('ems.master/MstGuaranteePrograms.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstColendingCategory', {
            url: '/MstColendingCategory',
            title: 'MstColendingCategory',
            templateUrl: helper.basepath('ems.master/MstColendingCategory.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
            .state('app.MstColendingPrograms', {
                url: '/MstColendingPrograms',
                title: 'MstColendingPrograms',
                templateUrl: helper.basepath('ems.master/MstColendingPrograms.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })
            .state('app.MstColendingProgramAdd', {
                url: '/MstColendingProgramAdd',
                title: 'MstColendingProgramAdd',
                templateUrl: helper.basepath('ems.master/MstColendingProgramAdd.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })
            .state('app.MstColendingProgramEdit', {
                url: '/MstColendingProgramEdit',
                title: 'MstColendingProgramEdit',
                templateUrl: helper.basepath('ems.master/MstColendingProgramEdit.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })
            .state('app.MstColendingProgramView', {
                url: '/MstColendingProgramView',
                title: 'MstColendingProgramView',
                templateUrl: helper.basepath('ems.master/MstColendingProgramView.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })
            .state('app.MstMarketingMyleadsClosedCall', {
                url: '/MstMarketingMyleadsClosedCall',
                title: 'MstMarketingMyleadsClosedCall',
                templateUrl: helper.basepath('ems.businessteam/MstMarketingMyleadsClosedCall.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })
        // Horizontal layout

        .state('app.AtmTrnInitiateAuditHold', {
            url: '/AtmTrnInitiateAuditHold',
             title: 'AtmTrnInitiateAuditHold',
             templateUrl: helper.basepath('ems.audit/AtmTrnInitiateAuditHold.html?ver=' + version + '"'),
             resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.AtmTrnInitiateAuditClosed', {
            url: '/AtmTrnInitiateAuditClosed',
             title: 'AtmTrnInitiateAuditClosed',
             templateUrl: helper.basepath('ems.audit/AtmTrnInitiateAuditClosed.html?ver=' + version + '"'),
             resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.AtmTrnInitiateAuditCompleted', {
            url: '/AtmTrnInitiateAuditCompleted',
             title: 'AtmTrnInitiateAuditCompleted',
             templateUrl: helper.basepath('ems.audit/AtmTrnInitiateAuditCompleted.html?ver=' + version + '"'),
             resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.AtmTrnInitiateAuditRejected', {
            url: '/AtmTrnInitiateAuditRejected',
             title: 'AtmTrnInitiateAuditRejected',
             templateUrl: helper.basepath('ems.audit/AtmTrnInitiateAuditRejected.html?ver=' + version + '"'),
             resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.AtmTrnInitiateAuditApproved', {
            url: '/AtmTrnInitiateAuditApproved',
             title: 'AtmTrnInitiateAuditApproved',
             templateUrl: helper.basepath('ems.audit/AtmTrnInitiateAuditApproved.html?ver=' + version + '"'),
             resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstVerticalRule', {
            url: '/MstVerticalRule',
            title: 'MstVerticalRule',
            templateUrl: helper.basepath('ems.master/MstVerticalRule.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstCreditColendingDetailAdd', {
            url: '/MstCreditColendingDetailAdd',
            title: 'MstCreditColendingDetailAdd',
            templateUrl: helper.basepath('ems.master/MstCreditColendingDetailAdd.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstCreditIndividualColendingDtlAdd', {
            url: '/MstCreditIndividualColendingDtlAdd',
            title: 'MstCreditIndividualColendingDtlAdd',
            templateUrl: helper.basepath('ems.master/MstCreditIndividualColendingDtlAdd.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstCreditGroupColendingDtlAdd', {
            url: '/MstCreditGroupColendingDtlAdd',
            title: 'MstCreditGroupColendingDtlAdd',
            templateUrl: helper.basepath('ems.master/MstCreditGroupColendingDtlAdd.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstColendingVerificationSummary', {
            url: '/MstColendingVerificationSummary',
            title: 'MstColendingVerificationSummary',
            templateUrl: helper.basepath('ems.master/MstColendingVerificationSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstColendingCreditVerification', {
            url: '/MstColendingCreditVerification',
            title: 'MstColendingCreditVerification',
            templateUrl: helper.basepath('ems.master/MstColendingCreditVerification.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstCreditCompletedVerification', {
            url: '/MstCreditCompletedVerification',
            title: 'MstCreditCompletedVerification',
            templateUrl: helper.basepath('ems.master/MstCreditCompletedVerification.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstColendingVerifyCompletedSummary', {
            url: '/MstColendingVerifyCompletedSummary',
            title: 'MstColendingVerifyCompletedSummary',
            templateUrl: helper.basepath('ems.master/MstColendingVerifyCompletedSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstColendingCCApprovedVerification', {
            url: '/MstColendingCCApprovedVerification',
            title: 'MstColendingCCApprovedVerification',
            templateUrl: helper.basepath('ems.master/MstColendingCCApprovedVerification.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstColendingCCApprovedVerifySummary', {
            url: '/MstColendingCCApprovedVerifySummary',
            title: 'MstColendingCCApprovedVerifySummary',
            templateUrl: helper.basepath('ems.master/MstColendingCCApprovedVerifySummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstCCFinalApprovedVerifiedSummary', {
            url: '/MstCCFinalApprovedVerifiedSummary',
            title: 'MstCCFinalApprovedVerifiedSummary',
            templateUrl: helper.basepath('ems.master/MstCCFinalApprovedVerifiedSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstCCFinalApprovedVerifiedView', {
            url: '/MstCCFinalApprovedVerifiedView',
            title: 'MstCCFinalApprovedVerifiedView',
            templateUrl: helper.basepath('ems.master/MstCCFinalApprovedVerifiedView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstSAOnboardingPendingwithRMSummary', {
            url: '/MstSAOnboardingPendingwithRMSummary',
            title: 'MstSAOnboardingPendingwithRMSummary',
            templateUrl: helper.basepath('ems.master/MstSAOnboardingPendingwithRMSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstSAOnboardingPendingwithCADSummary', {
            url: '/MstSAOnboardingPendingwithCADSummary',
            title: 'MstSAOnboardingPendingwithCADSummary',
            templateUrl: helper.basepath('ems.master/MstSAOnboardingPendingwithCADSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstSAOnboardingPendingwithRMIndividualSummary', {
            url: '/MstSAOnboardingPendingwithRMIndividualSummary',
            title: 'MstSAOnboardingPendingwithRMIndividualSummary',
            templateUrl: helper.basepath('ems.master/MstSAOnboardingPendingwithRMIndividualSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstSAOnboardingPendingwithCADIndividualSummary', {
            url: '/MstSAOnboardingPendingwithCADIndividualSummary',
            title: 'MstSAOnboardingPendingwithCADIndividualSummary',
            templateUrl: helper.basepath('ems.master/MstSAOnboardingPendingwithCADIndividualSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstSAOnboardingIndividualCodeCreateView', {
            url: '/MstSAOnboardingIndividualCodeCreateView',
            title: 'MstSAOnboardingIndividualCodeCreateView',
            templateUrl: helper.basepath('ems.master/MstSAOnboardingIndividualCodeCreateView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstSAOnboardingInstitutionCodeCreateView', {
            url: '/MstSAOnboardingInstitutionCodeCreateView',
            title: 'MstSAOnboardingInstitutionCodeCreateView',
            templateUrl: helper.basepath('ems.master/MstSAOnboardingInstitutionCodeCreateView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        
        .state('app.MstSanctionMISReportChecker', {
            url: '/MstSanctionMISReportChecker',
            title: 'MstSanctionMISReportChecker',
            templateUrl: helper.basepath('ems.master/MstSanctionMISReportChecker.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.MstSanctionMISReportApprover', {
            url: '/MstSanctionMISReportApprover',
            title: 'MstSanctionMISReportApprover',
            templateUrl: helper.basepath('ems.master/MstSanctionMISReportApprover.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.MstSanctionMISReportApproved', {
            url: '/MstSanctionMISReportApproved',
            title: 'MstSanctionMISReportApproved',
            templateUrl: helper.basepath('ems.master/MstSanctionMISReportApproved.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

	    .state('app.Msthypothecationreport', {
            url: '/Msthypothecationreport',
            title: 'Msthypothecationreport',
            templateUrl: helper.basepath('ems.master/Msthypothecationreport.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.MstSanctionMISReportMaker', {
            url: '/MstSanctionMISReportMaker',
            title: 'MstSanctionMISReportMaker',
            templateUrl: helper.basepath('ems.master/MstSanctionMISReportMaker.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

            .state('app.MstDocumentChecklistReport', {
                url: '/MstDocumentChecklistReport',
                title: 'MstDocumentChecklistReport',
                templateUrl: helper.basepath('ems.master/MstDocumentChecklistReport.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })

            .state('app.MstDocumentChecklistCheckerReport', {
                url: '/MstDocumentChecklistCheckerReport',
                title: 'MstDocumentChecklistCheckerReport',
                templateUrl: helper.basepath('ems.master/MstDocumentChecklistCheckerReport.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })

            .state('app.MstDocumentChecklistApprovalReport', {
                url: '/MstDocumentChecklistApprovalReport',
                title: 'MstDocumentChecklistApprovalReport',
                templateUrl: helper.basepath('ems.master/MstDocumentChecklistApprovalReport.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })

            .state('app.MstColendingRule', {
                url: '/MstColendingRule',
                title: 'MstColendingRule',
                templateUrl: helper.basepath('ems.master/MstColendingRule.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })
             .state('app.MstCADGuaranteeDetailAdd', {
            url: '/MstCADGuaranteeDetailAdd',
            title: 'MstCADGuaranteeDetailAdd',
            templateUrl: helper.basepath('ems.master/MstCADGuaranteeDetailAdd.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstCADIndividualGuaranteeDtlAdd', {
            url: '/MstCADIndividualGuaranteeDtlAdd',
            title: 'MstCADIndividualGuaranteeDtlAdd',
            templateUrl: helper.basepath('ems.master/MstCADIndividualGuaranteeDtlAdd.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstCADGroupGuaranteeDtlAdd', {
            url: '/MstCADGroupGuaranteeDtlAdd',
            title: 'MstCADGroupGuaranteeDtlAdd',
            templateUrl: helper.basepath('ems.master/MstCADGroupGuaranteeDtlAdd.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstCADAcceptColendingAdd', {
            url: '/MstCADAcceptColendingAdd',
            title: 'MstCADAcceptColendingAdd',
            templateUrl: helper.basepath('ems.master/MstCADAcceptColendingAdd.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.MstMarketingAssignedFollowupLeadsView', {
            url: '/MstMarketingAssignedFollowupLeadsView',
            title: 'MstMarketingAssignedFollowupLeadsView',
            templateUrl: helper.basepath('ems.businessteam/MstMarketingAssignedFollowupLeadsView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstSoftcopyVettingFollowupReport', {
            url: '/MstSoftcopyVettingFollowupReport',
            title: 'MstSoftcopyVettingFollowupReport',
            templateUrl: helper.basepath('ems.master/MstSoftcopyVettingFollowupReport.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
       .state('app.MstCadPhysicalDocFollowupStatus', {
            url: '/MstCadPhysicalDocFollowupStatus',
            title: 'MstCadPhysicalDocFollowupStatus',
            templateUrl: helper.basepath('ems.master/MstCadPhysicalDocFollowupStatus.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })  
        
	    .state('app.SysRptEsign', {
            url: '/SysRptEsign',
            title: 'SysRptEsign',
            templateUrl: helper.basepath('ems.system/SysRptEsign.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

       .state('app.SysRptESignSignedDoc', {
            url: '/SysRptESignSignedDoc',
            title: 'SysRptESignSignedDoc',
            templateUrl: helper.basepath('ems.system/SysRptESignSignedDoc.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

            .state('app.MstRptCadDeferral', {
                url: '/MstRptCadDeferral',
                title: 'MstRptCadDeferral',
                templateUrl: helper.basepath('ems.master/MstRptCadDeferral.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
            })

            .state('app.MstRptCadApplicationDeferralCovenantView', {
                url: '/MstRptCadApplicationDeferralCovenantView',
                title: 'MstRptCadApplicationDeferralCovenantView',
                templateUrl: helper.basepath('ems.master/MstRptCadApplicationDeferralCovenantView.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
            })

            .state('app.MstRptCadScannedDeferralCovenantDtls', {
                url: '/MstRptCadScannedDeferralCovenantDtls',
                title: 'MstRptCadScannedDeferralCovenantDtls',
                templateUrl: helper.basepath('ems.master/MstRptCadScannedDeferralCovenantDtls.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
            })

            .state('app.MstRptCadScannedDocchecklist', {
                url: '/MstRptCadScannedDocchecklist',
                title: 'MstRptCadScannedDocchecklist',
                templateUrl: helper.basepath('ems.master/MstRptCadScannedDocchecklist.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
            })

            .state('app.MstRptCadScannedQuery', {
                url: '/MstRptCadScannedQuery',
                title: 'MstRptCadScannedQuery',
                templateUrl: helper.basepath('ems.master/MstRptCadScannedQuery.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
            })

            .state('app.MstRptCadScannedDeferralHistory', {
                url: '/MstRptCadScannedDeferralHistory',
                title: 'MstRptCadScannedDeferralHistory',
                templateUrl: helper.basepath('ems.master/MstRptCadScannedDeferralHistory.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
            })



            .state('app.MstRptCadScannedDeferralStatus', {
                url: '/MstRptCadScannedDeferralStatus',
                title: 'MstRptCadScannedDeferralStatus',
                templateUrl: helper.basepath('ems.master/MstRptCadScannedDeferralStatus.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
            })

            .state('app.MstRptCadPhysicalDeferralCovenantDtls', {
                url: '/MstRptCadPhysicalDeferralCovenantDtls',
                title: 'MstRptCadPhysicalDeferralCovenantDtls',
                templateUrl: helper.basepath('ems.master/MstRptCadPhysicalDeferralCovenantDtls.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
            })

            .state('app.MstRptCadPhysicalDocchecklist', {
                url: '/MstRptCadPhysicalDocchecklist',
                title: 'MstRptCadPhysicalDocchecklist',
                templateUrl: helper.basepath('ems.master/MstRptCadPhysicalDocchecklist.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
            })


            .state('app.MstRptCadPhysicalDocQuery', {
                url: '/MstRptCadPhysicalDocQuery',
                title: 'MstRptCadPhysicalDocQuery',
                templateUrl: helper.basepath('ems.master/MstRptCadPhysicalDocQuery.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
            })

            .state('app.MstRptCadPhysicalDocStatus', {
                url: '/MstRptCadPhysicalDocStatus',
                title: 'MstRptCadPhysicalDocStatus',
                templateUrl: helper.basepath('ems.master/MstRptCadPhysicalDocStatus.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
            })

            .state('app.MstRptCadPhysicalDeferralHistory', {
                url: '/MstRptCadPhysicalDeferralHistory',
                title: 'MstRptCadPhysicalDeferralHistory',
                templateUrl: helper.basepath('ems.master/MstRptCadPhysicalDeferralHistory.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
            })

            .state('app.MstRptCadCovenant', {
                url: '/MstRptCadCovenant',
                title: 'MstRptCadCovenant',
                templateUrl: helper.basepath('ems.master/MstRptCadCovenant.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })

            .state('app.MstCCApprovedSummary', {
                url: '/MstCCApprovedSummary',
                title: 'MstCCApprovedSummary',
                templateUrl: helper.basepath('ems.master/MstCCApprovedSummary.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })

            .state('app.MstCreditColendingRule', {
                url: '/MstCreditColendingRule',
                title: 'MstCreditColendingRule',
                templateUrl: helper.basepath('ems.master/MstCreditColendingRule.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })

            .state('app.MstVerticalBRETrnRule', {
                url: '/MstVerticalBRETrnRule',
                title: 'MstVerticalBRETrnRule',
                templateUrl: helper.basepath('ems.master/MstVerticalBRETrnRule.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })

            .state('app.MstMarketingUnassignedLeadSummary', {
                url: '/MstMarketingUnassignedLeadSummary',
                title: 'MstMarketingUnassignedLeadSummary',
                templateUrl: helper.basepath('ems.businessteam/MstMarketingUnassignedLeadSummary.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
             })
            .state('app.MstMarketingUnassignedLeadEdit', {
                 url: '/MstMarketingUnassignedLeadEdit',
                 title: 'MstMarketingUnassignedLeadEdit',
                templateUrl: helper.basepath('ems.businessteam/MstMarketingUnassignedLeadEdit.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
             })
             .state('app.MstSAOnboardingIndividualVerificationEdit', {
                url: '/MstSAOnboardingIndividualVerificationEdit',
                title: 'MstSAOnboardingIndividualVerificationEdit',
                templateUrl: helper.basepath('ems.master/MstSAOnboardingIndividualVerificationEdit.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })
            .state('app.MstSAOnboardingInstitutionVerificationEdit', {
                url: '/MstSAOnboardingInstitutionVerificationEdit',
                title: 'MstSAOnboardingInstitutionVerificationEdit',
                templateUrl: helper.basepath('ems.master/MstSAOnboardingInstitutionVerificationEdit.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            }) 
           
            .state('app.AtmRptSamfinCustomerVisitSummary', {
                 url: '/AtmRptSamfinCustomerVisitSummary',
                 title: 'AtmRptSamfinCustomerVisitSummary',
                 templateUrl: helper.basepath('ems.audit/AtmRptSamfinCustomerVisitSummary.html?ver=' + version + '"'),
                 resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
             })
            .state('app.AtmRptCadUrnAcceptedCustomerDtls', {
                 url: '/AtmRptCadUrnAcceptedCustomerDtls',
                 title: 'AtmRptCadUrnAcceptedCustomerDtls',
                 templateUrl: helper.basepath('ems.audit/AtmRptCadUrnAcceptedCustomerDtls.html?ver=' + version + '"'),
                 resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
             })
            .state('app.AtmRptApplicationView', {
                 url: '/AtmRptApplicationView',
                 title: 'AtmRptApplicationView',
                 templateUrl: helper.basepath('ems.audit/AtmRptApplicationView.html?ver=' + version + '"'),
                 resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
             })
            .state('app.MstVerticalApplicantTypeRule', {
                url: '/MstVerticalApplicantTypeRule',
                title: 'MstVerticalApplicantTypeRule',
                templateUrl: helper.basepath('ems.master/MstVerticalApplicantTypeRule.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })
            .state('app.MstColendingRuleadd', {
                url: '/MstColendingRuleadd',
                title: 'MstColendingRuleadd',
                templateUrl: helper.basepath('ems.master/MstColendingRuleadd.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })
            .state('app.MstLimitManagementView', {
                url: '/MstLimitManagementView',
                title: 'MstLimitManagementView',
                templateUrl: helper.basepath('ems.master/MstLimitManagementView.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })
            .state('app.SysRptESignExpiredDoc', {
                url: '/SysRptESignExpiredDoc',
                title: 'SysRptESignExpiredDoc',
                templateUrl: helper.basepath('ems.system/SysRptESignExpiredDoc.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })
            .state('app.brsTrnimport', {
                url: '/brsTrnimport',    
                title: 'brsTrnimport',    
                templateUrl: helper.basepath('ems.brs/brsTrnimport.html?ver=' + version + '"'),    
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')    
            })

            .state('app.MstCadUrnAcceptedCustomersLegaltag', {
                url: '/MstCadUrnAcceptedCustomersLegaltag',
                title: 'MstCadUrnAcceptedCustomersLegaltag',
                templateUrl: helper.basepath('ems.master/MstCadUrnAcceptedCustomersLegaltag.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })

            .state('app.MstCadUrnAcceptedCustomersNPAtag', {
                url: '/MstCadUrnAcceptedCustomersNPAtag',
                title: 'MstCadUrnAcceptedCustomersNPAtag',
                templateUrl: helper.basepath('ems.master/MstCadUrnAcceptedCustomersNPAtag.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })

            .state('app.MstCadDocumentChecklistView', {
                url: '/MstCadDocumentChecklistView',
                title: 'MstCadDocumentChecklistView',
                templateUrl: helper.basepath('ems.master/MstCadDocumentChecklistView.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })

            .state('app.MstSBAInstitutionFinalApproval', {
                url: '/MstSBAInstitutionFinalApproval',
                title: 'MstSBAInstitutionFinalApproval',
                templateUrl: helper.basepath('ems.master/MstSBAInstitutionFinalApproval.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })

            .state('app.MstSBAIndividualFinalApproval', {
                url: '/MstSBAIndividualFinalApproval',
                title: 'MstSBAIndividualFinalApproval',
                templateUrl: helper.basepath('ems.master/MstSBAIndividualFinalApproval.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })

            .state('app.MstSBAIndividualFinalApprovalView', {
                url: '/MstSBAIndividualFinalApprovalView',
                title: 'MstSBAIndividualFinalApprovalView',
                templateUrl: helper.basepath('ems.master/MstSBAIndividualFinalApprovalView.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })
            .state('app.SysprtAgriFinanceSnapshot', {
                url: '/SysprtAgriFinanceSnapshot',
                title: 'SysprtAgriFinanceSnapshot',
                templateUrl: helper.basepath('ems.system/SysprtAgriFinanceSnapshot.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })
            .state('app.brsTrnbankconfiguration', {
                url: '/brsTrnbankconfiguration',
                title: 'brsTrnbankconfiguration',
                templateUrl: helper.basepath('ems.brs/brsTrnbankconfiguration.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })
            .state('app.brsTrnbankconfigurationadd', {
                url: '/brsTrnbankconfigurationadd',
                title: 'brsTrnbankconfigurationadd',
                templateUrl: helper.basepath('ems.brs/brsTrnbankconfigurationadd.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })

            .state('app.brsTrnbankconfigurationedit', {
                url: '/brsTrnbankconfigurationedit',
                title: 'brsTrnbankconfigurationedit',
                templateUrl: helper.basepath('ems.brs/brsTrnbankconfigurationedit.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })
            .state('app.SysprtAgriFinanceSummary', {
                url: '/SysprtAgriFinanceSummary',
                title: 'SysprtAgriFinanceSummary',
                templateUrl: helper.basepath('ems.system/SysprtAgriFinanceSummary.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })     
            
        .state('app.SysprtPipelinePlanner', {
                url: '/SysprtPipelinePlanner',
                title: 'SysprtPipelinePlanner',
                templateUrl: helper.basepath('ems.system/SysprtPipelinePlanner.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })
            .state('app.MstSanctionAccepted', {
                url: '/MstSanctionAccepted',
                title: 'MstSanctionAccepted',
                templateUrl: helper.basepath('ems.master/MstSanctionAccepted.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })
            .state('app.MstSanctionHistory', {
                url: '/MstSanctionHistory',
                title: 'MstSanctionHistory',
                templateUrl: helper.basepath('ems.master/MstSanctionHistory.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })
            .state('app.MstSanctionAcceptedView', {
                url: '/MstSanctionAcceptedView',
                title: 'MstSanctionAcceptedView',
                templateUrl: helper.basepath('ems.master/MstSanctionAcceptedView.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })
            .state('app.MstCreditColendingRuleView', {
                url: '/MstCreditColendingRuleView',
                title: 'MstCreditColendingRuleView',
                templateUrl: helper.basepath('ems.master/MstCreditColendingRuleView.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
            })
            .state('app.SysprtAgriFinancePortfolioQuality', {
                url: '/SysprtAgriFinancePortfolioQuality',
                title: 'SysprtAgriFinancePortfolioQuality',
                templateUrl: helper.basepath('ems.system/SysprtAgriFinancePortfolioQuality.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })

            .state('app.MstTemplateSummary', {
                url: '/MstTemplateSummary',
                title: 'MstTemplateSummary',
                templateUrl: helper.basepath('ems.master/MstTemplateSummary.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'textAngular', 'ngWig')
            })

            .state('app.MstAddTemplate', {
                url: '/MstAddTemplate',
                title: 'MstAddTemplate',
                templateUrl: helper.basepath('ems.master/MstAddTemplate.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'textAngular', 'ngWig')
            })  
       
            .state('app.MstEditTemplate', {
                url: '/MstEditTemplate',
                title: 'MstEditTemplate',
                templateUrl: helper.basepath('ems.master/MstEditTemplate.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'textAngular', 'ngWig')
            })

            .state('app.MstCourierMgmtsummary', {
                url: '/MstCourierMgmtsummary',
                title: 'MstCourierMgmtsummary',
                templateUrl: helper.basepath('ems.master/MstCourierMgmtsummary.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })
            .state('app.MstCourierCreation', {
                url: '/MstCourierCreation',
                title: 'MstCourierCreation',
                templateUrl: helper.basepath('ems.master/MstCourierCreation.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })
            .state('app.MstCourierMgmtAckList', {
                url: '/MstCourierMgmtAckList',
                title: 'MstCourierMgmtAckList',
                templateUrl: helper.basepath('ems.master/MstCourierMgmtAckList.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })
            .state('app.MstCourierEdit', {
                url: '/MstCourierEdit',
                title: 'MstCourierEdit',
                templateUrl: helper.basepath('ems.master/MstCourierEdit.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })
            .state('app.MstCourierView', {
                url: '/MstCourierView',
                title: 'MstCourierView',
                templateUrl: helper.basepath('ems.master/MstCourierView.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })
            .state('page.MstCourierMgmtAckForm', {
                url: '/MstCourierMgmtAckForm',
                title: 'MstCourierMgmtAckForm',
                templateUrl: 'app/pages/MstCourierMgmtAckForm.html?ver=' + version + '"'
            })
            .state('app.MstCourierCompanysummary', {
                url: '/MstCourierCompanysummary',
                title: 'MstCourierCompanysummary',
                templateUrl: helper.basepath('ems.master/MstCourierCompanysummary.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })
            .state('app.AtmRptAuditVisitReportSummary', {
                url: '/AtmRptAuditVisitReportSummary',
                title: 'AtmRptAuditVisitReportSummary',
                templateUrl: helper.basepath('ems.audit/AtmRptAuditVisitReportSummary.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })
             .state('app.AtmRptAuditVisitReportApprovalSummary', {
                url: '/AtmRptAuditVisitReportApprovalSummary',
                title: 'AtmRptAuditVisitReportApprovalSummary',
                templateUrl: helper.basepath('ems.audit/AtmRptAuditVisitReportApprovalSummary.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })
            .state('app.AtmRptAuditVisitReportApprovalApprovedSummary', {
                url: '/AtmRptAuditVisitReportApprovalApprovedSummary',
                title: 'AtmRptAuditVisitReportApprovalApprovedSummary',
                templateUrl: helper.basepath('ems.audit/AtmRptAuditVisitReportApprovalApprovedSummary.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })
            .state('app.AtmRptAuditVisitReportApproval', {
                url: '/AtmRptAuditVisitReportApproval',
                title: 'AtmRptAuditVisitReportApproval',
                templateUrl: helper.basepath('ems.audit/AtmRptAuditVisitReportApproval.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })
            .state('app.AtmRptAuditVisitReportApprovedSummary', {
                url: '/AtmRptAuditVisitReportApprovedSummary',
                title: 'AtmRptAuditVisitReportApprovedSummary',
                templateUrl: helper.basepath('ems.audit/AtmRptAuditVisitReportApprovedSummary.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })
            .state('app.AtmRptAuditVisitReportApprovedView', {
                url: '/AtmRptAuditVisitReportApprovedView',
                title: 'AtmRptAuditVisitReportApprovedView',
                templateUrl: helper.basepath('ems.audit/AtmRptAuditVisitReportApprovedView.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })
            .state('app.AtmRptAuditVisitReportApprovalPendingView', {
                url: '/AtmRptAuditVisitReportApprovalPendingView',
                title: 'AtmRptAuditVisitReportApprovalPendingView',
                templateUrl: helper.basepath('ems.audit/AtmRptAuditVisitReportApprovalPendingView.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })
            .state('app.AtmRptAuditVisitReportApprovalApprovedView', {
                url: '/AtmRptAuditVisitReportApprovalApprovedView',
                title: 'AtmRptAuditVisitReportApprovalApprovedView',
                templateUrl: helper.basepath('ems.audit/AtmRptAuditVisitReportApprovalApprovedView.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })
            .state('app.AtmRptAuditVisitReportEdit', {
                url: '/AtmRptAuditVisitReportEdit',
                title: 'AtmRptAuditVisitReportEdit',
                templateUrl: helper.basepath('ems.audit/AtmRptAuditVisitReportEdit.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })
            .state('app.AtmRptAuditVisitReportView', {
                url: '/AtmRptAuditVisitReportView',
                title: 'AtmRptAuditVisitReportView',
                templateUrl: helper.basepath('ems.audit/AtmRptAuditVisitReportView.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })
           .state('app.AtmRptAuditVisitReport', {
                url: '/AtmRptAuditVisitReport',
                title: 'AtmRptAuditVisitReport',
                templateUrl: helper.basepath('ems.audit/AtmRptAuditVisitReport.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })
            .state('app.osdTrnRHApprovalView', {
                url: '/osdTrnRHApprovalView',
                title: 'osdTrnRHApprovalView',
                templateUrl: helper.basepath('ems.osd/osdTrnRHApprovalView.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'textAngular', 'ngWig')
            })

            .state('app.osdTrnRHApprovalViewHistory', {
                url: '/osdTrnRHApprovalViewHistory',
                title: 'osdTrnRHApprovalViewHistory',
                templateUrl: helper.basepath('ems.osd/osdTrnRHApprovalViewHistory.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'textAngular', 'ngWig')
            })
            
.state('app.brsTrnReconcillation', {
                url: '/brsTrnReconcillation',
                title: 'brsTrnReconcillation',
                templateUrl: helper.basepath('ems.brs/brsTrnReconcillation.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })

            .state('app.brsTrnUnReconcillation', {
                url: '/brsTrnUnReconcillation',
                title: 'brsTrnUnReconcillation',
                templateUrl: helper.basepath('ems.brs/brsTrnUnReconcillation.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })
            .state('app.brsTrnRepaymentImport', {
                url: '/brsTrnRepaymentImport',
                title: 'brsTrnRepaymentImport',
                templateUrl: helper.basepath('ems.brs/brsTrnRepaymentImport.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })
            .state('app.brsTrnUnReconcillationManagement', {
                url: '/brsTrnUnReconcillationManagement',
                title: 'brsTrnUnReconcillationManagement',
                templateUrl: helper.basepath('ems.brs/brsTrnUnReconcillationPendingManagement.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })

            .state('app.brsTrnUnReconcillationCompletedManagement', {
                url: '/brsTrnUnReconcillationCompletedManagement',
                title: 'brsTrnUnReconcillationCompletedManagement',
                templateUrl: helper.basepath('ems.brs/brsTrnUnReconcillationCompletedManagement.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })
            .state('app.brsTrnUnreconcillationTag', {
                url: '/brsTrnUnreconcillationTag',
                title: 'brsTrnUnreconcillationTag',
                templateUrl: helper.basepath('ems.brs/brsTrnUnreconcillationTag.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })
            .state('app.osdTrnUnreconciliationRepaymentView', {
                url: '/osdTrnUnreconciliationRepaymentView',
                title: 'osdTrnUnreconciliationRepaymentView',
                templateUrl: helper.basepath('ems.osd/osdTrnUnreconciliationTransactionView.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })
            
            .state('app.AtmRptAuditVisitReportManagementPendingSummary', {                
                url: '/AtmRptAuditVisitReportManagementPendingSummary',                
                title: 'AtmRptAuditVisitReportManagementPendingSummary',                
                templateUrl: helper.basepath('ems.audit/AtmRptAuditVisitReportManagementPendingSummary.html?ver=' + version + '"'),                
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')            
            })
            .state('app.AtmRptAuditVisitReportManagementApprovedSummary', {                
                url: '/AtmRptAuditVisitReportManagementApprovedSummary',                
                title: 'AtmRptAuditVisitReportManagementApprovedSummary',                
                templateUrl: helper.basepath('ems.audit/AtmRptAuditVisitReportManagementApprovedSummary.html?ver=' + version + '"'),                
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')            
            })
            .state('app.AtmRptAuditVisitReportManagementApprovedView', {                
                url: '/AtmRptAuditVisitReportManagementApprovedView',                
                title: 'AtmRptAuditVisitReportManagementApprovedView',                
                templateUrl: helper.basepath('ems.audit/AtmRptAuditVisitReportManagementApprovedView.html?ver=' + version + '"'),                
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')            
            })
             .state('app.AtmRptAuditVisitReportManagementPendingView', {                
                url: '/AtmRptAuditVisitReportManagementPendingView',                
                title: 'AtmRptAuditVisitReportManagementPendingView',                
                templateUrl: helper.basepath('ems.audit/AtmRptAuditVisitReportManagementPendingView.html?ver=' + version + '"'),                
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')            
            })
            .state('app.brsTrnUnreconTagViewAssignedHistory', {
                url: '/brsTrnUnreconTagViewAssignedHistory',
                title: 'brsTrnUnreconTagViewAssignedHistory',
                templateUrl: helper.basepath('ems.brs/brsTrnUnreconTagViewAssignedHistory.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })
            .state('app.brsMstTemplateSummary', {
                url: '/brsMstTemplateSummary',
                title: 'brsMstTemplateSummary',
                templateUrl: helper.basepath('ems.brs/brsMstTemplateSummary.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })
            .state('app.brsMstRepaymentTemplate', {
                url: '/brsMstRepaymentTemplate',
                title: 'brsMstRepaymentTemplate',
                templateUrl: helper.basepath('ems.brs/brsMstRepaymentTemplate.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })
            .state('app.MstSanctionTemplateSummary', {
                url: '/MstSanctionTemplateSummary',
                title: 'MstSanctionTemplateSummary',
                templateUrl: helper.basepath('ems.master/MstSanctionTemplateSummary.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'textAngular', 'ngWig')
            })
            .state('app.MstSanctionAddTemplate', {
                url: '/MstSanctionAddTemplate',
                title: 'MstSanctionAddTemplate',
                templateUrl: helper.basepath('ems.master/MstSanctionAddTemplate.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'textAngular', 'ngWig')
            })
            .state('app.MstSanctionEditTemplate', {
                url: '/MstSanctionEditTemplate',
                title: 'MstSanctionEditTemplate',
                templateUrl: helper.basepath('ems.master/MstSanctionEditTemplate.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'textAngular', 'ngWig')
            })
            .state('app.brsTrnRepaymentReconcillation', {
                url: '/brsTrnRepaymentReconcillation',
                title: 'brsTrnRepaymentReconcillation',
                templateUrl: helper.basepath('ems.brs/brsTrnRepaymentReconcillation.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })
            .state('app.brsTrnRepaymentUnReconcillation', {
                url: '/brsTrnRepaymentUnReconcillation',
                title: 'brsTrnRepaymentUnReconcillation',
                templateUrl: helper.basepath('ems.brs/brsTrnRepaymentUnReconcillation.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })

            .state('app.brsTrnReconcillationdebit', {
                url: '/brsTrnReconcillationdebit',
                title: 'brsTrnReconcillationdebit',
                templateUrl: helper.basepath('ems.brs/brsTrnReconcillationdebit.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })
            .state('app.brsTrnUnReconcillationdebit', {
                url: '/brsTrnUnReconcillationdebit',
                title: 'brsTrnUnReconcillationdebit',
                templateUrl: helper.basepath('ems.brs/brsTrnUnReconcillationdebit.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })
            .state('app.brsTrnUnReconcillationPendingdebitManagement', {
                url: '/brsTrnUnReconcillationPendingdebitManagement',
                title: 'brsTrnUnReconcillationPendingdebitManagement',
                templateUrl: helper.basepath('ems.brs/brsTrnUnReconcillationPendingdebitManagement.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })
            .state('app.brsTrnUnReconcillationCompleteddebitManagement', {
                url: '/brsTrnUnReconcillationCompleteddebitManagement',
                title: 'brsTrnUnReconcillationCompleteddebitManagement',
                templateUrl: helper.basepath('ems.brs/brsTrnUnReconcillationCompleteddebitManagement.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })
            .state('app.brsTrnUnreconcillationTransfer', {
                url: '/brsTrnUnreconcillationTransfer',
                title: 'brsTrnUnreconcillationTransfer',
                templateUrl: helper.basepath('ems.brs/brsTrnUnreconcillationTransfer.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })
            .state('app.brsTrnMyUnReconClosedSummary', {
                url: '/brsTrnMyUnReconClosedSummary',
                title: 'brsTrnMyUnReconClosedSummary',
                templateUrl: helper.basepath('ems.brs/brsTrnMyUnReconClosedSummary.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })

            .state('app.brsTrnMyUnReconciliationSummary', {
                url: '/brsTrnMyUnReconciliationSummary',
                title: 'brsTrnMyUnReconciliationSummary',
                templateUrl: helper.basepath('ems.brs/brsTrnMyUnReconciliationSummary.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })
            .state('app.MstDisbursementDocument', {                
                url: '/MstDisbursementDocument',                
                title: 'MstDisbursementDocument',                
                templateUrl: helper.basepath('ems.master/MstDisbursementDocument.html?ver=' + version + '"'),                
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')            
            })
            .state('app.MstDeviationApprovalGroup', {
                url: '/MstDeviationApprovalGroup',
                title: 'MstDeviationApprovalGroup',
                templateUrl: helper.basepath('ems.master/MstDeviationApprovalGroup.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })
            .state('app.MstCADPSLCSAGuarantorDetails', {
                url: '/MstCADPSLCSAGuarantorDetails',
                title: 'MstCADPSLCSAGuarantorDetails',
                templateUrl: helper.basepath('ems.master/MstCADPSLCSAGuarantorDetails.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
            })
        
            .state('app.SysprtAgriCommerceTradeReceivablesQuality', {
                url: '/SysprtAgriCommerceTradeReceivablesQuality',
                title: 'SysprtAgriCommerceTradeReceivablesQuality',
                templateUrl: helper.basepath('ems.system/SysprtAgriCommerceTradeReceivablesQuality.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'ngWig')
            })

            .state('app.SysprtAgriFinanceApplicationStatus', {
                url: '/SysprtAgriFinanceApplicationStatus',
                title: 'SysprtAgriFinanceApplicationStatus',
                templateUrl: helper.basepath('ems.system/SysprtAgriFinanceApplicationStatus.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'ngWig')
            })

            .state('app.SysprtAgriCommerceApplicationStatus', {
                url: '/SysprtAgriCommerceApplicationStatus',
                title: 'SysprtAgriCommerceApplicationStatus',
                templateUrl: helper.basepath('ems.system/SysprtAgriCommerceApplicationStatus.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'ngWig')
            })

            .state('app.EnquiryRequire', {
                url: '/EnquiryRequire',
                title: 'EnquiryRequire',
                templateUrl: helper.basepath('ems.businessteam/EnquiryRequire.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })

            .state('app.StartupRequire', {
                url: '/StartupRequire',
                title: 'StartupRequire',
                templateUrl: helper.basepath('ems.businessteam/StartupRequire.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })

            .state('app.MstSBAManagement', {
                url: '/MstSBAManagement',
                title: 'MstSBAManagement',
                templateUrl: helper.basepath('ems.master/MstSBAManagement.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
            })
            .state('app.MstSBAIndividualManagement', {
                url: '/MstSBAIndividualManagement',
                title: 'MstSBAIndividualManagement',
                templateUrl: helper.basepath('ems.master/MstSBAIndividualManagement.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
            })
            .state('app.brsTrnCreditMatched', {
                url: '/brsTrnCreditMatched',
                title: 'brsTrnCreditMatched',
                templateUrl: helper.basepath('ems.brs/brsTrnCreditMatched.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })

            .state('app.brsTrnDebitMatched', {
                url: '/brsTrnDebitMatched',
                title: 'brsTrnDebitMatched',
                templateUrl: helper.basepath('ems.brs/brsTrnDebitMatched.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })

            .state('app.brsTrnCreditPartialMatched', {
                url: '/brsTrnCreditPartialMatched',
                title: 'brsTrnCreditPartialMatched',
                templateUrl: helper.basepath('ems.brs/brsTrnCreditPartialMatched.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })


            .state('app.brsTrnCreditUnMatchedUnAssigned', {
                url: '/brsTrnCreditUnMatchedUnAssigned',
                title: 'brsTrnCreditUnMatchedUnAssigned',
                templateUrl: helper.basepath('ems.brs/brsTrnCreditUnMatchedUnAssigned.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })


            .state('app.brsTrnCreditUnMatchedAssigned', {
                url: '/brsTrnCreditUnMatchedAssigned',
                title: 'brsTrnCreditUnMatchedAssigned',
                templateUrl: helper.basepath('ems.brs/brsTrnCreditUnMatchedAssigned.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })


            .state('app.brsTrnCreditClosed', {
                url: '/brsTrnCreditClosed',
                title: 'brsTrnCreditClosed',
                templateUrl: helper.basepath('ems.brs/brsTrnCreditClosed.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })



            .state('app.brsTrnDebitPartialMatched', {
                url: '/brsTrnDebitPartialMatched',
                title: 'brsTrnDebitPartialMatched',
                templateUrl: helper.basepath('ems.brs/brsTrnDebitPartialMatched.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })


            .state('app.brsTrnDebitUnMatchedUnAssigned', {
                url: '/brsTrnDebitUnMatchedUnAssigned',
                title: 'brsTrnDebitUnMatchedUnAssigned',
                templateUrl: helper.basepath('ems.brs/brsTrnDebitUnMatchedUnAssigned.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })

            .state('app.brsTrnDebitUnMatchedAssigned', {
                url: '/brsTrnDebitUnMatchedAssigned',
                title: 'brsTrnDebitUnMatchedAssigned',
                templateUrl: helper.basepath('ems.brs/brsTrnDebitUnMatchedAssigned.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })
            .state('app.brsTrnDebitClosed', {
                url: '/brsTrnDebitClosed',
                title: 'brsTrnDebitClosed',
                templateUrl: helper.basepath('ems.brs/brsTrnDebitClosed.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })
            .state('app.brsTrnUnreconCreditSummaryManagement', {
                url: '/brsTrnUnreconCreditSummaryManagement',
                title: 'brsTrnUnreconCreditSummaryManagement',
                templateUrl: helper.basepath('ems.brs/brsTrnUnreconCreditSummaryManagement.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })

            .state('app.brsTrnUnReconCreditAssignedManagement', {
                url: '/brsTrnUnReconCreditAssignedManagement',
                title: 'brsTrnUnReconCreditAssignedManagement',
                templateUrl: helper.basepath('ems.brs/brsTrnUnReconCreditAssignedManagement.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })


            .state('app.brsTrnUnReconCreditClosedManagement', {
                url: '/brsTrnUnReconCreditClosedManagement',
                title: 'brsTrnUnReconCreditClosedManagement',
                templateUrl: helper.basepath('ems.brs/brsTrnUnReconCreditClosedManagement.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })

            .state('app.brsTrnUnreconDebitSummaryManagement', {
                url: '/brsTrnUnreconDebitSummaryManagement',
                title: 'brsTrnUnreconDebitSummaryManagement',
                templateUrl: helper.basepath('ems.brs/brsTrnUnreconDebitSummaryManagement.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })


            .state('app.brsTrnUnReconDebitAssignedManagement', {
                url: '/brsTrnUnReconDebitAssignedManagement',
                title: 'brsTrnUnReconDebitAssignedManagement',
                templateUrl: helper.basepath('ems.brs/brsTrnUnReconDebitAssignedManagement.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })


            .state('app.brsTrnUnReconDebitClosedManagement', {
                url: '/brsTrnUnReconDebitClosedManagement',
                title: 'brsTrnUnReconDebitClosedManagement',
                templateUrl: helper.basepath('ems.brs/brsTrnUnReconCreditClosedManagement.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })

             .state('app.MstFieldMappingSummary', {
                 url: '/MstFieldMappingSummary',
                 title: 'MstFieldMappingSummary',
                 templateUrl: helper.basepath('ems.master/MstFieldMappingSummary.html?ver=' + version + '"'),
                 resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
             })

            .state('app.brsTrnMyUnReconAlloactedPendingSummary', {
                url: '/brsTrnMyUnReconAlloactedPendingSummary',
                title: 'brsTrnMyUnReconAlloactedPendingSummary',
                templateUrl: helper.basepath('ems.brs/brsTrnMyUnReconAlloactedPendingSummary.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })
            .state('app.brsTrnPartialMatchedView', {
                url: '/brsTrnPartialMatchedView',
                title: 'brsTrnPartialMatchedView',
                templateUrl: helper.basepath('ems.brs/brsTrnPartialMatchedView.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })

            .state('app.MstCreditManagerRejectRevokeSummary', {
                url: '/MstCreditManagerRejectRevokeSummary',
                title: 'MstCreditManagerRejectRevokeSummary',
                templateUrl: helper.basepath('ems.master/MstCreditManagerRejectRevokeSummary.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })
            .state('app.MstCreditManagerRejectRevoke', {
                url: '/MstCreditManagerRejectRevoke',
                title: 'MstCreditManagerRejectRevoke',
                templateUrl: helper.basepath('ems.master/MstCreditManagerRejectRevoke.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })
            .state('app.SBAProfileDetails', {
                url: '/SBAProfileDetails',
                title: 'SBAProfileDetails',
                templateUrl: helper.basepath('ems.sbiform/SBAProfileDetails.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })
            .state('app.MstSBAManagementInstitutionCodeCreateView', {
                url: '/MstSBAManagementInstitutionCodeCreateView',
                title: 'MstSBAManagementInstitutionCodeCreateView',
                templateUrl: helper.basepath('ems.master/MstSBAManagementInstitutionCodeCreateView.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })

            .state('app.MstSBAManagementIndividualCodeCreateView', {
                url: '/MstSBAManagementIndividualCodeCreateView',
                title: 'MstSBAManagementIndividualCodeCreateView',
                templateUrl: helper.basepath('ems.master/MstSBAManagementIndividualCodeCreateView.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })

            .state('app.AtmTrnTaggedAuditeeObservationScoreView', {
                url: '/AtmTrnTaggedAuditeeObservationScoreView',
                title: 'AtmTrnTaggedAuditeeObservationScoreView',
                templateUrl: helper.basepath('ems.audit/AtmTrnTaggedAuditeeObservationScoreView.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })

            .state('app.MstSAOnboardingInstitutionRenewalGrouping', {
                url: '/MstSAOnboardingInstitutionRenewalGrouping',
                title: 'MstSAOnboardingInstitutionRenewalGrouping',
                templateUrl: helper.basepath('ems.master/MstSAOnboardingInstitutionRenewalGrouping.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })
            .state('app.MstSAOnboardingIndividualRenewalGrouping', {
                url: '/MstSAOnboardingIndividualRenewalGrouping',
                title: 'MstSAOnboardingIndividualRenewalGrouping',
                templateUrl: helper.basepath('ems.master/MstSAOnboardingIndividualRenewalGrouping.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })
            .state('app.MstSAOnboardingInstitutionRenewal', {
                url: '/MstSAOnboardingInstitutionRenewal',
                title: 'MstSAOnboardingInstitutionRenewal',
                templateUrl: helper.basepath('ems.master/MstSAOnboardingInstitutionRenewal.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })
            .state('app.MstSAOnboardingIndividualRenewal', {
                url: '/MstSAOnboardingIndividualRenewal',
                title: 'MstSAOnboardingIndividualRenewal',
                templateUrl: helper.basepath('ems.master/MstSAOnboardingIndividualRenewal.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })
            .state('app.MstSAInstitutionActivityManagement', {
                url: '/MstSAInstitutionActivityManagement',
                title: 'MstSAInstitutionActivityManagement',
                templateUrl: helper.basepath('ems.master/MstSAInstitutionActivityManagement.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })
            .state('app.MstSBAIndividualActivityManagement', {
                url: '/MstSBAIndividualActivityManagement',
                title: 'MstSBAIndividualActivityManagement',
                templateUrl: helper.basepath('ems.master/MstSBAIndividualActivityManagement.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })
            .state('app.MstSAOnboardingInstitutionRejectedView', {
                url: '/MstSAOnboardingInstitutionRejectedView',
                title: 'MstSAOnboardingInstitutionRejectedView',
                templateUrl: helper.basepath('ems.master/MstSAOnboardingInstitutionRejectedView.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })
            .state('app.MstSAOnboardingIndividualRejectedView', {
                url: '/MstSAOnboardingIndividualRejectedView',
                title: 'MstSAOnboardingIndividualRejectedView',
                templateUrl: helper.basepath('ems.master/MstSAOnboardingIndividualRejectedView.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })

            .state('app.MstSAOnboardingRejectedSummary', {
                url: '/MstSAOnboardingRejectedSummary',
                title: 'MstSAOnboardingRejectedSummary',
                templateUrl: helper.basepath('ems.master/MstSAOnboardingRejectedSummary.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })
            .state('app.MstSAOnboardingIndividualRejectedSummary', {
                url: '/MstSAOnboardingIndividualRejectedSummary',
                title: 'MstSAOnboardingIndividualRejectedSummary',
                templateUrl: helper.basepath('ems.master/MstSAOnboardingIndividualRejectedSummary.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })
            .state('app.MstSAOnboardingInstitutionGrouping', {
                url: '/MstSAOnboardingInstitutionGrouping',
                title: 'MstSAOnboardingInstitutionGrouping',
                templateUrl: helper.basepath('ems.master/MstSAOnboardingInstitutionGrouping.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })
            .state('app.MstSAOnboardingIndividualGrouping', {
                url: '/MstSAOnboardingIndividualGrouping',
                title: 'MstSAOnboardingIndividualGrouping',
                templateUrl: helper.basepath('ems.master/MstSAOnboardingIndividualGrouping.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })
            .state('app.MstRMCustomerDashboard', {
                url: '/MstRMCustomerDashboard',
                title: 'MstRMCustomerDashboard',
                templateUrl: helper.basepath('ems.master/MstRMCustomerDashboard.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })
            .state('app.MstDisbursementDeferralDocument', {
                url: '/MstDisbursementDeferralDocument',
                title: 'MstDisbursementDeferralDocument',
                templateUrl: helper.basepath('ems.master/MstDisbursementDeferralDocument.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })
            .state('app.MstDisbursementBuyerDtlView', {
                url: '/MstDisbursementBuyerDtlView',
                title: 'MstDisbursementBuyerDtlView',
                templateUrl: helper.basepath('ems.master/MstDisbursementBuyerDtlView.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })
            .state('app.MstDisbIndividualFarmerDtlView', {
                url: '/MstDisbIndividualFarmerDtlView',
                title: 'MstDisbIndividualFarmerDtlView',
                templateUrl: helper.basepath('ems.master/MstDisbIndividualFarmerDtlView.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })
            .state('app.MstDisbCoApplicantDtlAdd', {
                url: '/MstDisbCoApplicantDtlAdd',
                title: 'MstDisbCoApplicantDtlAdd',
                templateUrl: helper.basepath('ems.master/MstDisbCoApplicantDtlAdd.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })
            .state('app.MstDisbCoApplicantContactDtlView', {
                url: '/MstDisbCoApplicantContactDtlView',
                title: 'MstDisbCoApplicantContactDtlView',
                templateUrl: helper.basepath('ems.master/MstDisbCoApplicantContactDtlView.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })
            .state('app.MstDisbursementRejectedSummary', {
                url: '/MstDisbursementRejectedSummary',
                title: 'MstDisbursementRejectedSummary',
                templateUrl: helper.basepath('ems.master/MstDisbursementRejectedSummary.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })
            .state('app.MstCreditOpsDisbursementRejectedSummary', {
                url: '/MstCreditOpsDisbursementRejectedSummary',
                title: 'MstCreditOpsDisbursementRejectedSummary',
                templateUrl: helper.basepath('ems.master/MstCreditOpsDisbursementRejectedSummary.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })

            .state('app.MstDisbursementBankAccount', {
                url: '/MstDisbursementBankAccount',
                title: 'MstDisbursementBankAccount',
                templateUrl: helper.basepath('ems.master/MstDisbursementBankAccount.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })
            .state('app.MstDisbursementODBelow30', {
                url: '/MstDisbursementODBelow30',
                title: 'MstDisbursementODBelow30',
                templateUrl: helper.basepath('ems.master/MstDisbursementODBelow30.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })
            .state('app.MstDisbursementODBelow90', {
                url: '/MstDisbursementODBelow90',
                title: 'MstDisbursementODBelow90',
                templateUrl: helper.basepath('ems.master/MstDisbursementODBelow90.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })
            .state('app.MstPenalWaiver', {
                url: '/MstPenalWaiver',
                title: 'MstPenalWaiver',
                templateUrl: helper.basepath('ems.master/MstPenalWaiver.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })
            .state('app.MstVerticalDisbursementDocument', {
                url: '/MstVerticalDisbursementDocument',
                title: 'MstVerticalDisbursementDocument',
                templateUrl: helper.basepath('ems.master/MstVerticalDisbursementDocument.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })
            .state('app.MstCADCustomerCreationLMS', {
            url: '/MstCADCustomerCreationLMS',
            title: 'MstCADCustomerCreationLMS',
            templateUrl: helper.basepath('ems.master/MstCADCustomerCreationLMS.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })


        .state('app.MstCustomerCreationRequestSummary', {
            url: '/MstCustomerCreationRequestSummary',
            title: 'MstCustomerCreationRequestSummary',
            templateUrl: helper.basepath('ems.master/MstCustomerCreationRequestSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })


        .state('app.MstCustomerUpdatedRequestSummary', {
            url: '/MstCustomerUpdatedRequestSummary',
            title: 'MstCustomerUpdatedRequestSummary',
            templateUrl: helper.basepath('ems.master/MstCustomerUpdatedRequestSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.MstCustomerRejectedRequestSummary', {
            url: '/MstCustomerRejectedRequestSummary',
            title: 'MstCustomerRejectedRequestSummary',
            templateUrl: helper.basepath('ems.master/MstCustomerRejectedRequestSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.MstCustomercreationlmsurnupdation', {
            url: '/MstCustomercreationlmsurnupdation',
            title: 'MstCustomercreationlmsurnupdation',
            templateUrl: helper.basepath('ems.master/MstCustomercreationlmsurnupdation.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.MstDocumentChecklistApprovalCompletedReport', {
             url: '/MstDocumentChecklistApprovalCompletedReport',
             title: 'MstDocumentChecklistApprovalCompletedReport',
             templateUrl: helper.basepath('ems.master/MstDocumentChecklistApprovalCompletedReport.html?ver=' + version + '"'),
             resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
       
        
        // -----------------------------------
        // -----------------------------------
        //	
        // CUSTOM RESOLVES
        //   Add your own resolves properties
        //   following this object extend
        //   method
        // -----------------------------------
        // .state('app.someroute', {
        //   url: '/some_url',
        //   templateUrl: 'path_to_template.html',
        //   controller: 'someController',
        //   resolve: angular.extend(
        //     helper.resolveFor(), {
        //     // YOUR RESOLVES GO HERE
        //     }
        //   )
        // })
        ;

    } // routesConfig

    angular.module('angle').config(['$httpProvider', function ($httpProvider) {
        $httpProvider.interceptors.push(function ($q, $location, $rootScope, $cookies) {
            return {
                'request': function (config) {
                    config.headers.Authorization = $cookies.getObject('token');
                    //$http.defaults.headers.common.Authorization = localStorage.getItem("token");
                    return config;
                },
                'response': function (response) {
                    //Will only be called for HTTP up to 300
                    //console.log(response);
                    return response;
                },
                'responseError': function (rejection) {

                    if (rejection.status === 401) {
                        // $location.url('page/404?errno=401');
                    }
                    else if (rejection.status === 404) {
                        //console.log('404');
                        //$location.url('page/404?errno=404');
                    }
                    else if (rejection.status === 400) {
                        // $location.url('page/404?errno=400');
                    }
                    else if (rejection.status === 403) {
                        // $location.url('page/404?errno=403');
                    }
                    ////$state.go('page.404');
                    if (rejection.message === 404) {
                        //location.reload();
                    }
                    return $q.reject(rejection);
                }
            };
        });
    }]); 


})();