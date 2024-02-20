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
        
        

         .state('app.AgrTrnCreditCommitteeSummary', {
            url: '/AgrTrnCreditCommitteeSummary',
            title: 'AgrTrnCreditCommitteeSummary',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnCreditCommitteeSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
    .state('app.AgrTrnCcScheduledMeetingSummary', {
            url: '/AgrTrnCcScheduledMeetingSummary',
            title: 'AgrTrnCcScheduledMeetingSummary',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnCcScheduledMeetingSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'textAngular', 'jquery-ui', 'jquery-ui-widgets', 'moment', 'fullcalendar')
        })
   .state('app.AgrMstSupplierSummary', {
            url: '/AgrMstSupplierSummary',
            title: 'AgrMstSupplierSummary',
            templateUrl: helper.basepath('ems.mastersamagro/AgrMstSupplierSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

  .state('app.AgrMstCadDeferralSummary', {
            url: '/AgrMstCadDeferralSummary',
            title: 'AgrMstCadDeferralSummary',
            templateUrl: helper.basepath('ems.mastersamagro/AgrMstCadDeferralSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

    .state('app.AgrMstLoanProductsSummary', {
            url: '/AgrMstLoanProductsSummary',
            title: 'AgrMstLoanProductsSummary',
            templateUrl: helper.basepath('ems.mastersamagro/AgrMstLoanProductsSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

     .state('app.AgrMstLoanSubProduct', {
            url: '/AgrMstLoanSubProduct',
            title: 'AgrMstLoanSubProduct',
            templateUrl: helper.basepath('ems.mastersamagro/AgrMstLoanSubProduct.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

       .state('app.AgrTrnCCscheduledSummary', {
            url: '/AgrTrnCCscheduledSummary',
            title: 'AgrTrnCCscheduledSummary',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnCCscheduledSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

       .state('app.AgrTrnCCCompletedSummary', {
            url: '/AgrTrnCCCompletedSummary',
            title: 'AgrTrnCCCompletedSummary',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnCCCompletedSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

       .state('app.AgrTrnSentbackcctoCredit', {
            url: '/AgrTrnSentbackcctoCredit',
            title: 'AgrTrnSentbackcctoCredit',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnSentbackcctoCredit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

       .state('app.AgrTrnCcCompletedScheduledMeeting', {
            url: '/AgrTrnCcCompletedScheduledMeeting',
            title: 'AgrTrnCcCompletedScheduledMeeting',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnCcCompletedScheduledMeeting.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
         .state('app.AgrTrnCreditApprovalSummary', {
            url: '/AgrTrnCreditApprovalSummary',
            title: 'AgrTrnCreditApprovalSummary',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnCreditApprovalSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

         .state('app.AgrTrnCreditApprovedSummary', {
            url: '/AgrTrnCreditApprovedSummary',
            title: 'AgrTrnCreditApprovedSummary',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnCreditApprovedSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.AgrTrnCreditSubmittedtoCCSummary', {
            url: '/AgrTrnCreditSubmittedtoCCSummary',
            title: 'AgrTrnCreditSubmittedtoCCSummary',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnCreditSubmittedtoCCSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.AgrTrnCreditCCSkippedSummary', {
            url: '/AgrTrnCreditCCSkippedSummary',
            title: 'AgrTrnCreditCCSkippedSummary',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnCreditCCSkippedSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.AgrTrnCreditRejectandHoldSummary', {
            url: '/AgrTrnCreditRejectandHoldSummary',
            title: 'AgrTrnCreditRejectandHoldSummary',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnCreditRejectandHoldSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
    .state('app.AgrMstCadDeferralCheckerSummary', {
      url: '/AgrMstCadDeferralCheckerSummary',
      title: 'AgrMstCadDeferralCheckerSummary',
      templateUrl: helper.basepath('ems.mastersamagro/AgrMstCadDeferralCheckerSummary.html?ver=' + version + '"'),
      resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
    })

     .state('app.AgrMstRMCustomerSummary', {
            url: '/AgrMstRMCustomerSummary',
            title: 'AgrMstRMCustomerSummary',
            templateUrl: helper.basepath('ems.mastersamagro/AgrMstRMCustomerSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
	.state('app.AgrMstCadDeferralApprovalSummary', {
      url: '/AgrMstCadDeferralApprovalSummary',
      title: 'AgrMstCadDeferralApprovalSummary',
      templateUrl: helper.basepath('ems.mastersamagro/AgrMstCadDeferralApprovalSummary.html?ver=' + version + '"'),
      resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
    })
	.state('app.AgrMstScannedCompletedSummary', {
      url: '/AgrMstScannedCompletedSummary',
      title: 'AgrMstScannedCompletedSummary',
      templateUrl: helper.basepath('ems.mastersamagro/AgrMstScannedCompletedSummary.html?ver=' + version + '"'),
      resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
    })
	.state('app.AgrMstCadChequeManagementSummary', {
      url: '/AgrMstCadChequeManagementSummary',
      title: 'AgrMstCadChequeManagementSummary',
      templateUrl: helper.basepath('ems.mastersamagro/AgrMstCadChequeManagementSummary.html?ver=' + version + '"'),
      resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
    })

    .state('app.AgrMstCadChequeMgmtCheckerSummary', {
      url: '/AgrMstCadChequeMgmtCheckerSummary',
      title: 'AgrMstCadChequeMgmtCheckerSummary',
      templateUrl: helper.basepath('ems.mastersamagro/AgrMstCadChequeMgmtCheckerSummary.html?ver=' + version + '"'),
      resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
    })

    .state('app.AgrMstCadChequeMgmtApprovalSummary', {
      url: '/AgrMstCadChequeMgmtApprovalSummary',
      title: 'AgrMstCadChequeMgmtApprovalSummary',
      templateUrl: helper.basepath('ems.mastersamagro/AgrMstCadChequeMgmtApprovalSummary.html?ver=' + version + '"'),
      resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
    })

    .state('app.AgrMstChequeApprovalCompleted', {
      url: '/AgrMstChequeApprovalCompleted',
      title: 'AgrMstChequeApprovalCompleted',
      templateUrl: helper.basepath('ems.mastersamagro/AgrMstChequeApprovalCompleted.html?ver=' + version + '"'),
      resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
    })

	.state('app.AgrMstApplicationCreationSummary', {
      url: '/AgrMstApplicationCreationSummary',
      title: 'AgrMstApplicationCreationSummary',
      templateUrl: helper.basepath('ems.mastersamagro/AgrMstApplicationCreationSummary.html?ver=' + version + '"'),
      resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
    })
    .state('app.AgrMstRejectedApplicationSummary', {
      url: '/AgrMstRejectedApplicationSummary',
      title: 'AgrMstRejectedApplicationSummary',
      templateUrl: helper.basepath('ems.mastersamagro/AgrMstRejectedApplicationSummary.html?ver=' + version + '"'),
      resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
    })
    .state('app.AgrMstHoldApplicationSummary', {
      url: '/AgrMstHoldApplicationSummary',
      title: 'AgrMstHoldApplicationSummary',
      templateUrl: helper.basepath('ems.mastersamagro/AgrMstHoldApplicationSummary.html?ver=' + version + '"'),
      resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
    })
    .state('app.AgrMstApprovedApplicationSummary', {
      url: '/AgrMstApprovedApplicationSummary',
      title: 'AgrMstApprovedApplicationSummary',
      templateUrl: helper.basepath('ems.mastersamagro/AgrMstApprovedApplicationSummary.html?ver=' + version + '"'),
      resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
    })

	.state('app.AgrTrnCadPhysicalDocCheckerSummary', {
      url: '/AgrTrnCadPhysicalDocCheckerSummary',
      title: 'AgrTrnCadPhysicalDocCheckerSummary',
      templateUrl: helper.basepath('ems.mastersamagro/AgrTrnCadPhysicalDocCheckerSummary.html?ver=' + version + '"'),
      resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
    })

        .state('app.AgrTrnCadDocumentChecklistSummary', {
            url: '/AgrTrnCadDocumentChecklistSummary',
            title: 'AgrTrnCadDocumentChecklistSummary',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnCadDocumentChecklistSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

      .state('app.AgrTrnCadDocChecklistCheckerSummary', {
            url: '/AgrTrnCadDocChecklistCheckerSummary',
            title: 'AgrTrnCadDocChecklistCheckerSummary',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnCadDocChecklistCheckerSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

     .state('app.AgrTrnCadDocChecklistApprovalSummary', {
            url: '/AgrTrnCadDocChecklistApprovalSummary',
            title: 'AgrTrnCadDocChecklistApprovalSummary',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnCadDocChecklistApprovalSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

    .state('app.AgrTrnCadPhysicalDocCompletedSummary', {
      url: '/AgrTrnCadPhysicalDocCompletedSummary',
      title: 'AgrTrnCadPhysicalDocCompletedSummary',
      templateUrl: helper.basepath('ems.mastersamagro/AgrTrnCadPhysicalDocCompletedSummary.html?ver=' + version + '"'),
      resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
    })
	.state('app.AgrTrnCadPhysicalDocSummary', {
      url: '/AgrTrnCadPhysicalDocSummary',
      title: 'AgrTrnCadPhysicalDocSummary',
      templateUrl: helper.basepath('ems.mastersamagro/AgrTrnCadPhysicalDocSummary.html?ver=' + version + '"'),
      resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
    })
	.state('app.AgrTrnCadPhysicalDocApprovalSummary', {
      url: '/AgrTrnCadPhysicalDocApprovalSummary',
      title: 'AgrTrnCadPhysicalDocApprovalSummary',
      templateUrl: helper.basepath('ems.mastersamagro/AgrTrnCadPhysicalDocApprovalSummary.html?ver=' + version + '"'),
      resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
    })
    .state('app.AgrMstUDCMakerSummary', {
            url: '/AgrMstUDCMakerSummary',
            title: 'AgrMstUDCMakerSummary',
            templateUrl: helper.basepath('ems.mastersamagro/AgrMstUDCMakerSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

    .state('app.AgrMstUDCMakerAdd', {
            url: '/AgrMstUDCMakerAdd',
            title: 'AgrMstUDCMakerAdd',
            templateUrl: helper.basepath('ems.mastersamagro/AgrMstUDCMakerAdd.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

    .state('app.AgrMstUDCMakerView', {
            url: '/AgrMstUDCMakerView',
            title: 'AgrMstUDCMakerView',
            templateUrl: helper.basepath('ems.mastersamagro/AgrMstUDCMakerView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

   .state('app.AgrMstUDCMakerEditCheque', {
            url: '/AgrMstUDCMakerEditCheque',
            title: 'AgrMstUDCMakerView',
            templateUrl: helper.basepath('ems.mastersamagro/AgrMstUDCMakerEditCheque.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })


    .state('app.AgrMstChequeCheckerDtls', {
            url: '/AgrMstChequeCheckerDtls',
            title: 'AgrMstChequeCheckerDtls',
            templateUrl: helper.basepath('ems.mastersamagro/AgrMstChequeCheckerDtls.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })


  .state('app.AgrMstChequeApprovalDtls', {
            url: '/AgrMstChequeApprovalDtls',
            title: 'AgrMstChequeApprovalDtls',
            templateUrl: helper.basepath('ems.mastersamagro/AgrMstChequeApprovalDtls.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })


   .state('app.AgrMstChequeMakerFollowDtls', {
            url: '/AgrMstChequeMakerFollowDtls',
            title: 'AgrMstChequeMakerFollowDtls',
            templateUrl: helper.basepath('ems.mastersamagro/AgrMstChequeMakerFollowDtls.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })


  .state('app.AgrMstCadApplicationView', {
            url: '/AgrMstCadApplicationView',
            title: 'AgrMstCadApplicationView',
            templateUrl: helper.basepath('ems.mastersamagro/AgrMstCadApplicationView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })


    .state('app.AgrTrnCadDeferralGuarantorDtls', {
      url: '/AgrTrnCadDeferralGuarantorDtls',
      title: 'AgrTrnCadDeferralGuarantorDtls',
      templateUrl: helper.basepath('ems.mastersamagro/AgrTrnCadDeferralGuarantorDtls.html?ver=' + version + '"'),
      resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
    })


  .state('app.AgrTrnPendingCADReview', {
            url: '/AgrTrnPendingCADReview',
            title: 'AgrTrnPendingCADReview',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnPendingCADReview.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
            
        .state('app.AgrTrnCadAcceptedCustomers', {
            url: '/AgrTrnCadAcceptedCustomers',
            title: 'AgrTrnCadAcceptedCustomers',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnCadAcceptedCustomers.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.AgrTrnSentBackToUnderwriting', {
            url: '/AgrTrnSentBackToUnderwriting',
            title: 'AgrTrnSentBackToUnderwriting',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnSentBackToUnderwriting.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.AgrTrnSentBackToCC', {
            url: '/AgrTrnSentBackToCC',
            title: 'AgrTrnSentBackToCC',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnSentBackToCC.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.AgrTrnCCRejectedApplications', {
            url: '/AgrTrnCCRejectedApplications',
            title: 'AgrTrnCCRejectedApplications',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnCCRejectedApplications.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

    .state('app.AgrAppassignedAssignmentSummary', {
	url: '/AgrAppassignedAssignmentSummary',
	title: 'AgrAppassignedAssignmentSummary',
	templateUrl: helper.basepath('ems.mastersamagro/AgrAppassignedAssignmentSummary.html?ver=' + version + '"'),
	resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
    })



    .state('app.AgrApplicationAssignmentSummary', {
     url: '/AgrApplicationAssignmentSummary',
     title: 'AgrApplicationAssignmentSummary',
      templateUrl: helper.basepath('ems.mastersamagro/AgrApplicationAssignmentSummary.html?ver=' + version + '"'),
    resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
    })



    .state('app.AgrApplSubmittedtoCCSummary', {
    url: '/AgrApplSubmittedtoCCSummary',
   title: 'AgrApplSubmittedtoCCSummary',
    templateUrl: helper.basepath('ems.mastersamagro/AgrApplSubmittedtoCCSummary.html?ver=' + version + '"'),
   resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
    })



    .state('app.AgrMstMyApplicationsSummary', {
    url: '/AgrMstMyApplicationsSummary',
    title: 'AgrMstMyApplicationsSummary',
    templateUrl: helper.basepath('ems.mastersamagro/AgrMstMyApplicationsSummary.html?ver=' + version + '"'),
    resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
    })



    .state('app.AgrMstSubmittedtoApprovalSummary', {
     url: '/AgrMstSubmittedtoApprovalSummary',
     title: 'AgrMstSubmittedtoApprovalSummary',
     templateUrl: helper.basepath('ems.mastersamagro/AgrMstSubmittedtoApprovalSummary.html?ver=' + version + '"'),
    resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
    })



      .state('app.AgrMstSubmittedtoCCSummary', {
      url: '/AgrMstSubmittedtoCCSummary',
       title: 'AgrMstSubmittedtoCCSummary',
     templateUrl: helper.basepath('ems.mastersamagro/AgrMstSubmittedtoCCSummary.html?ver=' + version + '"'),
      resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
      })



       .state('app.AgrMstCCSkippedApplicationSummary', {
        url: '/AgrMstCCSkippedApplicationSummary',
       title: 'AgrMstCCSkippedApplicationSummary',
      templateUrl: helper.basepath('ems.mastersamagro/AgrMstCCSkippedApplicationSummary.html?ver=' + version + '"'),
     resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
       })

     .state('app.AgrMstRejectandHoldSummary', {
     url: '/AgrMstRejectandHoldSummary',
     title: 'AgrMstRejectandHoldSummary',
      templateUrl: helper.basepath('ems.mastersamagro/AgrMstRejectandHoldSummary.html?ver=' + version + '"'),
     resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
      })

	.state('app.AgrTrnCadDeferralDochecklist', {
            url: '/AgrTrnCadDeferralDochecklist',
            title: 'AgrTrnCadDeferralDochecklist',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnCadDeferralDochecklist.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
	.state('app.AgrTrnScannedDeferralHistory', {
            url: '/AgrTrnScannedDeferralHistory',
            title: 'AgrTrnScannedDeferralHistory',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnScannedDeferralHistory.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
	.state('app.AgrTrnCadDeferralQuery', {
            url: '/AgrTrnCadDeferralQuery',
            title: 'AgrTrnCadDeferralQuery',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnCadDeferralQuery.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
        .state('app.AgrTrnCadDeferralStatus', {
            url: '/AgrTrnCadDeferralStatus',
            title: 'AgrTrnCadDeferralStatus',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnCadDeferralStatus.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

         .state('app.AgrTrnCadDocChecklistSummary', {
            url: '/AgrTrnCadDocChecklistSummary',
            title: 'AgrTrnCadDocChecklistSummary',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnCadDocChecklistSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })


	.state('app.AgrTrnCcCommitteeApplicationView', {
            url: '/AgrTrnCcCommitteeApplicationView',
            title: 'AgrTrnCcCommitteeApplicationView',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnCcCommitteeApplicationView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

     
      .state('app.AgrTrnCadGuarantorDetails', {
            url: '/AgrTrnCadGuarantorDetails',
            title: 'AgrTrnCadGuarantorDetails',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnCadGuarantorDetails.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
      .state('app.AgrTrnCadDocumentChecklistAdd', {
            url: '/AgrTrnCadDocumentChecklistAdd',
            title: 'AgrTrnCadDocumentChecklistAdd',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnCadDocumentChecklistAdd.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

	 .state('app.AgrTrnCCMeetingSchedule', {
            url: '/AgrTrnCCMeetingSchedule',
            title: 'AgrTrnCCMeetingSchedule',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnCCMeetingSchedule.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

     .state('app.AgrTrnSentbackCadtoCcHistory', {
            url: '/AgrTrnSentbackCadtoCcHistory',
            title: 'AgrTrnSentbackCadtoCcHistory',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnSentbackCadtoCcHistory.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

     .state('app.AgrTrnCCMeetingReschedule', {
            url: '/AgrTrnCCMeetingReschedule',
            title: 'AgrTrnCCMeetingReschedule',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnCCMeetingReschedule.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

      .state('app.AgrTrnCcScheduledMeetingDtlView', {
            url: '/AgrTrnCcScheduledMeetingDtlView',
            title: 'AgrTrnCcScheduledMeetingDtlView',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnCcScheduledMeetingDtlView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
 

	 .state('app.AgrTrnApplCreationGradingToolView', {
            url: '/AgrTrnApplCreationGradingToolView',
            title: 'AgrTrnApplCreationGradingToolView',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnApplCreationGradingToolView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
 
	 .state('app.AgrTrnApplCreationVisitReportView', {
            url: '/AgrTrnApplCreationVisitReportView',
            title: 'AgrTrnApplCreationVisitReportView',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnApplCreationVisitReportView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
 
 	.state('app.AgrTrnCCCommitteeInstitutionView', {
            url: '/AgrTrnCCCommitteeInstitutionView',
            title: 'AgrTrnCCCommitteeInstitutionView',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnCCCommitteeInstitutionView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
 
        .state('app.AgrTrnCCCommitteeGroupView', {
            url: '/AgrTrnCCCommitteeGroupView',
            title: 'AgrTrnCCCommitteeGroupView',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnCCCommitteeGroupView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

       .state('app.AgrIECDetailedProfileView', {
            url: '/AgrIECDetailedProfileView',
            title: 'AgrIECDetailedProfileView',
            templateUrl: helper.basepath('ems.mastersamagro/AgrIECDetailedProfileView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

       .state('app.AgrGSTAuthenticationView', {
            url: '/AgrGSTAuthenticationView',
            title: 'AgrGSTAuthenticationView',
            templateUrl: helper.basepath('ems.mastersamagro/AgrGSTAuthenticationView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

       .state('app.AgrGSPGSTINAuthenticationView', {
            url: '/AgrGSPGSTINAuthenticationView',
            title: 'AgrGSPGSTINAuthenticationView',
            templateUrl: helper.basepath('ems.mastersamagro/AgrGSPGSTINAuthenticationView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
       .state('app.AgrGSPGSTReturnFilingView', {
            url: '/AgrGSPGSTReturnFilingView',
            title: 'AgrGSPGSTReturnFilingView',
            templateUrl: helper.basepath('ems.mastersamagro/AgrGSPGSTReturnFilingView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
        .state('app.AgrRCAuthAdvancedView', {
            url: '/AgrRCAuthAdvancedView',
            title: 'AgrRCAuthAdvancedView',
            templateUrl: helper.basepath('ems.mastersamagro/AgrRCAuthAdvancedView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.AgrPropertyTaxView', {
            url: '/AgrPropertyTaxView',
            title: 'AgrPropertyTaxView',
            templateUrl: helper.basepath('ems.mastersamagro/AgrPropertyTaxView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

	.state('app.AgrMstCreditAssessedScoreAdd', {
            url: '/AgrMstCreditAssessedScoreAdd',
            title: 'AgrMstCreditAssessedScoreAdd',
            templateUrl: helper.basepath('ems.mastersamagro/AgrMstCreditAssessedScoreAdd.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
            
        .state('app.AgrMstCreditAssessedScoreEdit', {
            url: '/AgrMstCreditAssessedScoreEdit',
            title: 'AgrMstCreditAssessedScoreEdit',
            templateUrl: helper.basepath('ems.mastersamagro/AgrMstCreditAssessedScoreEdit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
                
        .state('app.AgrMstCreditAssessedScoreView', {
            url: '/AgrMstCreditAssessedScoreView',
            title: 'AgrMstCreditAssessedScoreView',
            templateUrl: helper.basepath('ems.mastersamagro/AgrMstCreditAssessedScoreView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

            
        .state('app.AgrMstRMAssessedScoreView', {
            url: '/AgrMstRMAssessedScoreView',
            title: 'AgrMstRMAssessedScoreView',
            templateUrl: helper.basepath('ems.mastersamagro/AgrMstRMAssessedScoreView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
            
        .state('app.AgrCreditVisitReportAdd', {
            url: '/AgrCreditVisitReportAdd',
            title: 'AgrCreditVisitReportAdd',
            templateUrl: helper.basepath('ems.mastersamagro/AgrCreditVisitReportAdd.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
            
        .state('app.AgrMstRMVisitReportView', {
            url: '/AgrMstRMVisitReportView',
            title: 'AgrMstRMVisitReportView',
            templateUrl: helper.basepath('ems.mastersamagro/AgrMstRMVisitReportView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
            
        .state('app.AgrMstCreditVisitReportView', {
            url: '/AgrMstCreditVisitReportView',
            title: 'AgrMstCreditVisitReportView',
            templateUrl: helper.basepath('ems.mastersamagro/AgrMstCreditVisitReportView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
     .state('app.AgrApplicationCreationView', {
	url: '/AgrApplicationCreationView',
	title: 'AgrApplicationCreationView',
	templateUrl: helper.basepath('ems.mastersamagro/AgrApplicationCreationView.html?ver=' + version + '"'),
	resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
     })
        .state('app.AgrTrnStartScheduledMeeting', {
            url: '/AgrTrnStartScheduledMeeting',
            title: 'AgrTrnStartScheduledMeeting',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnStartScheduledMeeting.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

.state('app.AgrMstApplicationEditKycView', {
            url: '/AgrMstApplicationEditKycView',
            title: 'AgrMstApplicationEditKycView',
            templateUrl: helper.basepath('ems.mastersamagro/AgrMstApplicationEditKycView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

      .state('app.AgrMstVisitReportAdd', {
        url: '/AgrMstVisitReportAdd',
        title: 'AgrMstVisitReportAdd',
        templateUrl: helper.basepath('ems.mastersamagro/AgrMstVisitReportAdd.html?ver=' + version + '"'),
        resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
      })
      .state('app.AgrMstVisitReportEdit', {
        url: '/AgrMstVisitReportEdit',
        title: 'AgrMstVisitReportEdit',
        templateUrl: helper.basepath('ems.mastersamagro/AgrMstVisitReportEdit.html?ver=' + version + '"'),
        resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
      })
      .state('app.AgrMstVisitReportView', {
        url: '/AgrMstVisitReportView',
        title: 'AgrMstVisitReportView',
        templateUrl: helper.basepath('ems.mastersamagro/AgrMstVisitReportView.html?ver=' + version + '"'),
        resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
      })


      .state('app.AgrMstApplicationGeneralEdit', {
          url: '/AgrMstApplicationGeneralEdit',
          title: 'AgrMstApplicationGeneralEdit',
          templateUrl: helper.basepath('ems.mastersamagro/AgrMstApplicationGeneralEdit.html?ver=' + version + '"'),
          resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

      .state('app.AgrMstApplicationIndividualEdit', {
          url: '/AgrMstApplicationIndividualEdit',
          title: 'AgrMstApplicationIndividualEdit',
          templateUrl: helper.basepath('ems.mastersamagro/AgrMstApplicationIndividualEdit.html?ver=' + version + '"'),
          resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

      .state('app.AgrMstApplicationInstitutionEdit', {
          url: '/AgrMstApplicationInstitutionEdit',
          title: 'AgrMstApplicationInstitutionEdit',
          templateUrl: helper.basepath('ems.mastersamagro/AgrMstApplicationInstitutionEdit.html?ver=' + version + '"'),
          resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

     .state('app.AgrMstApplicationGroupEdit', {
          url: '/AgrMstApplicationGroupEdit',
          title: 'AgrMstApplicationGroupEdit',
          templateUrl: helper.basepath('ems.mastersamagro/AgrMstApplicationGroupEdit.html?ver=' + version + '"'),
          resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

      .state('app.AgrMstApplicationSocialTradeCapitalEdit', {
          url: '/AgrMstApplicationSocialTradeCapitalEdit',
          title: 'AgrMstApplicationSocialTradeCapitalEdit',
          templateUrl: helper.basepath('ems.mastersamagro/AgrMstApplicationSocialTradeCapitalEdit.html?ver=' + version + '"'),
          resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

      .state('app.AgrMstAppEditOverallLimitAdd', {
          url: '/AgrMstAppEditOverallLimitAdd',
          title: 'AgrMstAppEditOverallLimitAdd',
          templateUrl: helper.basepath('ems.mastersamagro/AgrMstAppEditOverallLimitAdd.html?ver=' + version + '"'),
          resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

      .state('app.AgrMstAppEditProductAdd', {
          url: '/AgrMstAppEditProductAdd',
          title: 'AgrMstAppEditProductAdd',
          templateUrl: helper.basepath('ems.mastersamagro/AgrMstAppEditProductAdd.html?ver=' + version + '"'),
          resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

     .state('app.AgrMstAppEditChargeAdd', {
          url: '/AgrMstAppEditChargeAdd',
          title: 'AgrMstAppEditChargeAdd',
          templateUrl: helper.basepath('ems.mastersamagro/AgrMstAppEditChargeAdd.html?ver=' + version + '"'),
          resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

     .state('app.AgrMstAppEditHypothecationAdd', {
          url: '/AgrMstAppEditHypothecationAdd',
          title: 'AgrMstAppEditHypothecationAdd',
          templateUrl: helper.basepath('ems.mastersamagro/AgrMstAppEditHypothecationAdd.html?ver=' + version + '"'),
          resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

      .state('app.AgrMstApplicationEditCICUploadAdd', {
          url: '/AgrMstApplicationEditCICUploadAdd',
          title: 'AgrMstApplicationEditCICUploadAdd',
          templateUrl: helper.basepath('ems.mastersamagro/AgrMstApplicationEditCICUploadAdd.html?ver=' + version + '"'),
          resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })





.state('app.AgrMstApplicationGeneralAdd', {
url: '/AgrMstApplicationGeneralAdd',
title: 'AgrMstApplicationGeneralAdd',
templateUrl: helper.basepath('ems.mastersamagro/AgrMstApplicationGeneralAdd.html?ver=' + version + '"'),
resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
})


.state('app.AgrMstApplicationInstitutionAdd', {
url: '/AgrMstApplicationInstitutionAdd',
title: 'AgrMstApplicationInstitutionAdd',
templateUrl: helper.basepath('ems.mastersamagro/AgrMstApplicationInstitutionAdd.html?ver=' + version + '"'),
resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
})
.state('app.AgrMstApplicationIndividualAdd', {
url: '/AgrMstApplicationIndividualAdd',
title: 'AgrMstApplicationIndividualAdd',
templateUrl: helper.basepath('ems.mastersamagro/AgrMstApplicationIndividualAdd.html?ver=' + version + '"'),
resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
})
.state('app.AgrMstApplicationGroupAdd', {
url: '/AgrMstApplicationGroupAdd',
title: 'AgrMstApplicationGroupAdd',
templateUrl: helper.basepath('ems.mastersamagro/AgrMstApplicationGroupAdd.html?ver=' + version + '"'),
resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
})
.state('app.AgrMstApplicationSocialTradeCapitalAdd', {
url: '/AgrMstApplicationSocialTradeCapitalAdd',
title: 'AgrMstApplicationSocialTradeCapitalAdd',
templateUrl: helper.basepath('ems.mastersamagro/AgrMstApplicationSocialTradeCapitalAdd.html?ver=' + version + '"'),
resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
})




.state('app.AgrMstApplicationOverallLimitAdd', {
url: '/AgrMstApplicationOverallLimitAdd',
title: 'AgrMstApplicationOverallLimitAdd',
templateUrl: helper.basepath('ems.mastersamagro/AgrMstApplicationOverallLimitAdd.html?ver=' + version + '"'),
resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
})


.state('app.AgrMstApplicationProductChargesAdd', {
url: '/AgrMstApplicationProductChargesAdd',
title: 'AgrMstApplicationProductChargesAdd',
templateUrl: helper.basepath('ems.mastersamagro/AgrMstApplicationProductChargesAdd.html?ver=' + version + '"'),
resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
})


.state('app.AgrMstApplicationServiceChargeAdd', {
url: '/AgrMstApplicationServiceChargeAdd',
title: 'AgrMstApplicationServiceChargeAdd',
templateUrl: helper.basepath('ems.mastersamagro/AgrMstApplicationServiceChargeAdd.html?ver=' + version + '"'),
resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
})


.state('app.AgrMstApplicationHypothecationAdd', {
url: '/AgrMstApplicationHypothecationAdd',
title: 'AgrMstApplicationHypothecationAdd',
templateUrl: helper.basepath('ems.mastersamagro/AgrMstApplicationHypothecationAdd.html?ver=' + version + '"'),
resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
})


.state('app.AgrMstApplicationCICUploadAdd', {
url: '/AgrMstApplicationCICUploadAdd',
title: 'AgrMstApplicationCICUploadAdd',
templateUrl: helper.basepath('ems.mastersamagro/AgrMstApplicationCICUploadAdd.html?ver=' + version + '"'),
resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
})

.state('app.AgrMstApplcreationBasicdtlEdit', {
url: '/AgrMstApplcreationBasicdtlEdit',
title: 'AgrMstApplcreationBasicdtlEdit',
templateUrl: helper.basepath('ems.mastersamagro/AgrMstApplcreationBasicdtlEdit.html?ver=' + version + '"'),
resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
})
.state('app.AgrMstApplcreationInstitutiondtlEdit', {
url: '/AgrMstApplcreationInstitutiondtlEdit',
title: 'AgrMstApplcreationInstitutiondtlEdit',
templateUrl: helper.basepath('ems.mastersamagro/AgrMstApplcreationInstitutiondtlEdit.html?ver=' + version + '"'),
resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
})
.state('app.AgrMstApplcreationIndividualdtlEdit', {
url: '/AgrMstApplcreationIndividualdtlEdit',
title: 'AgrMstApplcreationIndividualdtlEdit',
templateUrl: helper.basepath('ems.mastersamagro/AgrMstApplcreationIndividualdtlEdit.html?ver=' + version + '"'),
resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
})




.state('app.AgrMstApplcreationGroupdtlEdit', {
url: '/AgrMstApplcreationGroupdtlEdit',
title: 'AgrMstApplcreationGroupdtlEdit',
templateUrl: helper.basepath('ems.mastersamagro/AgrMstApplcreationGroupdtlEdit.html?ver=' + version + '"'),
resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
})


.state('app.AgrMstApplcreationSocialTradeEdit', {
url: '/AgrMstApplcreationSocialTradeEdit',
title: 'AgrMstApplcreationSocialTradeEdit',
templateUrl: helper.basepath('ems.mastersamagro/AgrMstApplcreationSocialTradeEdit.html?ver=' + version + '"'),
resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
})


.state('app.AgrMstApplcreationProductchargesEdit', {
url: '/AgrMstApplcreationProductchargesEdit',
title: 'AgrMstApplcreationProductchargesEdit',
templateUrl: helper.basepath('ems.mastersamagro/AgrMstApplcreationProductchargesEdit.html?ver=' + version + '"'),
resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
})


.state('app.AgrMstApplicationHypothecationEdit', {
url: '/AgrMstApplicationHypothecationEdit',
title: 'AgrMstApplicationHypothecationEdit',
templateUrl: helper.basepath('ems.mastersamagro/AgrMstApplicationHypothecationEdit.html?ver=' + version + '"'),
resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
})

.state('app.AgrMstBureauUpdateIndividual', {
     url:'/AgrMstBureauUpdateIndividual',
     title:'AgrMstBureauUpdateIndividual',
     templateUrl:helper.basepath('ems.mastersamagro/AgrMstBureauUpdateIndividual.html?ver='+version+'"'),
     resolve:helper.resolveFor('oitozero.ngSweetAlert','ui.select','ngTable','ngDialog','datatables','localytics.directives','taginput','inputmask','textAngular')
    })


    .state('app.AgrMstApplcreationCICUploadView', {
     url:'/AgrMstApplcreationCICUploadView',
     title:'AgrMstApplcreationCICUploadView',
     templateUrl:helper.basepath('ems.mastersamagro/AgrMstApplcreationCICUploadView.html?ver='+version+'"'),
     resolve:helper.resolveFor('oitozero.ngSweetAlert','ui.select','ngTable','ngDialog','datatables','localytics.directives','taginput','inputmask','textAngular')
    })
 .state('app.AgrMstApplcreationCICUploadEdit', {
     url:'/AgrMstApplcreationCICUploadEdit',
     title:'AgrMstApplcreationCICUploadEdit',
     templateUrl:helper.basepath('ems.mastersamagro/AgrMstApplcreationCICUploadEdit.html?ver='+version+'"'),
     resolve:helper.resolveFor('oitozero.ngSweetAlert','ui.select','ngTable','ngDialog','datatables','localytics.directives','taginput','inputmask','textAngular')
    })


    .state('app.AgrMstBureauUpdateInstitution', {
     url:'/AgrMstBureauUpdateInstitution',
     title:'AgrMstBureauUpdateInstitution',
     templateUrl:helper.basepath('ems.mastersamagro/AgrMstBureauUpdateInstitution.html?ver='+version+'"'),
     resolve:helper.resolveFor('oitozero.ngSweetAlert','ui.select','ngTable','ngDialog','datatables','localytics.directives','taginput','inputmask','textAngular')
    })

 .state('app.AgrMstApplcreationCICUploadInstView', {
     url:'/AgrMstApplcreationCICUploadInstView',
     title:'AgrMstApplcreationCICUploadInstView',
     templateUrl:helper.basepath('ems.mastersamagro/AgrMstApplcreationCICUploadInstView.html?ver='+version+'"'),
     resolve:helper.resolveFor('oitozero.ngSweetAlert','ui.select','ngTable','ngDialog','datatables','localytics.directives','taginput','inputmask','textAngular')
    })


    .state('app.AgrMstApplcreationCICUploadInstEdit', {
     url:'/AgrMstApplcreationCICUploadInstEdit',
     title:'AgrMstApplcreationCICUploadInstEdit',
     templateUrl:helper.basepath('ems.mastersamagro/AgrMstApplcreationCICUploadInstEdit.html?ver='+version+'"'),
     resolve:helper.resolveFor('oitozero.ngSweetAlert','ui.select','ngTable','ngDialog','datatables','localytics.directives','taginput','inputmask','textAngular')
    })
  .state('app.AgrMstApplicationLoanEdit', {
     url:'/AgrMstApplicationLoanEdit',
     title:'AgrMstApplicationLoanEdit',
     templateUrl:helper.basepath('ems.mastersamagro/AgrMstApplicationLoanEdit.html?ver='+version+'"'),
     resolve:helper.resolveFor('oitozero.ngSweetAlert','ui.select','ngTable','ngDialog','datatables','localytics.directives','taginput','inputmask','textAngular')
    })


  .state('app.AgrMstCreditQueryStatus', {
    url: '/AgrMstCreditQueryStatus',
    title: 'AgrMstCreditQueryStatus',
    templateUrl: helper.basepath('ems.mastersamagro/AgrMstCreditQueryStatus.html?ver=' + version + '"'),
    resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
    })

  .state('app.AgrGradingToolAdd', {
     url: '/AgrGradingToolAdd',
     title: 'AgrGradingToolAdd',
     templateUrl: helper.basepath('ems.mastersamagro/AgrGradingToolAdd.html?ver=' + version + '"'),
     resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
     })

  .state('app.AgrGradingToolView', {
     url: '/AgrGradingToolView',
     title: 'AgrGradingToolView',
     templateUrl: helper.basepath('ems.mastersamagro/AgrGradingToolView.html?ver=' + version + '"'),
     resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
    })

   .state('app.AgrGradingToolEdit', {
     url: '/AgrGradingToolEdit',
     title: 'AgrGradingToolEdit',
     templateUrl: helper.basepath('ems.mastersamagro/AgrGradingToolEdit.html?ver=' + version + '"'),
     resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
    })

    .state('app.AgrMstSentbackcctoCreditHistory', {
                url: '/AgrMstSentbackcctoCreditHistory',
                title: 'AgrMstSentbackcctoCreditHistory',
                templateUrl: helper.basepath('ems.mastersamagro/AgrMstSentbackcctoCreditHistory.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
            })

   .state('app.AgrMstApplCreationGradingToolView', {
            url: '/AgrMstApplCreationGradingToolView',
            title: 'AgrMstApplCreationGradingToolView',
            templateUrl: helper.basepath('ems.mastersamagro/AgrMstApplCreationGradingToolView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

   .state('app.AgrMstApplCreationVisitReportView', {
        url: '/AgrMstApplCreationVisitReportView',
        title: 'AgrMstApplCreationVisitReportView',
       templateUrl: helper.basepath('ems.mastersamagro/AgrMstApplCreationVisitReportView.html?ver=' + version + '"'),
       resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

   .state('app.AgrTrnCCCommitteeIndividualView', {
        url: '/AgrTrnCCCommitteeIndividualView',
        title: 'AgrTrnCCCommitteeIndividualView',
        templateUrl: helper.basepath('ems.mastersamagro/AgrTrnCCCommitteeIndividualView.html?ver=' + version + '"'),
        resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
       })

    .state('app.AgrCreditAssessedScoreAdd', {
        url: '/AgrCreditAssessedScoreAdd',
        title: 'AgrCreditAssessedScoreAdd',
        templateUrl: helper.basepath('ems.mastersamagro/AgrCreditAssessedScoreAdd.html?ver=' + version + '"'),
        resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

    .state('app.AgrMstCreditVisitReportEdit', {
        url: '/AgrMstCreditVisitReportEdit',
        title: 'AgrMstCreditVisitReportEdit',
        templateUrl: helper.basepath('ems.mastersamagro/AgrMstCreditVisitReportEdit.html?ver=' + version + '"'),
        resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
       })

     .state('app.AgrMstApplCreationInstitutionGuarantorView', {
          url: '/AgrMstApplCreationInstitutionGuarantorView',
         title: 'AgrMstApplCreationInstitutionGuarantorView',
         templateUrl: helper.basepath('ems.mastersamagro/AgrMstApplCreationInstitutionGuarantorView.html?ver=' + version + '"'),
         resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

     .state('app.AgrMstApplCreationIndividualGuarantorView', {
          url: '/AgrMstApplCreationIndividualGuarantorView',
          title: 'AgrMstApplCreationIndividualGuarantorView',
          templateUrl: helper.basepath('ems.mastersamagro/AgrMstApplCreationIndividualGuarantorView.html?ver=' + version + '"'),
         resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
       })

     .state('app.AgrMstApplicationCreationRMApproval', {
         url: '/AgrMstApplicationCreationRMApproval',
	title: 'AgrMstApplicationCreationRMApproval',
	templateUrl: helper.basepath('ems.mastersamagro/AgrMstApplicationCreationRMApproval.html?ver=' + version + '"'),
	resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
     })
.state('app.AgrTrnCreditUnderwritingKycLogView', {
            url: '/AgrTrnCreditUnderwritingKycLogView',
            title: 'AgrTrnCreditUnderwritingKycLogView',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnCreditUnderwritingKycLogView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

.state('app.AgrTrnCAMGenerate', {
            url: '/AgrTrnCAMGenerate',
            title: 'AgrTrnCAMGenerate',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnCAMGenerate.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

.state('app.AgrTrnRMInstitutionView', {
            url: '/AgrTrnRMInstitutionView',
            title: 'AgrTrnRMInstitutionView',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnRMInstitutionView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })


.state('app.AgrTrnRMIndividualView', {
            url: '/AgrTrnRMIndividualView',
            title: 'AgrTrnRMIndividualView',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnRMIndividualView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

.state('app.AgrTrnCreditIndividualDtlView', {
            url: '/AgrTrnCreditIndividualDtlView',
            title: 'AgrTrnCreditIndividualDtlView',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnCreditIndividualDtlView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        
.state('app.AgrTrnCreditCompanyDtlView', {
	url: '/AgrTrnCreditCompanyDtlView',
	title: 'AgrTrnCreditCompanyDtlView',
	templateUrl: helper.basepath('ems.mastersamagro/AgrTrnRMIndividualView.html?ver=' + version + '"'),
	resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
	})
 .state('app.AgrTrnDocumentCheckList', {
            url: '/AgrTrnDocumentCheckList',
            title: 'AgrTrnDocumentCheckList',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnDocumentCheckList.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
.state('app.AgrTrnIndividualDocCheckList', {
            url: '/AgrTrnIndividualDocCheckList',
            title: 'AgrTrnIndividualDocCheckList',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnIndividualDocCheckList.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
.state('app.AgrTrnGroupDocCheckList', {
            url: '/AgrTrnGroupDocCheckList',
            title: 'AgrTrnGroupDocCheckList',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnGroupDocCheckList.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
.state('app.AgrTrnCreditGeneralDtlEdit', {
            url: '/AgrTrnCreditGeneralDtlEdit',
            title: 'AgrTrnCreditGeneralDtlEdit',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnCreditGeneralDtlEdit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

.state('app.AgrTrnCreditInstitutionDtlEdit', {
            url: '/AgrTrnCreditInstitutionDtlEdit',
            title: 'AgrTrnCreditInstitutionDtlEdit',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnCreditInstitutionDtlEdit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

.state('app.AgrTrnCreditGroupDtlEdit', {
            url: '/AgrTrnCreditGroupDtlEdit',
            title: 'AgrTrnCreditGroupDtlEdit',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnCreditGroupDtlEdit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
.state('app.AgrTrnCreditProductChargesDtlEdit', {
            url: '/AgrTrnCreditProductChargesDtlEdit',
            title: 'AgrTrnCreditProductChargesDtlEdit',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnCreditProductChargesDtlEdit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
.state('app.AgrTrnCreditLoanDtlEdit', {
            url: '/AgrTrnCreditLoanDtlEdit',
            title: 'AgrTrnCreditLoanDtlEdit',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnCreditLoanDtlEdit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
.state('app.AgrTrnCreditServicesDtlEdit', {
            url: '/AgrTrnCreditServicesDtlEdit',
            title: 'AgrTrnCreditServicesDtlEdit',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnCreditServicesDtlEdit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
.state('app.AgrTrnCreditHypothecationEdit', {
            url: '/AgrTrnCreditHypothecationEdit',
            title: 'AgrTrnCreditHypothecationEdit',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnCreditHypothecationEdit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.AgrTrnApplGroupMemberdtlView', {
            url: '/AgrTrnApplGroupMemberdtlView',
            title: 'AgrTrnApplGroupMemberdtlView',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnApplGroupMemberdtlView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
.state('app.AgrTrnCreditAddCovenantCheckList', {
            url: '/AgrTrnCreditAddCovenantCheckList',
            title: 'AgrTrnCreditAddCovenantCheckList',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnCreditAddCovenantCheckList.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.AgrTrnCreditEconomicCapitalAdd', {
            url: '/AgrTrnCreditEconomicCapitalAdd',
            title: 'AgrTrnCreditEconomicCapitalAdd',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnCreditEconomicCapitalAdd.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
.state('app.AgrTrnCreditCompanyDtlAdd', {
            url: '/AgrTrnCreditCompanyDtlAdd',
            title: 'AgrTrnCreditCompanyDtlAdd',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnCreditCompanyDtlAdd.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
.state('app.AgrTrnCreditPSLDataFlaggingAdd', {
            url: '/AgrTrnCreditPSLDataFlaggingAdd',
            title: 'AgrTrnCreditPSLDataFlaggingAdd',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnCreditPSLDataFlaggingAdd.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
 .state('app.AgrTrnCreditSuppliersDtlAdd', {
            url: '/AgrTrnCreditSuppliersDtlAdd',
            title: 'AgrTrnCreditSuppliersDtlAdd',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnCreditSuppliersDtlAdd.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
.state('app.AgrTrnCreditBuyerDtlAdd', {
            url: '/AgrTrnCreditBuyerDtlAdd',
            title: 'AgrTrnCreditBuyerDtlAdd',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnCreditBuyerDtlAdd.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
.state('app.AgrTrnCreditBankAccountDtlAdd', {
            url: '/AgrTrnCreditBankAccountDtlAdd',
            title: 'AgrTrnCreditBankAccountDtlAdd',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnCreditBankAccountDtlAdd.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
.state('app.AgrTrnCreditExistingBankDtlAdd', {
            url: '/AgrTrnCreditExistingBankDtlAdd',
            title: 'AgrTrnCreditExistingBankDtlAdd',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnCreditExistingBankDtlAdd.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        
        .state('app.AgrTrnCreditRepaymentDtlAdd', {
            url: '/AgrTrnCreditRepaymentDtlAdd',
            title: 'AgrTrnCreditRepaymentDtlAdd',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnCreditRepaymentDtlAdd.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        
        .state('app.AgrTrnCreditObservationAdd', {
            url: '/AgrTrnCreditObservationAdd',
            title: 'AgrTrnCreditObservationAdd',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnCreditObservationAdd.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        
        .state('app.AgrTrnCreditCompanyAPIAdd', {
            url: '/AgrTrnCreditCompanyAPIAdd',
            title: 'AgrTrnCreditCompanyAPIAdd',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnCreditCompanyAPIAdd.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        
        .state('app.AgrBaseDetailsView', {
            url: '/AgrBaseDetailsView',
            title: 'AgrBaseDetailsView',
            templateUrl: helper.basepath('ems.mastersamagro/AgrBaseDetailsView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        
        .state('app.AgrComprehensiveDetailsView', {
            url: '/AgrComprehensiveDetailsView',
            title: 'AgrComprehensiveDetailsView',
            templateUrl: helper.basepath('ems.mastersamagro/AgrComprehensiveDetailsView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        
        .state('app.AgrTrnCrimeReportCompanyView', {
            url: '/AgrTrnCrimeReportCompanyView',
            title: 'AgrTrnCrimeReportCompanyView',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnCrimeReportCompanyView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        
        .state('app.AgrTrnCreditBankStatementAnalysisAdd', {
            url: '/AgrTrnCreditBankStatementAnalysisAdd',
            title: 'AgrTrnCreditBankStatementAnalysisAdd',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnCreditBankStatementAnalysisAdd.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        
        .state('app.AgrTrnCreditFsaDetailAdd', {
            url: '/AgrTrnCreditFsaDetailAdd',
            title: 'AgrTrnCreditFsaDetailAdd',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnCreditFsaDetailAdd.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        
        .state('app.AgrTrnCreditInstitutionDtlAdd', {
            url: '/AgrTrnCreditInstitutionDtlAdd',
            title: 'AgrTrnCreditInstitutionDtlAdd',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnCreditInstitutionDtlAdd.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.AgrTrnCompanyCrimeCheckRecordAPI', {
            url: '/AgrTrnCompanyCrimeCheckRecordAPI',
            title: 'AgrTrnCompanyCrimeCheckRecordAPI',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnCompanyCrimeCheckRecordAPI.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        
        .state('app.AgrTrnIndividualCovenantDocChecklist', {
            url: '/AgrTrnIndividualCovenantDocChecklist',
            title: 'AgrTrnIndividualCovenantDocChecklist',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnIndividualCovenantDocChecklist.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        
        .state('app.AgrTrnCreditIndividualDtlAdd', {
            url: '/AgrTrnCreditIndividualDtlAdd',
            title: 'AgrTrnCreditIndividualDtlAdd',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnCreditIndividualDtlAdd.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
         
        .state('app.AgrTransUnionReport', {
            url: '/AgrTransUnionReport',
            title: 'AgrTransUnionReport',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTransUnionReport.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.AgrHighmarkReport', {
            url: '/AgrHighmarkReport',
            title: 'AgrHighmarkReport',
            templateUrl: helper.basepath('ems.mastersamagro/AgrHighmarkReport.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        
        .state('app.AgrTrnCreditIndividualBankAcctAdd', {
            url: '/AgrTrnCreditIndividualBankAcctAdd',
            title: 'AgrTrnCreditIndividualBankAcctAdd',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnCreditIndividualBankAcctAdd.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        
        .state('app.AgrTrnCreditIndividualExistingBankAdd', {
            url: '/AgrTrnCreditIndividualExistingBankAdd',
            title: 'AgrTrnCreditIndividualExistingBankAdd',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnCreditIndividualExistingBankAdd.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        
        .state('app.AgrTrnCreditIndividualPSLDataFlagAdd', {
            url: '/AgrTrnCreditIndividualPSLDataFlagAdd',
            title: 'AgrTrnCreditIndividualPSLDataFlagAdd',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnCreditIndividualPSLDataFlagAdd.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        
        .state('app.AgrTrnCreditIndividualRepaymentAdd', {
            url: '/AgrTrnCreditIndividualRepaymentAdd',
            title: 'AgrTrnCreditIndividualRepaymentAdd',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnCreditIndividualRepaymentAdd.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        
        .state('app.AgrTrnCreditIndividualObservationAdd', {
            url: '/AgrTrnCreditIndividualObservationAdd',
            title: 'AgrTrnCreditIndividualObservationAdd',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnCreditIndividualObservationAdd.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        
        .state('app.AgrTrnCreditIndividualBankAcctEdit', {
            url: '/AgrTrnCreditIndividualBankAcctEdit',
            title: 'AgrTrnCreditIndividualBankAcctEdit',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnCreditIndividualBankAcctEdit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        
        .state('app.AgrTrnCreditIndividualAPI', {
            url: '/AgrTrnCreditIndividualAPI',
            title: 'AgrTrnCreditIndividualAPI',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnCreditIndividualAPI.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        
        .state('app.AgrTrnCrimeReportIndividualView', {
            url: '/AgrTrnCrimeReportIndividualView',
            title: 'AgrTrnCrimeReportIndividualView',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnCrimeReportIndividualView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        
        .state('app.AgrTrnCreditIndividualBankStatementAnalysisAdd', {
            url: '/AgrTrnCreditIndividualBankStatementAnalysisAdd',
            title: 'AgrTrnCreditIndividualBankStatementAnalysisAdd',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnCreditIndividualBankStatementAnalysisAdd.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        
        .state('app.AgrTrnCreditCrimeCheckRecordAPI', {
            url: '/AgrTrnCreditCrimeCheckRecordAPI',
            title: 'AgrTrnCreditCrimeCheckRecordAPI',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnCreditCrimeCheckRecordAPI.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

       .state('app.AgrTrnGroupCovenantDocChecklist', {
            url: '/AgrTrnGroupCovenantDocChecklist',
            title: 'AgrTrnGroupCovenantDocChecklist',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnGroupCovenantDocChecklist.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })   
        
        .state('app.AgrTrnCreditGroupDtlAdd', {
            url: '/AgrTrnCreditGroupDtlAdd',
            title: 'AgrTrnCreditGroupDtlAdd',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnCreditGroupDtlAdd.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        
        .state('app.AgrTrnCreditGroupBankAcctAdd', {
            url: '/AgrTrnCreditGroupBankAcctAdd',
            title: 'AgrTrnCreditGroupBankAcctAdd',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnCreditGroupBankAcctAdd.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        
        .state('app.AgrTrnCreditGroupExistingBankAdd', {
            url: '/AgrTrnCreditGroupExistingBankAdd',
            title: 'AgrTrnCreditGroupExistingBankAdd',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnCreditGroupExistingBankAdd.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        
        .state('app.AgrTrnCreditGroupPSLDataFlagAdd', {
            url: '/AgrTrnCreditGroupPSLDataFlagAdd',
            title: 'AgrTrnCreditGroupPSLDataFlagAdd',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnCreditGroupPSLDataFlagAdd.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        
        .state('app.AgrTrnCreditGroupRepaymentAdd', {
            url: '/AgrTrnCreditGroupRepaymentAdd',
            title: 'AgrTrnCreditGroupRepaymentAdd',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnCreditGroupRepaymentAdd.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        
        .state('app.AgrTrnCreditGroupObservationAdd', {
            url: '/AgrTrnCreditGroupObservationAdd',
            title: 'AgrTrnCreditGroupObservationAdd',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnCreditGroupObservationAdd.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        
        .state('app.AgrTrnCreditGroupBankStatementAnalysisAdd', {
            url: '/AgrTrnCreditGroupBankStatementAnalysisAdd',
            title: 'AgrTrnCreditGroupBankStatementAnalysisAdd',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnCreditGroupBankStatementAnalysisAdd.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

	    .state('app.AgrTrnCreditApproval', {
            url: '/AgrTrnCreditApproval',
            title: 'AgrTrnCreditApproval',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnCreditApproval.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 	'textAngular')
        })

 	    .state('app.AgrMstCcCommitteeApplicationView', {
            url: '/AgrMstCcCommitteeApplicationView',
            title: 'AgrMstCcCommitteeApplicationView',
            templateUrl: helper.basepath('ems.mastersamagro/AgrMstCcCommitteeApplicationView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.AgrMstCcCommitteeKycView', {
                url: '/AgrMstCcCommitteeKycView',
                title: 'AgrMstCcCommitteeKycView',
                templateUrl: helper.basepath('ems.mastersamagro/AgrMstCcCommitteeKycView.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.AgrMstCcCommitteeInstitutionView', {
            url: '/AgrMstCcCommitteeInstitutionView',
            title: 'AgrMstCcCommitteeInstitutionView',
            templateUrl: helper.basepath('ems.mastersamagro/AgrMstCcCommitteeInstitutionView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

          .state('app.AgrMstCcCommitteeGroupView', {
            url: '/AgrMstCcCommitteeGroupView',
            title: 'AgrMstCcCommitteeGroupView',
            templateUrl: helper.basepath('ems.mastersamagro/AgrMstCcCommitteeGroupView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.AgrMstCcCommitteeIndividualView', {
            url: '/AgrMstCcCommitteeIndividualView',
            title: 'AgrMstCcCommitteeIndividualView',
            templateUrl: helper.basepath('ems.mastersamagro/AgrMstCcCommitteeIndividualView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
          
        .state('app.AgrHighmarkInstitutionReport', {
            url: '/AgrHighmarkInstitutionReport',
            title: 'AgrHighmarkInstitutionReport',
            templateUrl: helper.basepath('ems.mastersamagro/AgrHighmarkInstitutionReport.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        
        .state('app.AgrTrnCreditInstitutionBureauView', {
            url: '/AgrTrnCreditInstitutionBureauView',
            title: 'AgrTrnCreditInstitutionBureauView',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnCreditInstitutionBureauView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        
        .state('app.AgrTrnCreditInstitutionBureauEdit', {
            url: '/AgrTrnCreditInstitutionBureauEdit',
            title: 'AgrTrnCreditInstitutionBureauEdit',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnCreditInstitutionBureauEdit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        
        .state('app.AgrTrnCreditEconomicCapitalEdit', {
            url: '/AgrTrnCreditEconomicCapitalEdit',
            title: 'AgrTrnCreditEconomicCapitalEdit',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnCreditEconomicCapitalEdit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        
        .state('app.AgrTrnCreditPSLDataFlaggingEdit', {
            url: '/AgrTrnCreditPSLDataFlaggingEdit',
            title: 'AgrTrnCreditPSLDataFlaggingEdit',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnCreditPSLDataFlaggingEdit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        
        .state('app.AgrTrnCreditSuppliersDtlEdit', {
            url: '/AgrTrnCreditSuppliersDtlEdit',
            title: 'AgrTrnCreditSuppliersDtlEdit',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnCreditSuppliersDtlEdit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        
        .state('app.AgrTrnCreditBuyerDtlEdit', {
            url: '/AgrTrnCreditBuyerDtlEdit',
            title: 'AgrTrnCreditBuyerDtlEdit',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnCreditBuyerDtlEdit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        
        .state('app.AgrTrnCreditBankAccountDtlEdit', {
            url: '/AgrTrnCreditBankAccountDtlEdit',
            title: 'AgrTrnCreditBankAccountDtlEdit',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnCreditBankAccountDtlEdit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        
        .state('app.AgrTrnCreditExistingBankDtlEdit', {
            url: '/AgrTrnCreditExistingBankDtlEdit',
            title: 'AgrTrnCreditExistingBankDtlEdit',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnCreditExistingBankDtlEdit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        
        .state('app.AgrTrnCreditRepaymentDtlEdit', {
            url: '/AgrTrnCreditRepaymentDtlEdit',
            title: 'AgrTrnCreditRepaymentDtlEdit',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnCreditRepaymentDtlEdit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        
        .state('app.AgrTrnCreditFSAView', {
            url: '/AgrTrnCreditFSAView',
            title: 'AgrTrnCreditFSAView',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnCreditFSAView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.AgrTrnCreditIndividualDtlEdit', {
            url: '/AgrTrnCreditIndividualDtlEdit',
            title: 'AgrTrnCreditIndividualDtlEdit',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnCreditIndividualDtlEdit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        
        .state('app.AgrTrnCreditIndividualBureauView', {
            url: '/AgrTrnCreditIndividualBureauView',
            title: 'AgrTrnCreditIndividualBureauView',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnCreditIndividualBureauView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        
        .state('app.AgrTrnCreditIndividualBureauEdit', {
            url: '/AgrTrnCreditIndividualBureauEdit',
            title: 'AgrTrnCreditIndividualBureauEdit',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnCreditIndividualBureauEdit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        
        .state('app.AgrTrnCreditIndividualExistingBankEdit', {
            url: '/AgrTrnCreditIndividualExistingBankEdit',
            title: 'AgrTrnCreditIndividualExistingBankEdit',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnCreditIndividualExistingBankEdit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.AgrTrnCreditIndividualRepaymentEdit', {
            url: '/AgrTrnCreditIndividualRepaymentEdit',
            title: 'AgrTrnCreditIndividualRepaymentEdit',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnCreditIndividualRepaymentEdit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        
        .state('app.AgrTrnCreditIndividualPSLDataFlagEdit', {
            url: '/AgrTrnCreditIndividualPSLDataFlagEdit',
            title: 'AgrTrnCreditIndividualPSLDataFlagEdit',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnCreditIndividualPSLDataFlagEdit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        
        .state('app.AgrTrnCreditGroupBankAcctEdit', {
            url: '/AgrTrnCreditGroupBankAcctEdit',
            title: 'AgrTrnCreditGroupBankAcctEdit',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnCreditGroupBankAcctEdit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        
        .state('app.AgrTrnCreditGroupExistingBankEdit', {
            url: '/AgrTrnCreditGroupExistingBankEdit',
            title: 'AgrTrnCreditGroupExistingBankEdit',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnCreditGroupExistingBankEdit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        
        .state('app.AgrTrnCreditGroupRepaymentEdit', {
            url: '/AgrTrnCreditGroupRepaymentEdit',
            title: 'AgrTrnCreditGroupRepaymentEdit',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnCreditGroupRepaymentEdit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        
        .state('app.AgrTrnCreditGroupPSLDataFlagEdit', {
            url: '/AgrTrnCreditGroupPSLDataFlagEdit',
            title: 'AgrTrnCreditGroupPSLDataFlagEdit',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnCreditGroupPSLDataFlagEdit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        
        .state('app.AgrMstBusinessHoldSummary', {
            url: '/AgrMstBusinessHoldSummary',
            title: 'AgrMstBusinessHoldSummary',
            templateUrl: helper.basepath('ems.mastersamagro/AgrMstBusinessHoldSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.AgrMstBusinessApprovedSummary', {
            url: '/AgrMstBusinessApprovedSummary',
            title: 'AgrMstBusinessApprovedSummary',
            templateUrl: helper.basepath('ems.mastersamagro/AgrMstBusinessApprovedSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.AgrMstBusinessApproval', {
            url: '/AgrMstBusinessApproval',
            title: 'AgrMstBusinessApproval',
            templateUrl: helper.basepath('ems.mastersamagro/AgrMstBusinessApproval.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.AgrMstBusinessApprovalSummary', {
            url: '/AgrMstBusinessApprovalSummary',
            title: 'AgrMstBusinessApprovalSummary',
            templateUrl: helper.basepath('ems.mastersamagro/AgrMstBusinessApprovalSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.AgrMstBusinessRejectedSummary', {
            url: '/AgrMstBusinessRejectedSummary',
            title: 'AgrMstBusinessRejectedSummary',
            templateUrl: helper.basepath('ems.mastersamagro/AgrMstBusinessRejectedSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.AgrTrnStartCreditUnderwriting', {
            url: '/AgrTrnStartCreditUnderwriting',
            title: 'AgrTrnStartCreditUnderwriting',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnStartCreditUnderwriting.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.AgrTrnCADApplicationEdit', {
            url: '/AgrTrnCADApplicationEdit',
            title: 'AgrTrnCADApplicationEdit',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnCADApplicationEdit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.AgrTrnCADGroupProcessAssign', {
            url: '/AgrTrnCADGroupProcessAssign',
            title: 'AgrTrnCADGroupProcessAssign',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnCADGroupProcessAssign.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.AgrTrnCADReassignApplication', {
            url: '/AgrTrnCADReassignApplication',
            title: 'AgrTrnCADReassignApplication',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnCADReassignApplication.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.AgrMstCADInstitutionDtlAdd', {
            url: '/AgrMstCADInstitutionDtlAdd',
            title: 'AgrMstCADInstitutionDtlAdd',
            templateUrl: helper.basepath('ems.mastersamagro/AgrMstCADInstitutionDtlAdd.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.AgrMstCADIndividualDtlAdd', {
            url: '/AgrMstCADIndividualDtlAdd',
            title: 'AgrMstCADIndividualDtlAdd',
            templateUrl: helper.basepath('ems.mastersamagro/AgrMstCADIndividualDtlAdd.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.AgrMstCADGroupDtlAdd', {
            url: '/AgrMstCADGroupDtlAdd',
            title: 'AgrMstCADGroupDtlAdd',
            templateUrl: helper.basepath('ems.mastersamagro/AgrMstCADGroupDtlAdd.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.AgrTrnCreditGroupDtlView', {
            url: '/AgrTrnCreditGroupDtlView',
            title: 'AgrTrnCreditGroupDtlView',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnCreditGroupDtlView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.AgrTrnCadPhysicalDocGuarantorDtls', {
            url: '/AgrTrnCadPhysicalDocGuarantorDtls',
            title: 'AgrTrnCadPhysicalDocGuarantorDtls',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnCadPhysicalDocGuarantorDtls.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.AgrTrnCadPhysicalDochecklist', {
           url: '/AgrTrnCadPhysicalDochecklist',
           title: 'AgrTrnCadPhysicalDochecklist',
           templateUrl: helper.basepath('ems.mastersamagro/AgrTrnCadPhysicalDochecklist.html?ver=' + version + '"'),
           resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.AgrTrnPhysicalDeferralHistory', {
           url: '/AgrTrnPhysicalDeferralHistory',
           title: 'AgrTrnPhysicalDeferralHistory',
           templateUrl: helper.basepath('ems.mastersamagro/AgrTrnPhysicalDeferralHistory.html?ver=' + version + '"'),
           resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.AgrTrnCadPhysicalDocQuery', {
           url: '/AgrTrnCadPhysicalDocQuery',
           title: 'AgrTrnCadPhysicalDocQuery',
           templateUrl: helper.basepath('ems.mastersamagro/AgrTrnCadPhysicalDocQuery.html?ver=' + version + '"'),
           resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.AgrTrnCadPhysicalDocStatus', {
           url: '/AgrTrnCadPhysicalDocStatus',
           title: 'AgrTrnCadPhysicalDocStatus',
           templateUrl: helper.basepath('ems.mastersamagro/AgrTrnCadPhysicalDocStatus.html?ver=' + version + '"'),
           resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
        
        .state('app.AgrTrnDocChecklistApprovalCompleted', {
            url: '/AgrTrnDocChecklistApprovalCompleted',
            title: 'AgrTrnDocChecklistApprovalCompleted',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnDocChecklistApprovalCompleted.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.AgrTrnCadGuarantorApproval', {
            url: '/AgrTrnCadGuarantorApproval',
            title: 'AgrTrnCadGuarantorApproval',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnCadGuarantorApproval.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

       .state('app.AgrTrnDeferralMyApproval', {
            url: '/AgrTrnDeferralMyApproval',
            title: 'AgrTrnDeferralMyApproval',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnDeferralMyApproval.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.AgrTrnDeferralMyApprovalHistory', {
            url: '/AgrTrnDeferralMyApprovalHistory',
            title: 'AgrTrnDeferralMyApprovalHistory',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnDeferralMyApprovalHistory.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.AgrTrnPostCcActivitiesRMView', {
            url: '/AgrTrnPostCcActivitiesRMView',
            title: 'AgrTrnPostCcActivitiesRMView',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnPostCcActivitiesRMView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        
        .state('app.AgrTrnRMDeferralDtls', {
            url: '/AgrTrnRMDeferralDtls',
            title: 'AgrTrnRMDeferralDtls',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnRMDeferralDtls.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.AgrTrnRMDeferralDtlsView', {
            url: '/AgrTrnRMDeferralDtlsView',
            title: 'AgrTrnRMDeferralDtlsView',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnRMDeferralDtlsView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.AgrTrnRMDeferralCloseQuery', {
            url: '/AgrTrnRMDeferralCloseQuery',
            title: 'AgrTrnRMDeferralCloseQuery',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnRMDeferralCloseQuery.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
        
        .state('page.AgrCCMeetingApproval', {
            url: '/AgrCCMeetingApproval',
            title: 'AgrCCMeetingApproval',
            templateUrl: 'app/pages/AgrCCMeetingApproval.html?ver=' + version + '"',
        })

        .state('app.AgrMstApplicationReport', {
            url: '/AgrMstApplicationReport',
            title: 'AgrMstApplicationReport',
            templateUrl: helper.basepath('ems.mastersamagro/AgrMstApplicationReport.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.AgrMstCreditAllocationReport', {
            url: '/AgrMstCreditAllocationReport',
            title: 'AgrMstCreditAllocationReport',
            templateUrl: helper.basepath('ems.mastersamagro/AgrMstCreditAllocationReport.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.AgrMstCCReport', {
            url: '/AgrMstCCReport',
            title: 'AgrMstCCReport',
            templateUrl: helper.basepath('ems.mastersamagro/AgrMstCCReport.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

         .state('app.AgrMstWarehouseFacility', {
             url: '/AgrMstWarehouseFacility',
             title: 'AgrMstWarehouseFacility',
             templateUrl: helper.basepath('ems.mastersamagro/AgrMstWarehouseFacility.html?ver=' + version + '"'),
             resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
         })

           .state('app.AgrMstWarehouseCreationSummary', {
               url: '/AgrMstWarehouseCreationSummary',
               title: 'AgrMstWarehouseCreationSummary',
               templateUrl: helper.basepath('ems.mastersamagro/AgrMstWarehouseCreationSummary.html?ver=' + version + '"'),
               resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
           })

        .state('app.AgrMstWarehouseAdd', {
            url: '/AgrMstWarehouseAdd',
            title: 'AgrMstWarehouseAdd',
            templateUrl: helper.basepath('ems.mastersamagro/AgrMstWarehouseAdd.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })


        .state('app.AgrMstWarehouseEdit', {
            url: '/AgrMstWarehouseEdit',
            title: 'AgrMstWarehouseEdit',
            templateUrl: helper.basepath('ems.mastersamagro/AgrMstWarehouseEdit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.AgrMstPmgApproval', {
            url: '/AgrMstPmgApproval',
            title: 'AgrMstPmgApproval',
            templateUrl: helper.basepath('ems.mastersamagro/AgrMstPmgApproval.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })



        .state('app.AgrMstProductApproval', {
            url: '/AgrMstProductApproval',
            title: 'AgrMstProductApproval',
            templateUrl: helper.basepath('ems.mastersamagro/AgrMstProductApproval.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })


        .state('app.AgrMstWarehouseAprovalSummary', {
            url: '/AgrMstWarehouseAprovalSummary',
            title: 'AgrMstWarehouseAprovalSummary',
            templateUrl: helper.basepath('ems.mastersamagro/AgrMstWarehouseAprovalSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.AgrMstPendingPmgApproval', {
            url: '/AgrMstPendingPmgApproval',
            title: 'AgrMstPendingPmgApproval',
            templateUrl: helper.basepath('ems.mastersamagro/AgrMstPendingPmgApproval.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })



        .state('app.AgrMstPendingProductApproval', {
            url: '/AgrMstPendingProductApproval',
            title: 'AgrMstPendingProductApproval',
            templateUrl: helper.basepath('ems.mastersamagro/AgrMstPendingProductApproval.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.AgrMstWarehouseDtlApproval', {
            url: '/AgrMstWarehouseDtlApproval',
            title: 'AgrMstWarehouseDtlApproval',
            templateUrl: helper.basepath('ems.mastersamagro/AgrMstWarehouseDtlApproval.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.AgrMstRejectedWarehouses', {
            url: '/AgrMstRejectedWarehouses',
            title: 'AgrMstRejectedWarehouses',
            templateUrl: helper.basepath('ems.mastersamagro/AgrMstRejectedWarehouses.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })


        .state('app.AgrMstApprovalPendingWarehouseSummary', {
            url: '/AgrMstApprovalPendingWarehouseSummary',
            title: 'AgrMstApprovalPendingWarehouseSummary',
            templateUrl: helper.basepath('ems.mastersamagro/AgrMstApprovalPendingWarehouseSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.AgrMstMyApprovalWarehouseSummary', {
            url: '/AgrMstApprovalPendingWarehouseSummary',
            title: 'AgrMstApprovalPendingWarehouseSummary',
            templateUrl: helper.basepath('ems.mastersamagro/AgrMstApprovalPendingWarehouseSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.AgrMstMyRejectedWarehouseSummary', {
            url: '/AgrMstApprovalPendingWarehouseSummary',
            title: 'AgrMstApprovalPendingWarehouseSummary',
            templateUrl: helper.basepath('ems.mastersamagro/AgrMstApprovalPendingWarehouseSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.AgrMstUOM', {
            url: '/AgrMstUOM',
            title: 'AgrMstUOM',
            templateUrl: helper.basepath('ems.mastersamagro/AgrMstUOM.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

         .state('app.AgrMstScope', {
             url: '/AgrMstScope',
             title: 'AgrMstScope',
             templateUrl: helper.basepath('ems.mastersamagro/AgrMstScope.html?ver=' + version + '"'),
             resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
         })

        .state('app.AgrMstOtherCreditorApplicantType', {
            url: '/AgrMstOtherCreditorApplicantType',
            title: 'AgrMstOtherCreditorApplicantType',
            templateUrl: helper.basepath('ems.mastersamagro/AgrMstOtherCreditorApplicantType.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.AgrMstNatureFormStateofCommodity', {
            url: '/AgrMstNatureFormStateofCommodity',
            title: 'AgrMstNatureFormStateofCommodity',
            templateUrl: helper.basepath('ems.mastersamagro/AgrMstNatureFormStateofCommodity.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.AgrMstMilestonePaymentType', {
            url: '/AgrMstMilestonePaymentType',
            title: 'AgrMstMilestonePaymentType',
            templateUrl: helper.basepath('ems.mastersamagro/AgrMstMilestonePaymentType.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

          .state('app.AgrMstCreditorMasterSummary', {
              url: '/AgrMstCreditorMasterSummary',
              title: 'AgrMstCreditorMasterSummary',
              templateUrl: helper.basepath('ems.mastersamagro/AgrMstCreditorMasterSummary.html?ver=' + version + '"'),
              resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
          })

        .state('app.AgrMstCreditorMasterAdd', {
            url: '/AgrMstCreditorMasterAdd',
            title: 'AgrMstCreditorMasterAdd',
            templateUrl: helper.basepath('ems.mastersamagro/AgrMstCreditorMasterAdd.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.AgrMstCreditorMasterView', {
            url: '/AgrMstCreditorMasterView',
            title: 'AgrMstCreditorMasterView',
            templateUrl: helper.basepath('ems.mastersamagro/AgrMstCreditorMasterView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.AgrMstCreditorMasterEdit', {
            url: '/AgrMstCreditorMasterEdit',
            title: 'AgrMstCreditorMasterEdit',
            templateUrl: helper.basepath('ems.mastersamagro/AgrMstCreditorMasterEdit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.AgrMstCreditorMasterChequeEdit', {
            url: '/AgrMstCreditorMasterChequeEdit',
            title: 'AgrMstCreditorMasterChequeEdit',
            templateUrl: helper.basepath('ems.mastersamagro/AgrMstCreditorMasterChequeEdit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.AgrMstAppEditTradeAdd', {
             url: '/AgrMstAppEditTradeAdd',
             title: 'AgrMstAppEditTradeAdd',
             templateUrl: helper.basepath('ems.mastersamagro/AgrMstAppEditTradeAdd.html?ver=' + version + '"'),
             resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.AgrTrnSuprCreditApproval', {
             url: '/AgrTrnSuprCreditApproval',
             title: 'AgrTrnSuprCreditApproval',
             templateUrl: helper.basepath('ems.mastersamagro/AgrTrnSuprCreditApproval.html?ver=' + version + '"'),
             resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.AgrTrnSuprCreditApprovedSummary', {
            url: '/AgrTrnSuprCreditApprovedSummary',
            title: 'AgrTrnSuprCreditApprovedSummary',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnSuprCreditApprovedSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.AgrTrnSuprCreditSubmittedtoCCSummary', {
            url: '/AgrTrnSuprCreditSubmittedtoCCSummary',
            title: 'AgrTrnSuprCreditSubmittedtoCCSummary',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnSuprCreditSubmittedtoCCSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.AgrTrnSuprCreditCCSkippedSummary', {
            url: '/AgrTrnSuprCreditCCSkippedSummary',
            title: 'AgrTrnSuprCreditCCSkippedSummary',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnSuprCreditCCSkippedSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.AgrTrnSuprCreditRejectandHoldSummary', {
            url: '/AgrTrnSuprCreditRejectandHoldSummary',
            title: 'AgrTrnSuprCreditRejectandHoldSummary',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnSuprCreditRejectandHoldSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.AgrTrnSuprCCscheduledSummary', {
            url: '/AgrTrnSuprCCscheduledSummary',
            title: 'AgrTrnSuprCCscheduledSummary',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnSuprCCscheduledSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'textAngular', 'jquery-ui', 'jquery-ui-widgets', 'moment', 'fullcalendar')
        })

        .state('app.AgrTrnSuprCCCompletedSummary', {
            url: '/AgrTrnSuprCCCompletedSummary',
            title: 'AgrTrnSuprCCCompletedSummary',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnSuprCCCompletedSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.AgrTrnSuprSentbackcctoCredit', {
            url: '/AgrTrnSuprSentbackcctoCredit',
            title: 'AgrTrnSuprSentbackcctoCredit',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnSuprSentbackcctoCredit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.AgrTrnSuprCcCompletedScheduledMeeting', {
            url: '/AgrTrnSuprCcCompletedScheduledMeeting',
            title: 'AgrTrnSuprCcCompletedScheduledMeeting',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnSuprCcCompletedScheduledMeeting.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.AgrTrnSuprSentbackCadtoCcHistory', {
            url: '/AgrTrnSuprSentbackCadtoCcHistory',
            title: 'AgrTrnSuprSentbackCadtoCcHistory',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnSuprSentbackCadtoCcHistory.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.AgrTrnSuprCCMeetingSchedule', {
            url: '/AgrTrnSuprCCMeetingSchedule',
            title: 'AgrTrnSuprCCMeetingSchedule',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnSuprCCMeetingSchedule.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

      .state('app.AgrTrnSuprCCMeetingReschedule', {
          url: '/AgrTrnSuprCCMeetingReschedule',
          title: 'AgrTrnSuprCCMeetingReschedule',
          templateUrl: helper.basepath('ems.mastersamagro/AgrTrnSuprCCMeetingReschedule.html?ver=' + version + '"'),
          resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
      })

      .state('app.AgrTrnSuprCcScheduledMeetingDtlView', {
          url: '/AgrTrnSuprCcScheduledMeetingDtlView',
          title: 'AgrTrnSuprCcScheduledMeetingDtlView',
          templateUrl: helper.basepath('ems.mastersamagro/AgrTrnSuprCcScheduledMeetingDtlView.html?ver=' + version + '"'),
          resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
      })

      .state('app.AgrTrnSuprStartScheduledMeeting', {
          url: '/AgrTrnSuprStartScheduledMeeting',
          title: 'AgrTrnSuprStartScheduledMeeting',
          templateUrl: helper.basepath('ems.mastersamagro/AgrTrnSuprStartScheduledMeeting.html?ver=' + version + '"'),
          resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
      })

         // Prem

    .state('app.AgrSuprApplicationAssignmentSummary', {
        url: '/AgrSuprApplicationAssignmentSummary',
        title: 'AgrSuprApplicationAssignmentSummary',
        templateUrl: helper.basepath('ems.mastersamagro/AgrSuprApplicationAssignmentSummary.html?ver=' + version + '"'),
        resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
    })

     .state('app.AgrMstSuprMyApplicationsSummary', {
         url: '/AgrMstSuprMyApplicationsSummary',
         title: 'AgrMstSuprMyApplicationsSummary',
         templateUrl: helper.basepath('ems.mastersamagro/AgrMstSuprMyApplicationsSummary.html?ver=' + version + '"'),
         resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
     })

     .state('app.AgrTrnSuprCreditApprovalSummary', {
         url: '/AgrTrnSuprCreditApprovalSummary',
         title: 'AgrTrnSuprCreditApprovalSummary',
         templateUrl: helper.basepath('ems.mastersamagro/AgrTrnSuprCreditApprovalSummary.html?ver=' + version + '"'),
         resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
     })

     .state('app.AgrTrnSuprCcScheduledMeetingSummary', {
         url: '/AgrTrnSuprCcScheduledMeetingSummary',
         title: 'AgrTrnSuprCcScheduledMeetingSummary',
         templateUrl: helper.basepath('ems.mastersamagro/AgrTrnSuprCcScheduledMeetingSummary.html?ver=' + version + '"'),
         resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'textAngular', 'jquery-ui', 'jquery-ui-widgets', 'moment', 'fullcalendar')
     })

     .state('app.AgrTrnSuprCreditCommitteeSummary', {
         url: '/AgrTrnSuprCreditCommitteeSummary',
         title: 'AgrTrnSuprCreditCommitteeSummary',
         templateUrl: helper.basepath('ems.mastersamagro/AgrTrnSuprCreditCommitteeSummary.html?ver=' + version + '"'),
         resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
     })

     .state('app.AgrSuprAppassignedAssignmentSummary', {
         url: '/AgrSuprAppassignedAssignmentSummary',
         title: 'AgrSuprAppassignedAssignmentSummary',
         templateUrl: helper.basepath('ems.mastersamagro/AgrSuprAppassignedAssignmentSummary.html?ver=' + version + '"'),
         resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
     })

     .state('app.AgrSuprApplSubmittedtoCCSummary', {
         url: '/AgrSuprApplSubmittedtoCCSummary',
         title: 'AgrSuprApplSubmittedtoCCSummary',
         templateUrl: helper.basepath('ems.mastersamagro/AgrSuprApplSubmittedtoCCSummary.html?ver=' + version + '"'),
         resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
     })

     .state('app.AgrMstSuprCreditAssessedScoreAdd', {
         url: '/AgrMstSuprCreditAssessedScoreAdd',
         title: 'AgrMstSuprCreditAssessedScoreAdd',
         templateUrl: helper.basepath('ems.mastersamagro/AgrMstSuprCreditAssessedScoreAdd.html?ver=' + version + '"'),
         resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
     })

     .state('app.AgrMstSuprCreditAssessedScoreEdit', {
         url: '/AgrMstSuprCreditAssessedScoreEdit',
         title: 'AgrMstSuprCreditAssessedScoreEdit',
         templateUrl: helper.basepath('ems.mastersamagro/AgrMstSuprCreditAssessedScoreEdit.html?ver=' + version + '"'),
         resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
     })

     .state('app.AgrSuprCreditVisitReportAdd', {
         url: '/AgrSuprCreditVisitReportAdd',
         title: 'AgrSuprCreditVisitReportAdd',
         templateUrl: helper.basepath('ems.mastersamagro/AgrSuprCreditVisitReportAdd.html?ver=' + version + '"'),
         resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
     })

     .state('app.AgrTrnSuprStartCreditUnderwriting', {
         url: '/AgrTrnSuprStartCreditUnderwriting',
         title: 'AgrTrnSuprStartCreditUnderwriting',
         templateUrl: helper.basepath('ems.mastersamagro/AgrTrnSuprStartCreditUnderwriting.html?ver=' + version + '"'),
         resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
     })

     .state('app.AgrTrnSuprRMInstitutionView', {
         url: '/AgrTrnSuprRMInstitutionView',
         title: 'AgrTrnSuprRMInstitutionView',
         templateUrl: helper.basepath('ems.mastersamagro/AgrTrnSuprRMInstitutionView.html?ver=' + version + '"'),
         resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
     })

     .state('app.AgrTrnSuprRMIndividualView', {
         url: '/AgrTrnSuprRMIndividualView',
         title: 'AgrTrnSuprRMIndividualView',
         templateUrl: helper.basepath('ems.mastersamagro/AgrTrnSuprRMIndividualView.html?ver=' + version + '"'),
         resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
     })

     .state('app.AgrTrnSuprCreditCompanyDtlView', {
         url: '/AgrTrnSuprCreditCompanyDtlView',
         title: 'AgrTrnSuprCreditCompanyDtlView',
         templateUrl: helper.basepath('ems.mastersamagro/AgrTrnSuprCreditCompanyDtlView.html?ver=' + version + '"'),
         resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
     })

     .state('app.AgrTrnSuprCreditIndividualDtlView', {
         url: '/AgrTrnSuprCreditIndividualDtlView',
         title: 'AgrTrnSuprCreditIndividualDtlView',
         templateUrl: helper.basepath('ems.mastersamagro/AgrTrnSuprCreditIndividualDtlView.html?ver=' + version + '"'),
         resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
     })

     .state('app.AgrTrnSuprApplCreationGradingToolView', {
         url: '/AgrTrnSuprApplCreationGradingToolView',
         title: 'AgrTrnSuprApplCreationGradingToolView',
         templateUrl: helper.basepath('ems.mastersamagro/AgrTrnSuprApplCreationGradingToolView.html?ver=' + version + '"'),
         resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
     })

     .state('app.AgrTrnSuprApplCreationVisitReportView', {
         url: '/AgrTrnSuprApplCreationVisitReportView',
         title: 'AgrTrnSuprApplCreationVisitReportView',
         templateUrl: helper.basepath('ems.mastersamagro/AgrTrnSuprApplCreationVisitReportView.html?ver=' + version + '"'),
         resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
     })

     .state('app.AgrMstSuprCreditQueryStatus', {
         url: '/AgrMstSuprCreditQueryStatus',
         title: 'AgrMstSuprCreditQueryStatus',
         templateUrl: helper.basepath('ems.mastersamagro/AgrMstSuprCreditQueryStatus.html?ver=' + version + '"'),
         resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
     })

     .state('app.AgrMstSuprSubmittedtoApprovalSummary', {
         url: '/AgrMstSuprSubmittedtoApprovalSummary',
         title: 'AgrMstSuprSubmittedtoApprovalSummary',
         templateUrl: helper.basepath('ems.mastersamagro/AgrMstSuprSubmittedtoApprovalSummary.html?ver=' + version + '"'),
         resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
     })

     .state('app.AgrMstSuprSubmittedtoCCSummary', {
         url: '/AgrMstSuprSubmittedtoCCSummary',
         title: 'AgrMstSuprSubmittedtoCCSummary',
         templateUrl: helper.basepath('ems.mastersamagro/AgrMstSuprSubmittedtoCCSummary.html?ver=' + version + '"'),
         resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
     })

     .state('app.AgrMstSuprCCSkippedApplicationSummary', {
         url: '/AgrMstSuprCCSkippedApplicationSummary',
         title: 'AgrMstSuprCCSkippedApplicationSummary',
         templateUrl: helper.basepath('ems.mastersamagro/AgrMstSuprCCSkippedApplicationSummary.html?ver=' + version + '"'),
         resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
     })

     .state('app.AgrMstSuprRejectandHoldSummary', {
         url: '/AgrMstSuprRejectandHoldSummary',
         title: 'AgrMstSuprRejectandHoldSummary',
         templateUrl: helper.basepath('ems.mastersamagro/AgrMstSuprRejectandHoldSummary.html?ver=' + version + '"'),
         resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
     })

     .state('app.AgrMstSuprSentbackcctoCreditHistory', {
         url: '/AgrMstSuprSentbackcctoCreditHistory',
         title: 'AgrMstSuprSentbackcctoCreditHistory',
         templateUrl: helper.basepath('ems.mastersamagro/AgrMstSuprSentbackcctoCreditHistory.html?ver=' + version + '"'),
         resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
     })

     .state('app.AgrTrnSuprIndividualDocCheckList', {
         url: '/AgrTrnSuprIndividualDocCheckList',
         title: 'AgrTrnSuprIndividualDocCheckList',
         templateUrl: helper.basepath('ems.mastersamagro/AgrTrnSuprIndividualDocCheckList.html?ver=' + version + '"'),
         resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
     })

    .state('app.AgrTrnSuprGroupDocCheckList', {
        url: '/AgrTrnSuprGroupDocCheckList',
        title: 'AgrTrnSuprGroupDocCheckList',
        templateUrl: helper.basepath('ems.mastersamagro/AgrTrnSuprGroupDocCheckList.html?ver=' + version + '"'),
        resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
    })

    .state('app.AgrTrnSuprIndividualCovenantDocChecklist', {
        url: '/AgrTrnSuprIndividualCovenantDocChecklist',
        title: 'AgrTrnSuprIndividualCovenantDocChecklist',
        templateUrl: helper.basepath('ems.mastersamagro/AgrTrnSuprIndividualCovenantDocChecklist.html?ver=' + version + '"'),
        resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
    })

     .state('app.AgrTrnSuprCreditIndividualDtlAdd', {
         url: '/AgrTrnSuprCreditIndividualDtlAdd',
         title: 'AgrTrnSuprCreditIndividualDtlAdd',
         templateUrl: helper.basepath('ems.mastersamagro/AgrTrnSuprCreditIndividualDtlAdd.html?ver=' + version + '"'),
         resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
     })

    .state('app.AgrTrnSuprCreditIndividualBankStatementAnalysisAdd', {
        url: '/AgrTrnSuprCreditIndividualBankStatementAnalysisAdd',
        title: 'AgrTrnSuprCreditIndividualBankStatementAnalysisAdd',
        templateUrl: helper.basepath('ems.mastersamagro/AgrTrnSuprCreditIndividualBankStatementAnalysisAdd.html?ver=' + version + '"'),
        resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
    })

    .state('app.AgrTrnSuprCreditIndividualPSLDataFlagAdd', {
        url: '/AgrTrnSuprCreditIndividualPSLDataFlagAdd',
        title: 'AgrTrnSuprCreditIndividualPSLDataFlagAdd',
        templateUrl: helper.basepath('ems.mastersamagro/AgrTrnSuprCreditIndividualPSLDataFlagAdd.html?ver=' + version + '"'),
        resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
    })

    .state('app.AgrTrnSuprCreditIndividualRepaymentAdd', {
        url: '/AgrTrnSuprCreditIndividualRepaymentAdd',
        title: 'AgrTrnSuprCreditIndividualRepaymentAdd',
        templateUrl: helper.basepath('ems.mastersamagro/AgrTrnSuprCreditIndividualRepaymentAdd.html?ver=' + version + '"'),
        resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
    })

    .state('app.AgrTrnSuprCreditIndividualObservationAdd', {
        url: '/AgrTrnSuprCreditIndividualObservationAdd',
        title: 'AgrTrnSuprCreditIndividualObservationAdd',
        templateUrl: helper.basepath('ems.mastersamagro/AgrTrnSuprCreditIndividualObservationAdd.html?ver=' + version + '"'),
        resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
    })

    .state('app.AgrTrnSuprCreditIndividualAPI', {
        url: '/AgrTrnSuprCreditIndividualAPI',
        title: 'AgrTrnSuprCreditIndividualAPI',
        templateUrl: helper.basepath('ems.mastersamagro/AgrTrnSuprCreditIndividualAPI.html?ver=' + version + '"'),
        resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
    })

         // Praveen

    .state('app.AgrMstSuprRMCustomerSummary', {
        url: '/AgrMstSuprRMCustomerSummary',
        title: 'AgrMstSuprRMCustomerSummary',
        templateUrl: helper.basepath('ems.mastersamagro/AgrMstSuprRMCustomerSummary.html?ver=' + version + '"'),
        resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
     })

        .state('app.AgrTrnSuprCadDocChecklistSummary', {
            url: '/AgrTrnSuprCadDocChecklistSummary',
            title: 'AgrTrnSuprCadDocChecklistSummary',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnSuprCadDocChecklistSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.AgrTrnSuprCadDocChecklistCheckerSummary', {
            url: '/AgrTrnSuprCadDocChecklistCheckerSummary',
            title: 'AgrTrnSuprCadDocChecklistCheckerSummary',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnSuprCadDocChecklistCheckerSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.AgrTrnSuprCadDocChecklistApprovalSummary', {
            url: '/AgrTrnSuprCadDocChecklistApprovalSummary',
            title: 'AgrTrnSuprCadDocChecklistApprovalSummary',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnSuprCadDocChecklistApprovalSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.AgrTrnSuprDocChecklistApprovalCompleted', {
            url: '/AgrTrnSuprDocChecklistApprovalCompleted',
            title: 'AgrTrnSuprDocChecklistApprovalCompleted',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnSuprDocChecklistApprovalCompleted.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.AgrTrnSuprCadGuarantorDetails', {
            url: '/AgrTrnSuprCadGuarantorDetails',
            title: 'AgrTrnSuprCadGuarantorDetails',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnSuprCadGuarantorDetails.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.AgrTrnSuprCadDocumentChecklistAdd', {
            url: '/AgrTrnSuprCadDocumentChecklistAdd',
            title: 'AgrTrnSuprCadDocumentChecklistAdd',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnSuprCadDocumentChecklistAdd.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.AgrTrnSuprCadGuarantorApproval', {
            url: '/AgrTrnSuprCadGuarantorApproval',
            title: 'AgrTrnSuprCadGuarantorApproval',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnSuprCadGuarantorApproval.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

//Scanned Document

        .state('app.AgrMstSuprCadDeferralSummary', {
            url: '/AgrMstSuprCadDeferralSummary',
            title: 'AgrMstSuprCadDeferralSummary',
            templateUrl: helper.basepath('ems.mastersamagro/AgrMstSuprCadDeferralSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.AgrMstSuprCadDeferralCheckerSummary', {
            url: '/AgrMstSuprCadDeferralCheckerSummary',
            title: 'AgrMstSuprCadDeferralCheckerSummary',
            templateUrl: helper.basepath('ems.mastersamagro/AgrMstSuprCadDeferralCheckerSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.AgrMstSuprCadDeferralApprovalSummary', {
            url: '/AgrMstSuprCadDeferralApprovalSummary',
            title: 'AgrMstSuprCadDeferralApprovalSummary',
            templateUrl: helper.basepath('ems.mastersamagro/AgrMstSuprCadDeferralApprovalSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.AgrMstSuprScannedCompletedSummary', {
            url: '/AgrMstSuprScannedCompletedSummary',
            title: 'AgrMstSuprScannedCompletedSummary',
            templateUrl: helper.basepath('ems.mastersamagro/AgrMstSuprScannedCompletedSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.AgrTrnSuprCadDeferralGuarantorDtls', {
            url: '/AgrTrnSuprCadDeferralGuarantorDtls',
            title: 'AgrTrnSuprCadDeferralGuarantorDtls',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnSuprCadDeferralGuarantorDtls.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.AgrTrnSuprCadDeferralDochecklist', {
            url: '/AgrTrnSuprCadDeferralDochecklist',
            title: 'AgrTrnSuprCadDeferralDochecklist',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnSuprCadDeferralDochecklist.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.AgrTrnSuprCadDeferralQuery', {
            url: '/AgrTrnSuprCadDeferralQuery',
            title: 'AgrTrnSuprCadDeferralQuery',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnSuprCadDeferralQuery.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.AgrTrnSuprCadDeferralStatus', {
            url: '/AgrTrnSuprCadDeferralStatus',
            title: 'AgrTrnSuprCadDeferralStatus',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnSuprCadDeferralStatus.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.AgrTrnSuprRMDeferralDtls', {
            url: '/AgrTrnSuprRMDeferralDtls',
            title: 'AgrTrnSuprRMDeferralDtls',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnSuprRMDeferralDtls.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.AgrTrnSuprRMDeferralCloseQuery', {
            url: '/AgrTrnSuprRMDeferralCloseQuery',
            title: 'AgrTrnSuprRMDeferralCloseQuery',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnSuprRMDeferralCloseQuery.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.AgrTrnSuprScannedDeferralHistory', {
            url: '/AgrTrnSuprScannedDeferralHistory',
            title: 'AgrTrnSuprScannedDeferralHistory',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnSuprScannedDeferralHistory.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        //Physical Document

        .state('app.AgrTrnSuprCadPhysicalDocSummary', {
            url: '/AgrTrnSuprCadPhysicalDocSummary',
            title: 'AgrTrnSuprCadPhysicalDocSummary',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnSuprCadPhysicalDocSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.AgrTrnSuprCadPhysicalDocCheckerSummary', {
            url: '/AgrTrnSuprCadPhysicalDocCheckerSummary',
            title: 'AgrTrnSuprCadPhysicalDocCheckerSummary',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnSuprCadPhysicalDocCheckerSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.AgrTrnSuprCadPhysicalDocApprovalSummary', {
            url: '/AgrTrnSuprCadPhysicalDocApprovalSummary',
            title: 'AgrTrnSuprCadPhysicalDocApprovalSummary',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnSuprCadPhysicalDocApprovalSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.AgrTrnSuprCadPhysicalDocCompletedSummary', {
            url: '/AgrTrnSuprCadPhysicalDocCompletedSummary',
            title: 'AgrTrnSuprCadPhysicalDocCompletedSummary',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnSuprCadPhysicalDocCompletedSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.AgrTrnSuprCadPhysicalDocGuarantorDtls', {
            url: '/AgrTrnSuprCadPhysicalDocGuarantorDtls',
            title: 'AgrTrnSuprCadPhysicalDocGuarantorDtls',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnSuprCadPhysicalDocGuarantorDtls.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.AgrTrnSuprCadPhysicalDochecklist', {
            url: '/AgrTrnSuprCadPhysicalDochecklist',
            title: 'AgrTrnSuprCadPhysicalDochecklist',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnSuprCadPhysicalDochecklist.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.AgrTrnSuprPhysicalDeferralHistory', {
            url: '/AgrTrnSuprPhysicalDeferralHistory',
            title: 'AgrTrnSuprPhysicalDeferralHistory',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnSuprPhysicalDeferralHistory.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.AgrTrnSuprCadPhysicalDocQuery', {
            url: '/AgrTrnSuprCadPhysicalDocQuery',
            title: 'AgrTrnSuprCadPhysicalDocQuery',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnSuprCadPhysicalDocQuery.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.AgrTrnSuprCadPhysicalDocStatus', {
            url: '/AgrTrnSuprCadPhysicalDocStatus',
            title: 'AgrTrnSuprCadPhysicalDocStatus',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnSuprCadPhysicalDocStatus.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

		//My Approvals
        .state('app.AgrTrnSuprDeferralMyApproval', {
            url: '/AgrTrnSuprDeferralMyApproval',
            title: 'AgrTrnSuprDeferralMyApproval',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnSuprDeferralMyApproval.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.AgrTrnSuprDeferralMyApprovalHistory', {
            url: '/AgrTrnSuprDeferralMyApprovalHistory',
            title: 'AgrTrnSuprDeferralMyApprovalHistory',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnSuprDeferralMyApprovalHistory.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
        //Supplier 360 - Process
        .state('app.AgrTrnSuprPostCcActivitiesRMView', {
            url: '/AgrTrnSuprPostCcActivitiesRMView',
            title: 'AgrTrnSuprPostCcActivitiesRMView',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnSuprPostCcActivitiesRMView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        //Supplier Samagro Views
        //Application Creation View
        .state('app.AgrMstSuprApplicationCreationView', {
            url: '/AgrMstSuprApplicationCreationView',
            title: 'AgrMstSuprApplicationCreationView',
            templateUrl: helper.basepath('ems.mastersamagro/AgrMstSuprApplicationCreationView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.AgrMstSuprApplCreationGradingToolView', {
            url: '/AgrMstSuprApplCreationGradingToolView',
            title: 'AgrMstSuprApplCreationGradingToolView',
            templateUrl: helper.basepath('ems.mastersamagro/AgrMstSuprApplCreationGradingToolView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.AgrMstSuprApplCreationVisitReportView', {
            url: '/AgrMstSuprApplCreationVisitReportView',
            title: 'AgrMstSuprApplCreationVisitReportView',
            templateUrl: helper.basepath('ems.mastersamagro/AgrMstSuprApplCreationVisitReportView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.AgrMstSuprApplCreationInstitutionGuarantorView', {
            url: '/AgrMstSuprApplCreationInstitutionGuarantorView',
            title: 'AgrMstSuprApplCreationInstitutionGuarantorView',
            templateUrl: helper.basepath('ems.mastersamagro/AgrMstSuprApplCreationInstitutionGuarantorView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.AgrMstSuprApplCreationIndividualGuarantorView', {
            url: '/AgrMstSuprApplCreationIndividualGuarantorView',
            title: 'AgrMstSuprApplCreationIndividualGuarantorView',
            templateUrl: helper.basepath('ems.mastersamagro/AgrMstSuprApplCreationIndividualGuarantorView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.AgrMstSuprApplicationEditKycView', {
            url: '/AgrMstSuprApplicationEditKycView',
            title: 'AgrMstSuprApplicationEditKycView',
            templateUrl: helper.basepath('ems.mastersamagro/AgrMstSuprApplicationEditKycView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
        //Credit View
        .state('app.AgrMstSuprCcCommitteeApplicationView', {
            url: '/AgrMstSuprCcCommitteeApplicationView',
            title: 'AgrMstSuprCcCommitteeApplicationView',
            templateUrl: helper.basepath('ems.mastersamagro/AgrMstSuprCcCommitteeApplicationView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.AgrMstSuprCcCommitteeKycView', {
            url: '/AgrMstSuprCcCommitteeKycView',
            title: 'AgrMstSuprCcCommitteeKycView',
            templateUrl: helper.basepath('ems.mastersamagro/AgrMstSuprCcCommitteeKycView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.AgrTrnSuprCCCommitteeInstitutionView', {
            url: '/AgrTrnSuprCCCommitteeInstitutionView',
            title: 'AgrTrnSuprCCCommitteeInstitutionView',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnSuprCCCommitteeInstitutionView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.AgrTrnSuprCCCommitteeIndividualView', {
            url: '/AgrTrnSuprCCCommitteeIndividualView',
            title: 'AgrTrnSuprCCCommitteeIndividualView',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnSuprCCCommitteeIndividualView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.AgrTrnSuprCCCommitteeGroupView', {
            url: '/AgrTrnSuprCCCommitteeGroupView',
            title: 'AgrTrnSuprCCCommitteeGroupView',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnSuprCCCommitteeGroupView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.AgrMstSuprCcCommitteeInstitutionView', {
            url: '/AgrMstSuprCcCommitteeInstitutionView',
            title: 'AgrMstSuprCcCommitteeInstitutionView',
            templateUrl: helper.basepath('ems.mastersamagro/AgrMstSuprCcCommitteeInstitutionView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.AgrMstSuprCcCommitteeIndividualView', {
            url: '/AgrMstSuprCcCommitteeIndividualView',
            title: 'AgrMstSuprCcCommitteeIndividualView',
            templateUrl: helper.basepath('ems.mastersamagro/AgrMstSuprCcCommitteeIndividualView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.AgrMstSuprCcCommitteeGroupView', {
            url: '/AgrMstSuprCcCommitteeGroupView',
            title: 'AgrMstSuprCcCommitteeGroupView',
            templateUrl: helper.basepath('ems.mastersamagro/AgrMstSuprCcCommitteeGroupView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

         // Abilash

          // Supplier creation Routes

         .state('app.AgrTrnSuprPendingCADReview', {
             url: '/AgrTrnSuprPendingCADReview',
             title: 'AgrTrnSuprPendingCADReview',
             templateUrl: helper.basepath('ems.mastersamagro/AgrTrnSuprPendingCADReview.html?ver=' + version + '"'),
             resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'textAngular', 'jquery-ui', 'jquery-ui-widgets', 'moment', 'fullcalendar')
         })

         .state('app.AgrTrnSuprCadAcceptedCustomers', {
             url: '/AgrTrnSuprCadAcceptedCustomers',
             title: 'AgrTrnSuprCadAcceptedCustomers',
             templateUrl: helper.basepath('ems.mastersamagro/AgrTrnSuprCadAcceptedCustomers.html?ver=' + version + '"'),
             resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
         })

        .state('app.AgrTrnSuprSentBackToUnderwriting', {
            url: '/AgrTrnSuprSentBackToUnderwriting',
            title: 'AgrTrnSuprSentBackToUnderwriting',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnSuprSentBackToUnderwriting.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.AgrTrnSuprSentBackToCC', {
            url: '/AgrTrnSuprSentBackToCC',
            title: 'AgrTrnSuprSentBackToCC',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnSuprSentBackToCC.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.AgrTrnSuprCCRejectedApplications', {
            url: '/AgrTrnSuprCCRejectedApplications',
            title: 'AgrTrnSuprCCRejectedApplications',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnSuprCCRejectedApplications.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.AgrTrnSuprCADApplicationEdit', {
            url: '/AgrTrnSuprCADApplicationEdit',
            title: 'AgrTrnSuprCADApplicationEdit',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnSuprCADApplicationEdit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

          .state('app.AgrTrnSuprCADGroupProcessAssign', {
              url: '/AgrTrnSuprCADGroupProcessAssign',
              title: 'AgrTrnSuprCADGroupProcessAssign',
              templateUrl: helper.basepath('ems.mastersamagro/AgrTrnSuprCADGroupProcessAssign.html?ver=' + version + '"'),
              resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
          })

        .state('app.AgrTrnSuprCADReassignApplication', {
            url: '/AgrTrnSuprCADReassignApplication',
            title: 'AgrTrnSuprCADReassignApplication',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnSuprCADReassignApplication.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

         .state('app.AgrMstSuprApplicationCreationSummary', {
             url: '/AgrMstSuprApplicationCreationSummary',
             title: 'AgrMstSuprApplicationCreationSummary',
             templateUrl: helper.basepath('ems.mastersamagro/AgrMstSuprApplicationCreationSummary.html?ver=' + version + '"'),
             resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
         })

         .state('app.AgrMstSuprRejectedApplicationSummary', {
             url: '/AgrMstSuprRejectedApplicationSummary',
             title: 'AgrMstSuprRejectedApplicationSummary',
             templateUrl: helper.basepath('ems.mastersamagro/AgrMstSuprRejectedApplicationSummary.html?ver=' + version + '"'),
             resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
         })

         .state('app.AgrMstSuprHoldApplicationSummary', {
             url: '/AgrMstSuprHoldApplicationSummary',
             title: 'AgrMstSuprHoldApplicationSummary',
             templateUrl: helper.basepath('ems.mastersamagro/AgrMstSuprHoldApplicationSummary.html?ver=' + version + '"'),
             resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
         })

           .state('app.AgrMstSuprApprovedApplicationSummary', {
               url: '/AgrMstSuprApprovedApplicationSummary',
               title: 'AgrMstSuprApprovedApplicationSummary',
               templateUrl: helper.basepath('ems.mastersamagro/AgrMstSuprApprovedApplicationSummary.html?ver=' + version + '"'),
               resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
           })

          .state('app.AgrMstSuprApplicationGeneralAdd', {
              url: '/AgrMstSuprApplicationGeneralAdd',
              title: 'AgrMstSuprApplicationGeneralAdd',
              templateUrl: helper.basepath('ems.mastersamagro/AgrMstSuprApplicationGeneralAdd.html?ver=' + version + '"'),
              resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
          })

          .state('app.AgrMstSuprApplicationInstitutionAdd', {
              url: '/AgrMstSuprApplicationInstitutionAdd',
              title: 'AgrMstSuprApplicationInstitutionAdd',
              templateUrl: helper.basepath('ems.mastersamagro/AgrMstSuprApplicationInstitutionAdd.html?ver=' + version + '"'),
              resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
          })

         .state('app.AgrMstSuprApplicationIndividualAdd', {
             url: '/AgrMstSuprApplicationIndividualAdd',
             title: 'AgrMstSuprApplicationIndividualAdd',
             templateUrl: helper.basepath('ems.mastersamagro/AgrMstSuprApplicationIndividualAdd.html?ver=' + version + '"'),
             resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
         })

         .state('app.AgrMstSuprApplicationSocialTradeCapitalAdd', {
             url: '/AgrMstSuprApplicationSocialTradeCapitalAdd',
             title: 'AgrMstSuprApplicationSocialTradeCapitalAdd',
             templateUrl: helper.basepath('ems.mastersamagro/AgrMstSuprApplicationSocialTradeCapitalAdd.html?ver=' + version + '"'),
             resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
         })

          .state('app.AgrMstSuprApplicationOverallLimitAdd', {
              url: '/AgrMstSuprApplicationOverallLimitAdd',
              title: 'AgrMstSuprApplicationOverallLimitAdd',
              templateUrl: helper.basepath('ems.mastersamagro/AgrMstSuprApplicationOverallLimitAdd.html?ver=' + version + '"'),
              resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
          })

         .state('app.AgrMstSuprApplicationProductChargesAdd', {
             url: '/AgrMstSuprApplicationProductChargesAdd',
             title: 'AgrMstSuprApplicationProductChargesAdd',
             templateUrl: helper.basepath('ems.mastersamagro/AgrMstSuprApplicationProductChargesAdd.html?ver=' + version + '"'),
             resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
         })

          .state('app.AgrMstSuprApplicationServiceChargeAdd', {
              url: '/AgrMstSuprApplicationServiceChargeAdd',
              title: 'AgrMstSuprApplicationServiceChargeAdd',
              templateUrl: helper.basepath('ems.mastersamagro/AgrMstSuprApplicationServiceChargeAdd.html?ver=' + version + '"'),
              resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
          })

           .state('app.AgrMstSuprApplicationCICUploadAdd', {
               url: '/AgrMstSuprApplicationCICUploadAdd',
               title: 'AgrMstSuprApplicationCICUploadAdd',
               templateUrl: helper.basepath('ems.mastersamagro/AgrMstSuprApplicationCICUploadAdd.html?ver=' + version + '"'),
               resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
           })

          .state('app.AgrMstSuprApplcreationBasicdtlEdit', {
              url: '/AgrMstSuprApplcreationBasicdtlEdit',
              title: 'AgrMstSuprApplcreationBasicdtlEdit',
              templateUrl: helper.basepath('ems.mastersamagro/AgrMstSuprApplcreationBasicdtlEdit.html?ver=' + version + '"'),
              resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
          })

         .state('app.AgrMstSuprApplcreationIndividualdtlEdit', {
             url: '/AgrMstSuprApplcreationIndividualdtlEdit',
             title: 'AgrMstSuprApplcreationIndividualdtlEdit',
             templateUrl: helper.basepath('ems.mastersamagro/AgrMstSuprApplcreationIndividualdtlEdit.html?ver=' + version + '"'),
             resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
         })

          .state('app.AgrMstSuprApplcreationInstitutiondtlEdit', {
              url: '/AgrMstSuprApplcreationInstitutiondtlEdit',
              title: 'AgrMstSuprApplcreationInstitutiondtlEdit',
              templateUrl: helper.basepath('ems.mastersamagro/AgrMstSuprApplcreationInstitutiondtlEdit.html?ver=' + version + '"'),
              resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
          })

         .state('app.AgrMstSuprApplcreationSocialTradeEdit', {
             url: '/AgrMstSuprApplcreationSocialTradeEdit',
             title: 'AgrMstSuprApplcreationSocialTradeEdit',
             templateUrl: helper.basepath('ems.mastersamagro/AgrMstSuprApplcreationSocialTradeEdit.html?ver=' + version + '"'),
             resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
         })

           .state('app.AgrMstSuprApplcreationProductchargesEdit', {
               url: '/AgrMstSuprApplcreationProductchargesEdit',
               title: 'AgrMstSuprApplcreationProductchargesEdit',
               templateUrl: helper.basepath('ems.mastersamagro/AgrMstSuprApplcreationProductchargesEdit.html?ver=' + version + '"'),
               resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
           })

         .state('app.AgrMstSuprBureauUpdateIndividual', {
             url: '/AgrMstSuprBureauUpdateIndividual',
             title: 'AgrMstSuprBureauUpdateIndividual',
             templateUrl: helper.basepath('ems.mastersamagro/AgrMstSuprBureauUpdateIndividual.html?ver=' + version + '"'),
             resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
         })

         .state('app.AgrMstSuprBureauUpdateInstitution', {
             url: '/AgrMstSuprBureauUpdateInstitution',
             title: 'AgrMstSuprBureauUpdateInstitution',
             templateUrl: helper.basepath('ems.mastersamagro/AgrMstSuprBureauUpdateInstitution.html?ver=' + version + '"'),
             resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
         })

          .state('app.AgrMstSuprApplcreationCICUploadView', {
              url: '/AgrMstSuprApplcreationCICUploadView',
              title: 'AgrMstSuprApplcreationCICUploadView',
              templateUrl: helper.basepath('ems.mastersamagro/AgrMstSuprApplcreationCICUploadView.html?ver=' + version + '"'),
              resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
          })

          .state('app.AgrMstSuprApplcreationCICUploadEdit', {
              url: '/AgrMstSuprApplcreationCICUploadEdit',
              title: 'AgrMstSuprApplcreationCICUploadEdit',
              templateUrl: helper.basepath('ems.mastersamagro/AgrMstSuprApplcreationCICUploadEdit.html?ver=' + version + '"'),
              resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
          })

          .state('app.AgrMstSuprApplcreationCICUploadInstView', {
              url: '/AgrMstSuprApplcreationCICUploadInstView',
              title: 'AgrMstSuprApplcreationCICUploadInstView',
              templateUrl: helper.basepath('ems.mastersamagro/AgrMstSuprApplcreationCICUploadInstView.html?ver=' + version + '"'),
              resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
          })

         .state('app.AgrMstSuprApplCreationCICUploadInstEdit', {
             url: '/AgrMstSuprApplCreationCICUploadInstEdit',
             title: 'AgrMstSuprApplCreationCICUploadInstEdit',
             templateUrl: helper.basepath('ems.mastersamagro/AgrMstSuprApplCreationCICUploadInstEdit.html?ver=' + version + '"'),
             resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
         })

          .state('app.AgrMstSuprApplicationGeneralEdit', {
              url: '/AgrMstSuprApplicationGeneralEdit',
              title: 'AgrMstSuprApplicationGeneralEdit',
              templateUrl: helper.basepath('ems.mastersamagro/AgrMstSuprApplicationGeneralEdit.html?ver=' + version + '"'),
              resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
          })

          .state('app.AgrMstSuprApplicationInstitutionEdit', {
              url: '/AgrMstSuprApplicationInstitutionEdit',
              title: 'AgrMstSuprApplicationInstitutionEdit',
              templateUrl: helper.basepath('ems.mastersamagro/AgrMstSuprApplicationInstitutionEdit.html?ver=' + version + '"'),
              resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
          })

           .state('app.AgrMstSuprApplicationIndividualEdit', {
               url: '/AgrMstSuprApplicationIndividualEdit',
               title: 'AgrMstSuprApplicationIndividualEdit',
               templateUrl: helper.basepath('ems.mastersamagro/AgrMstSuprApplicationIndividualEdit.html?ver=' + version + '"'),
               resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
           })

           .state('app.AgrMstSuprApplicationSocialTradeCapitalEdit', {
               url: '/AgrMstSuprApplicationSocialTradeCapitalEdit',
               title: 'AgrMstSuprApplicationSocialTradeCapitalEdit',
               templateUrl: helper.basepath('ems.mastersamagro/AgrMstSuprApplicationSocialTradeCapitalEdit.html?ver=' + version + '"'),
               resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
           })

          .state('app.AgrMstSuprAppEditOverallLimitAdd', {
              url: '/AgrMstSuprAppEditOverallLimitAdd',
              title: 'AgrMstSuprAppEditOverallLimitAdd',
              templateUrl: helper.basepath('ems.mastersamagro/AgrMstSuprAppEditOverallLimitAdd.html?ver=' + version + '"'),
              resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
          })

           .state('app.AgrMstSuprAppEditChargeAdd', {
               url: '/AgrMstSuprAppEditChargeAdd',
               title: 'AgrMstSuprAppEditChargeAdd',
               templateUrl: helper.basepath('ems.mastersamagro/AgrMstSuprAppEditChargeAdd.html?ver=' + version + '"'),
               resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
           })

         .state('app.AgrMstSuprApplicationEditCICUploadAdd', {
             url: '/AgrMstSuprApplicationEditCICUploadAdd',
             title: 'AgrMstSuprApplicationEditCICUploadAdd',
             templateUrl: helper.basepath('ems.mastersamagro/AgrMstSuprApplicationEditCICUploadAdd.html?ver=' + version + '"'),
             resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
         })

          .state('app.AgrSuprGradingToolAdd', {
              url: '/AgrSuprGradingToolAdd',
              title: 'AgrSuprGradingToolAdd',
              templateUrl: helper.basepath('ems.mastersamagro/AgrSuprGradingToolAdd.html?ver=' + version + '"'),
              resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
          })

           .state('app.AgrSuprGradingToolEdit', {
               url: '/AgrSuprGradingToolEdit',
               title: 'AgrSuprGradingToolEdit',
               templateUrl: helper.basepath('ems.mastersamagro/AgrSuprGradingToolEdit.html?ver=' + version + '"'),
               resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
           })

         .state('app.AgrSuprGradingToolView', {
             url: '/AgrSuprGradingToolView',
             title: 'AgrSuprGradingToolView',
             templateUrl: helper.basepath('ems.mastersamagro/AgrSuprGradingToolView.html?ver=' + version + '"'),
             resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
         })

          .state('app.AgrMstSuprVisitReportAdd', {
              url: '/AgrMstSuprVisitReportAdd',
              title: 'AgrMstSuprVisitReportAdd',
              templateUrl: helper.basepath('ems.mastersamagro/AgrMstSuprVisitReportAdd.html?ver=' + version + '"'),
              resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
          })

          .state('app.AgrMstSuprVisitReportEdit', {
              url: '/AgrMstSuprVisitReportEdit',
              title: 'AgrMstSuprVisitReportEdit',
              templateUrl: helper.basepath('ems.mastersamagro/AgrMstSuprVisitReportEdit.html?ver=' + version + '"'),
              resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
          })

          .state('app.AgrMstSuprVisitReportView', {
              url: '/AgrMstSuprVisitReportView',
              title: 'AgrMstSuprVisitReportView',
              templateUrl: helper.basepath('ems.mastersamagro/AgrMstSuprVisitReportView.html?ver=' + version + '"'),
              resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
          })

         .state('app.AgrMstSuprAppEditProductAdd', {
             url: '/AgrMstSuprAppEditProductAdd',
             title: 'AgrMstSuprAppEditProductAdd',
             templateUrl: helper.basepath('ems.mastersamagro/AgrMstSuprAppEditProductAdd.html?ver=' + version + '"'),
             resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
         })

         .state('app.AgrMstSuprAppEditTradeAdd', {
             url: '/AgrMstSuprAppEditTradeAdd',
             title: 'AgrMstSuprAppEditTradeAdd',
             templateUrl: helper.basepath('ems.mastersamagro/AgrMstSuprAppEditTradeAdd.html?ver=' + version + '"'),
             resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
         })

         .state('app.AgrMstSuprApplicationCreationRMApproval', {
             url: '/AgrMstSuprApplicationCreationRMApproval',
             title: 'AgrMstSuprApplicationCreationRMApproval',
             templateUrl: helper.basepath('ems.mastersamagro/AgrMstSuprApplicationCreationRMApproval.html?ver=' + version + '"'),
             resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
         })

         .state('app.AgrMstSuprCADInstitutionDtlAdd', {
             url: '/AgrMstSuprCADInstitutionDtlAdd',
             title: 'AgrMstSuprCADInstitutionDtlAdd',
             templateUrl: helper.basepath('ems.mastersamagro/AgrMstSuprCADInstitutionDtlAdd.html?ver=' + version + '"'),
             resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
         })

        .state('app.AgrMstSuprCADIndividualDtlAdd', {
            url: '/AgrMstSuprCADIndividualDtlAdd',
            title: 'AgrMstSuprCADIndividualDtlAdd',
            templateUrl: helper.basepath('ems.mastersamagro/AgrMstSuprCADIndividualDtlAdd.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.AgrTrnSuprCreditGeneralDtlEdit', {
            url: '/AgrTrnSuprCreditGeneralDtlEdit',
            title: 'AgrTrnSuprCreditGeneralDtlEdit',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnSuprCreditGeneralDtlEdit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.AgrTrnSuprCreditInstitutionDtlEdit', {
            url: '/AgrTrnSuprCreditInstitutionDtlEdit',
            title: 'AgrTrnSuprCreditInstitutionDtlEdit',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnSuprCreditInstitutionDtlEdit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.AgrTrnSuprCreditIndividualDtlEdit', {
            url: '/AgrTrnSuprCreditIndividualDtlEdit',
            title: 'AgrTrnSuprCreditIndividualDtlEdit',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnSuprCreditIndividualDtlEdit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.AgrTrnSuprCreditProductChargesDtlEdit', {
            url: '/AgrTrnSuprCreditProductChargesDtlEdit',
            title: 'AgrTrnSuprCreditProductChargesDtlEdit',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnSuprCreditProductChargesDtlEdit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

         .state('app.AgrTrnSuprCreditLoanDtlEdit', {
             url: '/AgrTrnSuprCreditLoanDtlEdit',
             title: 'AgrTrnSuprCreditLoanDtlEdit',
             templateUrl: helper.basepath('ems.mastersamagro/AgrTrnSuprCreditLoanDtlEdit.html?ver=' + version + '"'),
             resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
         })
// Sundar

        .state('app.AgrTrnSuprCcCommitteeApplicationView', {
            url: '/AgrTrnSuprCcCommitteeApplicationView',
            title: 'AgrTrnSuprCcCommitteeApplicationView',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnSuprCcCommitteeApplicationView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.AgrMstSuprBusinessApprovalSummary', {
            url: '/AgrMstSuprBusinessApprovalSummary',
            title: 'AgrMstSuprBusinessApprovalSummary',
            templateUrl: helper.basepath('ems.mastersamagro/AgrMstSuprBusinessApprovalSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.AgrMstSuprBusinessRejectedSummary', {
            url: '/AgrMstSuprBusinessRejectedSummary',
            title: 'AgrMstSuprBusinessRejectedSummary',
            templateUrl: helper.basepath('ems.mastersamagro/AgrMstSuprBusinessRejectedSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.AgrMstSuprBusinessHoldSummary', {
            url: '/AgrMstSuprBusinessHoldSummary',
            title: 'AgrMstSuprBusinessHoldSummary',
            templateUrl: helper.basepath('ems.mastersamagro/AgrMstSuprBusinessHoldSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.AgrMstSuprBusinessApproval', {
            url: '/AgrMstSuprBusinessApproval',
            title: 'AgrMstSuprBusinessApproval',
            templateUrl: helper.basepath('ems.mastersamagro/AgrMstSuprBusinessApproval.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.AgrMstSuprBusinessApprovedSummary', {
            url: '/AgrMstSuprBusinessApprovedSummary',
            title: 'AgrMstSuprBusinessApprovedSummary',
            templateUrl: helper.basepath('ems.mastersamagro/AgrMstSuprBusinessApprovedSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.AgrSuprGSPGSTReturnFilingView', {
            url: '/AgrSuprGSPGSTReturnFilingView',
            title: 'AgrSuprGSPGSTReturnFilingView',
            templateUrl: helper.basepath('ems.mastersamagro/AgrSuprGSPGSTReturnFilingView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

         .state('app.AgrSuprRCAuthAdvancedView', {
             url: '/AgrSuprRCAuthAdvancedView',
             title: 'AgrSuprRCAuthAdvancedView',
             templateUrl: helper.basepath('ems.mastersamagro/AgrSuprRCAuthAdvancedView.html?ver=' + version + '"'),
             resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
         })

         .state('app.AgrSuprPropertyTaxView', {
             url: '/AgrSuprPropertyTaxView',
             title: 'AgrSuprPropertyTaxView',
             templateUrl: helper.basepath('ems.mastersamagro/AgrSuprPropertyTaxView.html?ver=' + version + '"'),
             resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
         })

         .state('app.AgrSuprIECDetailedProfileView', {
             url: '/AgrSuprIECDetailedProfileView',
             title: 'AgrSuprIECDetailedProfileView',
             templateUrl: helper.basepath('ems.mastersamagro/AgrSuprIECDetailedProfileView.html?ver=' + version + '"'),
             resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
         })

        .state('app.AgrSuprGSTAuthenticationView', {
            url: '/AgrSuprGSTAuthenticationView',
            title: 'AgrSuprGSTAuthenticationView',
            templateUrl: helper.basepath('ems.mastersamagro/AgrSuprGSTAuthenticationView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.AgrSuprGSPGSTINAuthenticationView', {
            url: '/AgrSuprGSPGSTINAuthenticationView',
            title: 'AgrSuprGSPGSTINAuthenticationView',
            templateUrl: helper.basepath('ems.mastersamagro/AgrSuprGSPGSTINAuthenticationView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.AgrTrnSuprCreditIndividualRepaymentEdit', {
            url: '/AgrTrnSuprCreditIndividualRepaymentEdit',
            title: 'AgrTrnSuprCreditIndividualRepaymentEdit',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnSuprCreditIndividualRepaymentEdit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

       .state('app.AgrTrnSuprCreditIndividualBankAcctAdd', {
           url: '/AgrTrnSuprCreditIndividualBankAcctAdd',
           title: 'AgrTrnSuprCreditIndividualBankAcctAdd',
           templateUrl: helper.basepath('ems.mastersamagro/AgrTrnSuprCreditIndividualBankAcctAdd.html?ver=' + version + '"'),
           resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
       })

       .state('app.AgrTrnSuprCreditIndividualExistingBankAdd', {
           url: '/AgrTrnSuprCreditIndividualExistingBankAdd',
           title: 'AgrTrnSuprCreditIndividualExistingBankAdd',
           templateUrl: helper.basepath('ems.mastersamagro/AgrTrnSuprCreditIndividualExistingBankAdd.html?ver=' + version + '"'),
           resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
       })

       .state('app.AgrTrnSuprCreditIndividualBureauEdit', {
           url: '/AgrTrnSuprCreditIndividualBureauEdit',
           title: 'AgrTrnSuprCreditIndividualBureauEdit',
           templateUrl: helper.basepath('ems.mastersamagro/AgrTrnSuprCreditIndividualBureauEdit.html?ver=' + version + '"'),
           resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
       })

       .state('app.AgrTrnSuprCreditIndividualBureauView', {
           url: '/AgrTrnSuprCreditIndividualBureauView',
           title: 'AgrTrnSuprCreditIndividualBureauView',
           templateUrl: helper.basepath('ems.mastersamagro/AgrTrnSuprCreditIndividualBureauView.html?ver=' + version + '"'),
           resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
       })

       .state('app.AgrTrnSuprCreditCrimeCheckRecordAPI', {
           url: '/AgrTrnSuprCreditCrimeCheckRecordAPI',
           title: 'AgrTrnSuprCreditCrimeCheckRecordAPI',
           templateUrl: helper.basepath('ems.mastersamagro/AgrTrnSuprCreditCrimeCheckRecordAPI.html?ver=' + version + '"'),
           resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
       })

       .state('app.AgrTrnSuprDocumentCheckList', {
           url: '/AgrTrnSuprDocumentCheckList',
           title: 'AgrTrnSuprDocumentCheckList',
           templateUrl: helper.basepath('ems.mastersamagro/AgrTrnSuprDocumentCheckList.html?ver=' + version + '"'),
           resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
       })

       .state('app.AgrTrnSuprCreditAddCovenantCheckList', {
           url: '/AgrTrnSuprCreditAddCovenantCheckList',
           title: 'AgrTrnSuprCreditAddCovenantCheckList',
           templateUrl: helper.basepath('ems.mastersamagro/AgrTrnSuprCreditAddCovenantCheckList.html?ver=' + version + '"'),
           resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
       })

       .state('app.AgrTrnSuprCreditEconomicCapitalAdd', {
           url: '/AgrTrnSuprCreditEconomicCapitalAdd',
           title: 'AgrTrnSuprCreditEconomicCapitalAdd',
           templateUrl: helper.basepath('ems.mastersamagro/AgrTrnSuprCreditEconomicCapitalAdd.html?ver=' + version + '"'),
           resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
       })

       .state('app.AgrTrnSuprCreditCompanyDtlAdd', {
           url: '/AgrTrnSuprCreditCompanyDtlAdd',
           title: 'AgrTrnSuprCreditCompanyDtlAdd',
           templateUrl: helper.basepath('ems.mastersamagro/AgrTrnSuprCreditCompanyDtlAdd.html?ver=' + version + '"'),
           resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
       })

       .state('app.AgrTrnSuprCreditPSLDataFlaggingAdd', {
           url: '/AgrTrnSuprCreditPSLDataFlaggingAdd',
           title: 'AgrTrnSuprCreditPSLDataFlaggingAdd',
           templateUrl: helper.basepath('ems.mastersamagro/AgrTrnSuprCreditPSLDataFlaggingAdd.html?ver=' + version + '"'),
           resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
       })

       .state('app.AgrTrnSuprCreditSuppliersDtlAdd', {
           url: '/AgrTrnSuprCreditSuppliersDtlAdd',
           title: 'AgrTrnSuprCreditSuppliersDtlAdd',
           templateUrl: helper.basepath('ems.mastersamagro/AgrTrnSuprCreditSuppliersDtlAdd.html?ver=' + version + '"'),
           resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
       })

       .state('app.AgrTrnSuprCreditBuyerDtlAdd', {
           url: '/AgrTrnSuprCreditBuyerDtlAdd',
           title: 'AgrTrnSuprCreditBuyerDtlAdd',
           templateUrl: helper.basepath('ems.mastersamagro/AgrTrnSuprCreditBuyerDtlAdd.html?ver=' + version + '"'),
           resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
       })

       .state('app.AgrTrnSuprCreditBankAccountDtlAdd', {
           url: '/AgrTrnSuprCreditBankAccountDtlAdd',
           title: 'AgrTrnSuprCreditBankAccountDtlAdd',
           templateUrl: helper.basepath('ems.mastersamagro/AgrTrnSuprCreditBankAccountDtlAdd.html?ver=' + version + '"'),
           resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

       .state('app.AgrTrnSuprCreditExistingBankDtlAdd', {
           url: '/AgrTrnSuprCreditExistingBankDtlAdd',
           title: 'AgrTrnSuprCreditExistingBankDtlAdd',
           templateUrl: helper.basepath('ems.mastersamagro/AgrTrnSuprCreditExistingBankDtlAdd.html?ver=' + version + '"'),
           resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

       .state('app.AgrTrnSuprCreditCompanyAPIAdd', {
           url: '/AgrTrnSuprCreditCompanyAPIAdd',
           title: 'AgrTrnSuprCreditCompanyAPIAdd',
           templateUrl: helper.basepath('ems.mastersamagro/AgrTrnSuprCreditCompanyAPIAdd.html?ver=' + version + '"'),
           resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
       })

        .state('app.AgrTrnSuprCreditRepaymentDtlAdd', {
            url: '/AgrTrnSuprCreditRepaymentDtlAdd',
            title: 'AgrTrnSuprCreditRepaymentDtlAdd',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnSuprCreditRepaymentDtlAdd.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

       .state('app.AgrTrnSuprCreditObservationAdd', {
            url: '/AgrTrnSuprCreditObservationAdd',
            title: 'AgrTrnSuprCreditObservationAdd',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnSuprCreditObservationAdd.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.AgrTrnSuprCreditBankStatementAnalysisAdd', {
            url: '/AgrTrnSuprCreditBankStatementAnalysisAdd',
            title: 'AgrTrnSuprCreditBankStatementAnalysisAdd',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnSuprCreditBankStatementAnalysisAdd.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.AgrTrnSuprCreditFsaDetailAdd', {
            url: '/AgrTrnSuprCreditFsaDetailAdd',
            title: 'AgrTrnSuprCreditFsaDetailAdd',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnSuprCreditFsaDetailAdd.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.AgrTrnSuprCompanyCrimeCheckRecordAPI', {
            url: '/AgrTrnSuprCompanyCrimeCheckRecordAPI',
            title: 'AgrTrnSuprCompanyCrimeCheckRecordAPI',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnSuprCompanyCrimeCheckRecordAPI.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.AgrTrnSuprCreditInstitutionBureauEdit', {
            url: '/AgrTrnSuprCreditInstitutionBureauEdit',
            title: 'AgrTrnSuprCreditInstitutionBureauEdit',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnSuprCreditInstitutionBureauEdit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
         })

        .state('app.AgrTrnSuprCreditInstitutionDtlAdd', {
            url: '/AgrTrnSuprCreditInstitutionDtlAdd',
            title: 'AgrTrnSuprCreditInstitutionDtlAdd',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnSuprCreditInstitutionDtlAdd.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
         })

        .state('app.AgrTrnSuprCreditInstitutionBureauView', {
             url: '/AgrTrnSuprCreditInstitutionBureauView',
             title: 'AgrTrnSuprCreditInstitutionBureauView',
             templateUrl: helper.basepath('ems.mastersamagro/AgrTrnSuprCreditInstitutionBureauView.html?ver=' + version + '"'),
             resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
         })

         .state('app.AgrSuprHighmarkReport', {
              url: '/AgrSuprHighmarkReport',
              title: 'AgrSuprHighmarkReport',
              templateUrl: helper.basepath('ems.mastersamagro/AgrSuprHighmarkReport.html?ver=' + version + '"'),
              resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
          })

         .state('app.AgrSuprTransUnionReport', {
              url: '/AgrSuprTransUnionReport',
              title: 'AgrSuprTransUnionReport',
              templateUrl: helper.basepath('ems.mastersamagro/AgrSuprTransUnionReport.html?ver=' + version + '"'),
              resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
          })

         .state('app.AgrTrnSuprCrimeReportCompanyView', {
              url: '/AgrTrnSuprCrimeReportCompanyView',
              title: 'AgrTrnSuprCrimeReportCompanyView',
              templateUrl: helper.basepath('ems.mastersamagro/AgrTrnSuprCrimeReportCompanyView.html?ver=' + version + '"'),
              resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
          })

         .state('app.AgrTrnSuprCAMGenerate', {
               url: '/AgrTrnSuprCAMGenerate',
               title: 'AgrTrnSuprCAMGenerate',
               templateUrl: helper.basepath('ems.mastersamagro/AgrTrnSuprCAMGenerate.html?ver=' + version + '"'),
               resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
          })

         .state('app.AgrMstSuprCadApplicationView', {
             url: '/AgrMstSuprCadApplicationView',
             title: 'AgrMstSuprCadApplicationView',
             templateUrl: helper.basepath('ems.mastersamagro/AgrMstSuprCadApplicationView.html?ver=' + version + '"'),
             resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
         })

         .state('app.AgrMstSuprApplicationLoanEdit', {
             url: '/AgrMstSuprApplicationLoanEdit',
             title: 'AgrMstSuprApplicationLoanEdit',
             templateUrl: helper.basepath('ems.mastersamagro/AgrMstSuprApplicationLoanEdit.html?ver=' + version + '"'),
             resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
         })

        .state('app.AgrTrnSuprRMDeferralDtlsView', {
            url: '/AgrTrnSuprRMDeferralDtlsView',
            title: 'AgrTrnSuprRMDeferralDtlsView',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnSuprRMDeferralDtlsView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.AgrTrnSuprCreditEconomicCapitalEdit', {
            url: '/AgrTrnSuprCreditEconomicCapitalEdit',
            title: 'AgrTrnSuprCreditEconomicCapitalEdit',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnSuprCreditEconomicCapitalEdit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

       .state('app.AgrTrnSuprCreditPSLDataFlaggingEdit', {
           url: '/AgrTrnSuprCreditPSLDataFlaggingEdit',
           title: 'AgrTrnSuprCreditPSLDataFlaggingEdit',
           templateUrl: helper.basepath('ems.mastersamagro/AgrTrnSuprCreditPSLDataFlaggingEdit.html?ver=' + version + '"'),
           resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
       })

       .state('app.AgrTrnSuprCreditSuppliersDtlEdit', {
           url: '/AgrTrnSuprCreditSuppliersDtlEdit',
           title: 'AgrTrnSuprCreditSuppliersDtlEdit',
           templateUrl: helper.basepath('ems.mastersamagro/AgrTrnSuprCreditSuppliersDtlEdit.html?ver=' + version + '"'),
           resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
       })

        .state('app.AgrTrnSuprCreditBuyerDtlEdit', {
            url: '/AgrTrnSuprCreditBuyerDtlEdit',
            title: 'AgrTrnSuprCreditBuyerDtlEdit',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnSuprCreditBuyerDtlEdit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.AgrTrnSuprCreditBankAccountDtlEdit', {
            url: '/AgrTrnSuprCreditBankAccountDtlEdit',
            title: 'AgrTrnSuprCreditBankAccountDtlEdit',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnSuprCreditBankAccountDtlEdit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.AgrTrnSuprCreditExistingBankDtlEdit', {
            url: '/AgrTrnSuprCreditExistingBankDtlEdit',
            title: 'AgrTrnSuprCreditExistingBankDtlEdit',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnSuprCreditExistingBankDtlEdit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.AgrTrnSuprCreditRepaymentDtlEdit', {
            url: '/AgrTrnSuprCreditRepaymentDtlEdit',
            title: 'AgrTrnSuprCreditRepaymentDtlEdit',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnSuprCreditRepaymentDtlEdit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.AgrTrnSuprCreditIndividualBankAcctEdit', {
            url: '/AgrTrnSuprCreditIndividualBankAcctEdit',
            title: 'AgrTrnSuprCreditIndividualBankAcctEdit',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnSuprCreditIndividualBankAcctEdit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.AgrTrnSuprCreditIndividualExistingBankEdit', {
            url: '/AgrTrnSuprCreditIndividualExistingBankEdit',
            title: 'AgrTrnSuprCreditIndividualExistingBankEdit',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnSuprCreditIndividualExistingBankEdit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.AgrTrnSuprCreditIndividualPSLDataFlagEdit', {
            url: '/AgrTrnSuprCreditIndividualPSLDataFlagEdit',
            title: 'AgrTrnSuprCreditIndividualPSLDataFlagEdit',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnSuprCreditIndividualPSLDataFlagEdit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.AgrTrnSuprCreditUnderwritingKycLogView', {
            url: '/AgrTrnSuprCreditUnderwritingKycLogView',
            title: 'AgrTrnSuprCreditUnderwritingKycLogView',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnSuprCreditUnderwritingKycLogView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.AgrTrnSuprCreditServicesDtlEdit', {
            url: '/AgrTrnSuprCreditServicesDtlEdit',
            title: 'AgrTrnSuprCreditServicesDtlEdit',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnSuprCreditServicesDtlEdit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

         //moved to test
         .state('app.AgrMstSuprCadChequeManagementSummary', {
             url: '/AgrMstSuprCadChequeManagementSummary',
             title: 'AgrMstSuprCadChequeManagementSummary',
             templateUrl: helper.basepath('ems.mastersamagro/AgrMstSuprCadChequeManagementSummary.html?ver=' + version + '"'),
             resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
         })

        .state('app.AgrMstSuprCadChequeMgmtCheckerSummary', {
            url: '/AgrMstSuprCadChequeMgmtCheckerSummary',
            title: 'AgrMstSuprCadChequeMgmtCheckerSummary',
            templateUrl: helper.basepath('ems.mastersamagro/AgrMstSuprCadChequeMgmtCheckerSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.AgrMstSuprCadChequeMgmtApprovalSummary', {
            url: '/AgrMstSuprCadChequeMgmtApprovalSummary',
            title: 'AgrMstSuprCadChequeMgmtApprovalSummary',
            templateUrl: helper.basepath('ems.mastersamagro/AgrMstSuprCadChequeMgmtApprovalSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.AgrMstSuprChequeApprovalCompleted', {
            url: '/AgrMstSuprChequeApprovalCompleted',
            title: 'AgrMstSuprChequeApprovalCompleted',
            templateUrl: helper.basepath('ems.mastersamagro/AgrMstSuprChequeApprovalCompleted.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.AgrMstSuprUDCMakerSummary', {
            url: '/AgrMstSuprUDCMakerSummary',
            title: 'AgrMstSuprUDCMakerSummary',
            templateUrl: helper.basepath('ems.mastersamagro/AgrMstSuprUDCMakerSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.AgrMstSuprChequeMakerFollowDtls', {
            url: '/AgrMstSuprChequeMakerFollowDtls',
            title: 'AgrMstSuprChequeMakerFollowDtls',
            templateUrl: helper.basepath('ems.mastersamagro/AgrMstSuprChequeMakerFollowDtls.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.AgrMstSuprUDCMakerAdd', {
            url: '/AgrMstSuprUDCMakerAdd',
            title: 'AgrMstSuprUDCMakerAdd',
            templateUrl: helper.basepath('ems.mastersamagro/AgrMstSuprUDCMakerAdd.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.AgrMstSuprUDCMakerEditCheque', {
            url: '/AgrMstSuprUDCMakerEditCheque',
            title: 'AgrMstSuprUDCMakerEditCheque',
            templateUrl: helper.basepath('ems.mastersamagro/AgrMstSuprUDCMakerEditCheque.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.AgrMstSuprUDCMakerView', {
            url: '/AgrMstSuprUDCMakerView',
            title: 'AgrMstSuprUDCMakerView',
            templateUrl: helper.basepath('ems.mastersamagro/AgrMstSuprUDCMakerView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

         .state('app.AgrMstTypeofSupply', {
             url: '/AgrMstTypeofSupply',
             title: 'AgrMstTypeofSupply',
             templateUrl: helper.basepath('ems.mastersamagro/AgrMstTypeofSupply.html?ver=' + version + '"'),
             resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.AgrMstSectorClassification', {
            url: '/AgrMstSectorClassification',
            title: 'AgrMstSectorClassification',
            templateUrl: helper.basepath('ems.mastersamagro/AgrMstSectorClassification.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.AgrMstTypeofWarehouse', {
            url: '/AgrMstTypeofWarehouse',
            title: 'AgrMstTypeofWarehouse',
            templateUrl: helper.basepath('ems.mastersamagro/AgrMstTypeofWarehouse.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.AgrMstProductDeskMapping', {
            url: '/AgrMstProductDeskMapping',
            title: 'AgrMstProductDeskMapping',
            templateUrl: helper.basepath('ems.mastersamagro/AgrMstProductDeskMapping.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
        
        //27th July 2022--committed
        .state('app.AgrMstInsuranceCompany', {
            url: '/AgrMstInsuranceCompany',
            title: 'AgrMstInsuranceCompany',
            templateUrl: helper.basepath('ems.mastersamagro/AgrMstInsuranceCompany.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.AgrMstInsuranceCompanyAdd', {
            url: '/AgrMstInsuranceCompanyAdd',
            title: 'AgrMstInsuranceCompanyAdd',
            templateUrl: helper.basepath('ems.mastersamagro/AgrMstInsuranceCompanyAdd.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
        
        .state('app.AgrMstInsuranceCompanyEdit', {
            url: '/AgrMstInsuranceCompanyEdit',
            title: 'AgrMstInsuranceCompanyEdit',
            templateUrl: helper.basepath('ems.mastersamagro/AgrMstInsuranceCompanyEdit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.AgrMstInsuranceCompanyView', {
            url: '/AgrMstInsuranceCompanyView',
            title: 'AgrMstInsuranceCompanyView',
            templateUrl: helper.basepath('ems.mastersamagro/AgrMstInsuranceCompanyView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        //28th July 2022 --committed
        .state('app.AgrTrnApplCCApproved', {
            url: '/AgrTrnApplCCApproved',
            title: 'AgrTrnApplCCApproved',
            templateUrl: helper.basepath('ems.mastersamagro/AgrTrnApplCCApproved.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
    
        .state('app.AgrMstSupplierApplicationReport', {
            url: '/AgrMstSupplierApplicationReport',
            title: 'AgrMstSupplierApplicationReport',
            templateUrl: helper.basepath('ems.mastersamagro/AgrMstSupplierApplicationReport.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
    
        .state('app.AgrMstSupplierCCReport', {
                url: '/AgrMstSupplierCCReport',
                title: 'AgrMstSupplierCCReport',
                templateUrl: helper.basepath('ems.mastersamagro/AgrMstSupplierCCReport.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
            })
    
         .state('app.AgrMstSupplierCreditAllocationReport', {
                url: '/AgrMstSupplierCreditAllocationReport',
                title: 'AgrMstSupplierCreditAllocationReport',
                templateUrl: helper.basepath('ems.mastersamagro/AgrMstSupplierCreditAllocationReport.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
         })

         .state('app.AgrMstProductPendingAssignmentSummary', {
             url: '/AgrMstProductPendingAssignmentSummary',
             title: 'AgrMstProductPendingAssignmentSummary',
             templateUrl: helper.basepath('ems.mastersamagro/AgrMstProductPendingAssignmentSummary.html?ver=' + version + '"'),
             resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
         })

         .state('app.AgrMstProductAssignedSummary', {
             url: '/AgrMstProductAssignedSummary',
             title: 'AgrMstProductAssignedSummary',
             templateUrl: helper.basepath('ems.mastersamagro/AgrMstProductAssignedSummary.html?ver=' + version + '"'),
             resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
         })

         .state('app.AgrProductDescVisitReportAdd', {
             url: '/AgrProductDescVisitReportAdd',
             title: 'AgrProductDescVisitReportAdd',
             templateUrl: helper.basepath('ems.mastersamagro/AgrProductDescVisitReportAdd.html?ver=' + version + '"'),
             resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
         })

         .state('app.AgrMstProductDescVisitReportEdit', {
             url: '/AgrMstProductDescVisitReportEdit',
             title: 'AgrMstProductDescVisitReportEdit',
             templateUrl: helper.basepath('ems.mastersamagro/AgrMstProductDescVisitReportEdit.html?ver=' + version + '"'),
             resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
         })

          .state('app.AgrMstProductDescVisitReportView', {
              url: '/AgrMstProductDescVisitReportView',
              title: 'AgrMstProductDescVisitReportView',
              templateUrl: helper.basepath('ems.mastersamagro/AgrMstProductDescVisitReportView.html?ver=' + version + '"'),
              resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
          })

         .state('app.AgrMstProductMyAssignmentSummary', {
             url: '/AgrMstProductMyAssignmentSummary',
             title: 'AgrMstProductMyAssignmentSummary',
             templateUrl: helper.basepath('ems.mastersamagro/AgrMstProductMyAssignmentSummary.html?ver=' + version + '"'),
             resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
         })

           .state('app.AgrMstProducDesctVerification', {
               url: '/AgrMstProducDesctVerification',
               title: 'AgrMstProducDesctVerification',
               templateUrl: helper.basepath('ems.mastersamagro/AgrMstProducDesctVerification.html?ver=' + version + '"'),
               resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
           })

           .state('app.AgrMstProductDescQueryStatus', {
               url: '/AgrMstProductDescQueryStatus',
               title: 'AgrMstProductDescQueryStatus',
               templateUrl: helper.basepath('ems.mastersamagro/AgrMstProductDescQueryStatus.html?ver=' + version + '"'),
               resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
           })

         .state('app.AgrMstProductSubmittedtoApprovalSummary', {
             url: '/AgrMstProductSubmittedtoApprovalSummary',
             title: 'AgrMstProductSubmittedtoApprovalSummary',
             templateUrl: helper.basepath('ems.mastersamagro/AgrMstProductSubmittedtoApprovalSummary.html?ver=' + version + '"'),
             resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
         })

         .state('app.AgrMstProductRejectedApplSummary', {
             url: '/AgrMstProductRejectedApplSummary',
             title: 'AgrMstProductRejectedApplSummary',
             templateUrl: helper.basepath('ems.mastersamagro/AgrMstProductRejectedApplSummary.html?ver=' + version + '"'),
             resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
         })

         .state('app.AgrMstProductcDescApprovalSummary', {
             url: '/AgrMstProductcDescApprovalSummary',
             title: 'AgrMstProductcDescApprovalSummary',
             templateUrl: helper.basepath('ems.mastersamagro/AgrMstProductcDescApprovalSummary.html?ver=' + version + '"'),
             resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
         })

         .state('app.AgrTrnProductDescApproval', {
             url: '/AgrTrnProductDescApproval',
             title: 'AgrTrnProductDescApproval',
             templateUrl: helper.basepath('ems.mastersamagro/AgrTrnProductDescApproval.html?ver=' + version + '"'),
             resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
         })

         .state('app.AgrTrnProductDescApprovedSummary', {
             url: '/AgrTrnProductDescApprovedSummary',
             title: 'AgrTrnProductDescApprovedSummary',
             templateUrl: helper.basepath('ems.mastersamagro/AgrTrnProductDescApprovedSummary.html?ver=' + version + '"'),
             resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
         })

         .state('app.AgrTrnProductDescRejectandHoldSummary', {
             url: '/AgrTrnProductDescRejectandHoldSummary',
             title: 'AgrTrnProductDescRejectandHoldSummary',
             templateUrl: helper.basepath('ems.mastersamagro/AgrTrnProductDescRejectandHoldSummary.html?ver=' + version + '"'),
             resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
         })
        
           .state('app.AgrMstCustomerOnboardingInfoAdd', {
               url: '/AgrMstCustomerOnboardingInfoAdd',
               title: 'AgrMstCustomerOnboardingInfoAdd',
               templateUrl: helper.basepath('ems.mastersamagro/AgrMstCustomerOnboardingInfoAdd.html?ver=' + version + '"'),
               resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
           })

         .state('app.AgrMstCustomerOnboardingApproval', {
             url: '/AgrMstCustomerOnboardingApproval',
             title: 'AgrMstCustomerOnboardingApproval',
             templateUrl: helper.basepath('ems.mastersamagro/AgrMstCustomerOnboardingApproval.html?ver=' + version + '"'),
             resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
         })

         .state('app.AgrMstSupplierOnboardingApproval', {
             url: '/AgrMstSupplierOnboardingApproval',
             title: 'AgrMstSupplierOnboardingApproval',
             templateUrl: helper.basepath('ems.mastersamagro/AgrMstSupplierOnboardingApproval.html?ver=' + version + '"'),
             resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
         })

 	      .state('app.AgrMstOnboardingApprovalCompleted', {
 	          url: '/AgrMstOnboardingApprovalCompleted',
 	          title: 'AgrMstOnboardingApprovalCompleted',
 	          templateUrl: helper.basepath('ems.mastersamagro/AgrMstOnboardingApprovalCompleted.html?ver=' + version + '"'),
 	          resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
 	      })

          .state('app.AgrMstCustomerApprovalSummary', {
               url: '/AgrMstCustomerApprovalSummary',
               title: 'AgrMstCustomerApprovalSummary',
               templateUrl: helper.basepath('ems.mastersamagro/AgrMstCustomerApprovalSummary.html?ver=' + version + '"'),
               resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
          })

          .state('app.AgrMstCustomerOnboardingSummary', {
              url: '/AgrMstCustomerOnboardingSummary',
              title: 'AgrMstCustomerOnboardingSummary',
              templateUrl: helper.basepath('ems.mastersamagro/AgrMstCustomerOnboardingSummary.html?ver=' + version + '"'),
              resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
          })

         .state('app.AgrMstSupplierOnboardingInfoAdd', {
             url: '/AgrMstSupplierOnboardingInfoAdd',
             title: 'AgrMstSupplierOnboardingInfoAdd',
             templateUrl: helper.basepath('ems.mastersamagro/AgrMstSupplierOnboardingInfoAdd.html?ver=' + version + '"'),
             resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
         })

          .state('app.AgrMstCustomerOnboardRejectedSummary', {
             url: '/AgrMstCustomerOnboardRejectedSummary',
             title: 'AgrMstCustomerOnboardRejectedSummary',
             templateUrl: helper.basepath('ems.mastersamagro/AgrMstCustomerOnboardRejectedSummary.html?ver=' + version + '"'),
             resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
          })

         .state('app.AgrMstbyrOnboardInfoEdit', {
             url: '/AgrMstbyrOnboardInfoEdit',
             title: 'AgrMstbyrOnboardInfoEdit',
             templateUrl: helper.basepath('ems.mastersamagro/AgrMstbyrOnboardInfoEdit.html?ver=' + version + '"'),
             resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
         })

        .state('app.AgrMstSuprOnboardInfoEdit', {
            url: '/AgrMstSuprOnboardInfoEdit',
            title: 'AgrMstSuprOnboardInfoEdit',
            templateUrl: helper.basepath('ems.mastersamagro/AgrMstSuprOnboardInfoEdit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

         .state('app.AgrMstBuyerApprovedSummary', {
             url: '/AgrMstBuyerApprovedSummary',
             title: 'AgrMstBuyerApprovedSummary',
             templateUrl: helper.basepath('ems.mastersamagro/AgrMstBuyerApprovedSummary.html?ver=' + version + '"'),
             resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
         })

        .state('app.AgrMstSupplierApprovedSummary', {
             url: '/AgrMstSupplierApprovedSummary',
             title: 'AgrMstSupplierApprovedSummary',
             templateUrl: helper.basepath('ems.mastersamagro/AgrMstSupplierApprovedSummary.html?ver=' + version + '"'),
             resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

         .state('app.AgrMstByrOnboardApprovalEdit', {
             url: '/AgrMstByrOnboardApprovalEdit',
             title: 'AgrMstByrOnboardApprovalEdit',
             templateUrl: helper.basepath('ems.mastersamagro/AgrMstByrOnboardApprovalEdit.html?ver=' + version + '"'),
             resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
         })

        .state('app.AgrMstSuprOnboardApprovalEdit', {
             url: '/AgrMstSuprOnboardApprovalEdit',
             title: 'AgrMstSuprOnboardApprovalEdit',
             templateUrl: helper.basepath('ems.mastersamagro/AgrMstSuprOnboardApprovalEdit.html?ver=' + version + '"'),
             resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
        .state('app.AgrMstInitiateApplication', {
            url: '/AgrMstInitiateApplication',
            title: 'AgrMstInitiateApplication',
            templateUrl: helper.basepath('ems.mastersamagro/AgrMstInitiateApplication.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
       })
 
         .state('app.AgrMstOnboardingApplicationInfo', {
             url: '/AgrMstOnboardingApplicationInfo',
             title: 'AgrMstOnboardingApplicationInfo',
             templateUrl: helper.basepath('ems.mastersamagro/AgrMstOnboardingApplicationInfo.html?ver=' + version + '"'),
             resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
         })
   
         .state('app.AgrMstCreditorMasterApproval', {
             url: '/AgrMstCreditorMasterApproval',
             title: 'AgrMstCreditorMasterApproval',
             templateUrl: helper.basepath('ems.mastersamagro/AgrMstCreditorMasterApproval.html?ver=' + version + '"'),
             resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
         })

             .state('app.AgrMstByrOnboardApprovedEdit', {
                 url: '/AgrMstByrOnboardApprovedEdit',
                 title: 'AgrMstByrOnboardApprovedEdit',
                 templateUrl: helper.basepath('ems.mastersamagro/AgrMstByrOnboardApprovedEdit.html?ver=' + version + '"'),
             resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
         })

             .state('app.AgrMstSuprOnboardApprovedEdit', {
                 url: '/AgrMstSuprOnboardApprovedEdit',
                 title: 'AgrMstSuprOnboardApprovedEdit',
                 templateUrl: helper.basepath('ems.mastersamagro/AgrMstSuprOnboardApprovedEdit.html?ver=' + version + '"'),
                 resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
             })

        .state('app.AgrMstBuyerSupplierType', {
            url: '/AgrMstBuyerSupplierType',
            title: 'AgrMstBuyerSupplierType',
            templateUrl: helper.basepath('ems.mastersamagro/AgrMstBuyerSupplierType.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.AgrRptCadDeferral', {
                 url: '/AgrRptCadDeferral',
                 title: 'AgrRptCadDeferral',
                 templateUrl: helper.basepath('ems.mastersamagro/AgrRptCadDeferral.html?ver=' + version + '"'),
                 resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
         })

          .state('app.AgrRptCadCovenant', {
                 url: '/AgrRptCadCovenant',
                 title: 'AgrRptCadCovenant',
                 templateUrl: helper.basepath('ems.mastersamagro/AgrRptCadCovenant.html?ver=' + version + '"'),
                 resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
          })

        .state('app.AgrDocumentChecklistApprovalReport', {
                url: '/AgrDocumentChecklistApprovalReport',
                title: 'AgrDocumentChecklistApprovalReport',
                templateUrl: helper.basepath('ems.mastersamagro/AgrDocumentChecklistApprovalReport.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })

            .state('app.AgrDocumentChecklistCheckerReport', {
                url: '/AgrDocumentChecklistCheckerReport',
                title: 'AgrDocumentChecklistCheckerReport',
                templateUrl: helper.basepath('ems.mastersamagro/AgrDocumentChecklistCheckerReport.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
            })

            .state('app.AgrDocumentChecklistMakerReport', {
                url: '/AgrDocumentChecklistMakerReport',
                title: 'AgrDocumentChecklistMakerReport',
                templateUrl: helper.basepath('ems.mastersamagro/AgrDocumentChecklistMakerReport.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
            })
            .state('app.AgrMstCadScannedFollowupSummary', {
                url: '/AgrMstCadScannedFollowupSummary',
                title: 'AgrMstCadScannedFollowupSummary',
                templateUrl: helper.basepath('ems.mastersamagro/AgrMstCadScannedFollowupSummary.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')      
            })
                
           .state('app.AgrTrnCadPhysicalDocFollowupSummary', {
                 url: '/AgrTrnCadPhysicalDocFollowupSummary',
                 title: 'AgrTrnCadPhysicalDocFollowupSummary',
                 templateUrl: helper.basepath('ems.mastersamagro/AgrTrnCadPhysicalDocFollowupSummary.html?ver=' + version + '"'),
                 resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')  
            })

             .state('app.AgrRptCadScannedDeferralCovenantDtls', {
                 url: '/AgrRptCadScannedDeferralCovenantDtls',
                 title: 'AgrRptCadScannedDeferralCovenantDtls',
                 templateUrl: helper.basepath('ems.mastersamagro/AgrRptCadScannedDeferralCovenantDtls.html?ver=' + version + '"'),
                 resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
             })

             .state('app.AgrRptCadApplicationDeferralCovenantView', {
                 url: '/AgrRptCadApplicationDeferralCovenantView',
                 title: 'AgrRptCadApplicationDeferralCovenantView',
                 templateUrl: helper.basepath('ems.mastersamagro/AgrRptCadApplicationDeferralCovenantView.html?ver=' + version + '"'),
                 resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
             })

             .state('app.AgrRptCadScannedDocchecklist', {
                 url: '/AgrRptCadScannedDocchecklist',
                 title: 'AgrRptCadScannedDocchecklist',
                 templateUrl: helper.basepath('ems.mastersamagro/AgrRptCadScannedDocchecklist.html?ver=' + version + '"'),
                 resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
             })

             .state('app.AgrRptCadScannedQuery', {
                 url: '/AgrRptCadScannedQuery',
                 title: 'AgrRptCadScannedQuery',
                 templateUrl: helper.basepath('ems.mastersamagro/AgrRptCadScannedQuery.html?ver=' + version + '"'),
                 resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
             })

             .state('app.AgrRptScannedDeferralHistory', {
                 url: '/AgrRptScannedDeferralHistory',
                 title: 'AgrRptScannedDeferralHistory',
                 templateUrl: helper.basepath('ems.mastersamagro/AgrRptScannedDeferralHistory.html?ver=' + version + '"'),
                 resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
             })

             .state('app.AgrRptCadPhysicalDeferralCovenantDtls', {
                 url: '/AgrRptCadPhysicalDeferralCovenantDtls',
                 title: 'AgrRptCadPhysicalDeferralCovenantDtls',
                 templateUrl: helper.basepath('ems.mastersamagro/AgrRptCadPhysicalDeferralCovenantDtls.html?ver=' + version + '"'),
                 resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
             })

             .state('app.AgrRptCadPhysicalDocchecklist', {
                 url: '/AgrRptCadPhysicalDocchecklist',
                 title: 'AgrRptCadPhysicalDocchecklist',
                 templateUrl: helper.basepath('ems.mastersamagro/AgrRptCadPhysicalDocchecklist.html?ver=' + version + '"'),
                 resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
             })

             .state('app.AgrRptCadPhysicalQuery', {
                 url: '/AgrRptCadPhysicalQuery',
                 title: 'AgrRptCadPhysicalQuery',
                 templateUrl: helper.basepath('ems.mastersamagro/AgrRptCadPhysicalQuery.html?ver=' + version + '"'),
                 resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
             })

             .state('app.AgrRptPhysicalDeferralHistory', {
                 url: '/AgrRptPhysicalDeferralHistory',
                 title: 'AgrRptPhysicalDeferralHistory',
                 templateUrl: helper.basepath('ems.mastersamagro/AgrRptPhysicalDeferralHistory.html?ver=' + version + '"'),
                 resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
             })

             .state('app.AgrRptScannedDeferralStatus', {
                 url: '/AgrRptScannedDeferralStatus',
                 title: 'AgrRptScannedDeferralStatus',
                 templateUrl: helper.basepath('ems.mastersamagro/AgrRptScannedDeferralStatus.html?ver=' + version + '"'),
                 resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
             })


             .state('app.AgrRptPhysicalDeferralStatus', {
                 url: '/AgrRptPhysicalDeferralStatus',
                 title: 'AgrRptPhysicalDeferralStatus',
                 templateUrl: helper.basepath('ems.mastersamagro/AgrRptPhysicalDeferralStatus.html?ver=' + version + '"'),
                 resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
             })

             .state('app.AgrRptCadCovenantChecker', {
                 url: '/AgrRptCadCovenantChecker',
                 title: 'AgrRptCadCovenantChecker',
                 templateUrl: helper.basepath('ems.mastersamagro/AgrRptCadCovenantChecker.html?ver=' + version + '"'),
                 resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
             })
             .state('app.AgrRptCadCovenantApproval', {
                 url: '/AgrRptCadCovenantApproval',
                 title: 'AgrRptCadCovenantApproval',
                 templateUrl: helper.basepath('ems.mastersamagro/AgrRptCadCovenantApproval.html?ver=' + version + '"'),
                 resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
             })

             .state('app.AgrMstCCApprovedSummary', {
                 url: '/AgrMstCCApprovedSummary',
                 title: 'AgrMstCCApprovedSummary',
                 templateUrl: helper.basepath('ems.mastersamagro/AgrMstCCApprovedSummary.html?ver=' + version + '"'),
                 resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
             })
             .state('app.AgrMstSuprCCApprovedSummary', {
                url: '/AgrMstSuprCCApprovedSummary',
                title: 'AgrMstSuprCCApprovedSummary',
                templateUrl: helper.basepath('ems.mastersamagro/AgrMstSuprCCApprovedSummary.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })

             .state('app.AgrMstAmendmentSummary', {
                 url: '/AgrMstAmendmentSummary',
                 title: 'AgrMstAmendmentSummary',
                 templateUrl: helper.basepath('ems.mastersamagro/AgrMstAmendmentSummary.html?ver=' + version + '"'),
                 resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
             })

             .state('app.AgrMstRenewalApplicationAdd', {
                 url: '/AgrMstRenewalApplicationAdd',
                 title: 'AgrMstRenewalApplicationAdd',
                 templateUrl: helper.basepath('ems.mastersamagro/AgrMstRenewalApplicationAdd.html?ver=' + version + '"'),
                 resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
             })

             .state('app.AgrMstWarehouseReport', {
                 url: '/AgrMstWarehouseReport',
                 title: 'AgrMstWarehouseReport',
                 templateUrl: helper.basepath('ems.mastersamagro/AgrMstWarehouseReport.html?ver=' + version + '"'),
                 resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')

             })

             .state('app.AgrMstOtherCreditorReport', {
                 url: '/AgrMstOtherCreditorReport',
                 title: 'AgrMstOtherCreditorReport',
                 templateUrl: helper.basepath('ems.mastersamagro/AgrMstOtherCreditorReport.html?ver=' + version + '"'),
                 resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')

             })

             .state('app.AgrMstShortClosing', {
                 url: '/AgrMstShortClosing',
                 title: 'AgrMstShortClosing',
                 templateUrl: helper.basepath('ems.mastersamagro/AgrMstShortClosing.html?ver=' + version + '"'),
                 resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
             })

             .state('app.AgrTrnProductDeskAutoApprovalSummary', {
                 url: '/AgrTrnProductDeskAutoApprovalSummary',
                 title: 'AgrTrnProductDeskAutoApprovalSummary',
                 templateUrl: helper.basepath('ems.mastersamagro/AgrTrnProductDeskAutoApprovalSummary.html?ver=' + version + '"'),
                 resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
             })

             .state('app.AgrTrnCreditAutoApprovalSummary', {
                 url: '/AgrTrnCreditAutoApprovalSummary',
                 title: 'AgrTrnCreditAutoApprovalSummary',
                 templateUrl: helper.basepath('ems.mastersamagro/AgrTrnCreditAutoApprovalSummary.html?ver=' + version + '"'),
                 resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
             })

             .state('app.AgrTrnACAutoApprovalSummary', {
                 url: '/AgrTrnACAutoApprovalSummary',
                 title: 'AgrTrnACAutoApprovalSummary',
                 templateUrl: helper.basepath('ems.mastersamagro/AgrTrnACAutoApprovalSummary.html?ver=' + version + '"'),
                 resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
             })

             .state('app.AgrTrnPmgAdvanceRejectedSummary', {
                 url: '/AgrTrnPmgAdvanceRejectedSummary',
                 title: 'AgrTrnPmgAdvanceRejectedSummary',
                 templateUrl: helper.basepath('ems.mastersamagro/AgrTrnPmgAdvanceRejectedSummary.html?ver=' + version + '"'),
                 resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
             })

             .state('app.AgrTrnPMGDeferralDochecklist', {
                 url: '/AgrTrnPMGDeferralDochecklist',
                 title: 'AgrTrnPMGDeferralDochecklist',
                 templateUrl: helper.basepath('ems.mastersamagro/AgrTrnPMGDeferralDochecklist.html?ver=' + version + '"'),
                 resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
             })              
            .state('app.AgrTrnPMGDeferralCloseQuery', {
                 url: '/AgrTrnPMGDeferralCloseQuery',
                 title: 'AgrTrnPMGDeferralCloseQuery',
                 templateUrl: helper.basepath('ems.mastersamagro/AgrTrnPMGDeferralCloseQuery.html?ver=' + version + '"'),
                 resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
             })              
            .state('app.AgrTrnPMGDeferralQuery', {
                 url: '/AgrTrnPMGDeferralQuery',
                 title: 'AgrTrnPMGDeferralQuery',
                 templateUrl: helper.basepath('ems.mastersamagro/AgrTrnPMGDeferralQuery.html?ver=' + version + '"'),
                 resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
             })              
             .state('app.AgrTrnPMGDeferralStatus', {
                 url: '/AgrTrnPMGDeferralStatus',
                 title: 'AgrTrnPMGDeferralStatus',
                 templateUrl: helper.basepath('ems.mastersamagro/AgrTrnPMGDeferralStatus.html?ver=' + version + '"'),
                 resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
             }) 
             .state('app.AgrTrnBuyerDeferralMyApproval', {
                 url: '/AgrTrnBuyerDeferralMyApproval',
                 title: 'AgrTrnBuyerDeferralMyApproval',
                 templateUrl: helper.basepath('ems.mastersamagro/AgrTrnBuyerDeferralMyApproval.html?ver=' + version + '"'),
                 resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
             })
             .state('app.AgrIncompleteStageSummary', {
                 url: '/AgrIncompleteStageSummary',
                 title: 'AgrIncompleteStageSummary',
                 templateUrl: helper.basepath('ems.mastersamagro/AgrIncompleteStageSummary.html?ver=' + version + '"'),
                 resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
             })
             .state('app.AgrMstBusinessRejectRevoke', {
                 url: '/AgrMstBusinessRejectRevoke',
                 title: 'AgrMstBusinessRejectRevoke',
                 templateUrl: helper.basepath('ems.mastersamagro/AgrMstBusinessRejectRevoke.html?ver=' + version + '"'),
                 resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
             })
             .state('app.AgrMstBusinessRevokeHistory', {
                 url: '/AgrMstBusinessRevokeHistory',
                 title: 'AgrMstBusinessRevokeHistory',
                 templateUrl: helper.basepath('ems.mastersamagro/AgrMstBusinessRevokeHistory.html?ver=' + version + '"'),
                 resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
             })
             .state('app.AgrMstBusinessRevokedApplSummary', {
                 url: '/AgrMstBusinessRevokedApplSummary',
                 title: 'AgrMstBusinessRevokedApplSummary',
                 templateUrl: helper.basepath('ems.mastersamagro/AgrMstBusinessRevokedApplSummary.html?ver=' + version + '"'),
                 resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
             })
             .state('app.AgrMstBusinessHoldRevokeSummary', {
                 url: '/AgrMstBusinessHoldRevokeSummary',
                 title: 'AgrMstBusinessHoldRevokeSummary',
                 templateUrl: helper.basepath('ems.mastersamagro/AgrMstBusinessHoldRevokeSummary.html?ver=' + version + '"'),
                 resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
             })
             .state('app.AgrMstCreditRevokeSummary', {
                 url: '/AgrMstCreditRevokeSummary',
                 title: 'AgrMstCreditRevokeSummary',
                 templateUrl: helper.basepath('ems.mastersamagro/AgrMstCreditRevokeSummary.html?ver=' + version + '"'),
                 resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
             })

             .state('app.AgrMstBusinessRevokeSummary', {
                 url: '/AgrMstBusinessRevokeSummary',
                 title: 'AgrMstBusinessRevokeSummary',
                 templateUrl: helper.basepath('ems.mastersamagro/AgrMstBusinessRevokeSummary.html?ver=' + version + '"'),
                 resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
             })

             .state('app.AgrMstCreditRejectHoldRevoke', {
                 url: '/AgrMstCreditRejectHoldRevoke',
                 title: 'AgrMstCreditRejectHoldRevoke',
                 templateUrl: helper.basepath('ems.mastersamagro/AgrMstCreditRejectHoldRevoke.html?ver=' + version + '"'),
                 resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
             })

             .state('app.AgrMstCreditHoldRevokeSummary', {
                 url: '/AgrMstCreditHoldRevokeSummary',
                 title: 'AgrMstCreditHoldRevokeSummary',
                 templateUrl: helper.basepath('ems.mastersamagro/AgrMstCreditHoldRevokeSummary.html?ver=' + version + '"'),
                 resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
             })

             .state('app.AgrMstCreditRevokedApplSummary', {
                 url: '/AgrMstCreditRevokedApplSummary',
                 title: 'AgrMstCreditRevokedApplSummary',
                 templateUrl: helper.basepath('ems.mastersamagro/AgrMstCreditRevokedApplSummary.html?ver=' + version + '"'),
                 resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
             })

             .state('app.AgrMstCreditRejectHoldRevokeHistory', {
                 url: '/AgrMstCreditRejectHoldRevokeHistory',
                 title: 'AgrMstCreditRejectHoldRevokeHistory',
                 templateUrl: helper.basepath('ems.mastersamagro/AgrMstCreditRejectHoldRevokeHistory.html?ver=' + version + '"'),
                 resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
             })

             .state('app.AgrMstBusinessHierarchyUpdateSummary', {
                 url: '/AgrMstBusinessHierarchyUpdateSummary',
                 title: 'AgrMstBusinessHierarchyUpdateSummary',
                 templateUrl: helper.basepath('ems.mastersamagro/AgrMstBusinessHierarchyUpdateSummary.html?ver=' + version + '"'),
                 resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
             })

             .state('app.AgrMstCreditStageSummary', {
                 url: '/AgrMstCreditStageSummary',
                 title: 'AgrMstCreditStageSummary',
                 templateUrl: helper.basepath('ems.mastersamagro/AgrMstCreditStageSummary.html?ver=' + version + '"'),
                 resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
             })

             .state('app.AgrMstCcStageSummary', {
                 url: '/AgrMstCcStageSummary',
                 title: 'AgrMstCcStageSummary',
                 templateUrl: helper.basepath('ems.mastersamagro/AgrMstCcStageSummary.html?ver=' + version + '"'),
                 resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
             })

             .state('app.AgrMstCadPendingStageSummary', {
                 url: '/AgrMstCadPendingStageSummary',
                 title: 'AgrMstCadPendingStageSummary',
                 templateUrl: helper.basepath('ems.mastersamagro/AgrMstCadPendingStageSummary.html?ver=' + version + '"'),
                 resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
             })

             .state('app.AgrMstCadAcceptedStageSummary', {
                 url: '/AgrMstCadAcceptedStageSummary',
                 title: 'AgrMstCadAcceptedStageSummary',
                 templateUrl: helper.basepath('ems.mastersamagro/AgrMstCadAcceptedStageSummary.html?ver=' + version + '"'),
                 resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
             })

             .state('app.AgrMstBusinessHierarchyUpdate', {
                 url: '/AgrMstBusinessHierarchyUpdate',
                 title: 'AgrMstBusinessHierarchyUpdate',
                 templateUrl: helper.basepath('ems.mastersamagro/AgrMstBusinessHierarchyUpdate.html?ver=' + version + '"'),
                 resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
             })

             .state('app.AgrMstBusinessHierarchyUpdateHistory', {
                 url: '/AgrMstBusinessHierarchyUpdateHistory',
                 title: 'AgrMstBusinessHierarchyUpdateHistory',
                 templateUrl: helper.basepath('ems.mastersamagro/AgrMstBusinessHierarchyUpdateHistory.html?ver=' + version + '"'),
                 resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
             })
             .state('app.AgrMstSuprCreditVisitReportEdit', {
                 url: '/AgrMstSuprCreditVisitReportEdit',
                 title: 'AgrMstSuprCreditVisitReportEdit',
                 templateUrl: helper.basepath('ems.mastersamagro/AgrMstSuprCreditVisitReportEdit.html?ver=' + version + '"'),
                 resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
             })              
	        .state('app.AgrMstSuprCreditVisitReportView', {
                 url: '/AgrMstSuprCreditVisitReportView',
                 title: 'AgrMstSuprCreditVisitReportView',
                 templateUrl: helper.basepath('ems.mastersamagro/AgrMstSuprCreditVisitReportView.html?ver=' + version + '"'),
                 resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
             })
             .state('app.AgrTrnUpcomingCreditApprovalSummary', {
                 url: '/AgrTrnUpcomingCreditApprovalSummary',
                 title: 'AgrTrnUpcomingCreditApprovalSummary',
                 templateUrl: helper.basepath('ems.mastersamagro/AgrTrnUpcomingCreditApprovalSummary.html?ver=' + version + '"'),
                 resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
             })

             .state('app.AgrMstUpcomingBusinessApprovalSummary', {
                 url: '/AgrMstUpcomingBusinessApprovalSummary',
                 title: 'AgrMstUpcomingBusinessApprovalSummary',
                 templateUrl: helper.basepath('ems.mastersamagro/AgrMstUpcomingBusinessApprovalSummary.html?ver=' + version + '"'),
                 resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
             })

            .state('app.AgrMstProductRevokeSummary', {
                url: '/AgrMstProductRevokeSummary',
                title: 'AgrMstProductRevokeSummary',
                templateUrl: helper.basepath('ems.mastersamagro/AgrMstProductRevokeSummary.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })
            
            .state('app.AgrMstProductHoldRevokeSummary', {
                url: '/AgrMstProductHoldRevokeSummary',
                title: 'AgrMstProductHoldRevokeSummary',
                templateUrl: helper.basepath('ems.mastersamagro/AgrMstProductHoldRevokeSummary.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })

            .state('app.AgrMstProductRejectHoldRevokeHistory', {
                url: '/AgrMstProductRejectHoldRevokeHistory',
                title: 'AgrMstProductRejectHoldRevokeHistory',
                templateUrl: helper.basepath('ems.mastersamagro/AgrMstProductRejectHoldRevokeHistory.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })
            
            .state('app.AgrMstProductRevokedApplSummary', {
                url: '/AgrMstProductRevokedApplSummary',
                title: 'AgrMstProductRevokedApplSummary',
                templateUrl: helper.basepath('ems.mastersamagro/AgrMstProductRevokedApplSummary.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })

            
            

         // Horizontal layout
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