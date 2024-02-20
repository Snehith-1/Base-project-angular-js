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
        
        

                .state('app.AgrProductStageSummary', {
                url: '/AgrProductStageSummary',
                title: 'AgrProductStageSummary',
                templateUrl: helper.basepath('ems.mastersamagro/AgrProductStageSummary.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })

             .state('app.AgrSuprDocumentChecklistReport', {
                 url: '/AgrSuprDocumentChecklistReport',
                 title: 'AgrSuprDocumentChecklistReport',
                 templateUrl: helper.basepath('ems.mastersamagro/AgrSuprDocumentChecklistReport.html?ver=' + version + '"'),
                 resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
             })

 .state('app.AgrDocumentChecklistApprovalCompletedReport', {
                 url: '/AgrDocumentChecklistApprovalCompletedReport',
                 title: 'AgrDocumentChecklistApprovalCompletedReport',
                 templateUrl: helper.basepath('ems.mastersamagro/AgrDocumentChecklistApprovalCompletedReport.html?ver=' + version + '"'),
                 resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
             })
             .state('app.AgrMstByrProposalProductView', {
                 url: '/AgrMstByrProposalProductView',
                 title: 'AgrMstByrProposalProductView',
                 templateUrl: helper.basepath('ems.mastersamagro/AgrMstByrProposalProductView.html?ver=' + version + '"'),
                 resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
             })
             .state('app.AgrMstByrProductcomparisonView', {
                 url: '/AgrMstByrProductcomparisonView',
                 title: 'AgrMstByrProductcomparisonView',
                 templateUrl: helper.basepath('ems.mastersamagro/AgrMstByrProductcomparisonView.html?ver=' + version + '"'),
                 resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
             })
 .state('app.brsTrnMyUnReconciliationPendingView', {
            url: '/brsTrnMyUnReconciliationPendingView',
            title: 'brsTrnMyUnReconciliationPendingView',
            templateUrl: helper.basepath('ems.brs/brsTrnMyUnReconciliationPendingView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
	              .state('app.MstSAVerificationMakerInstitutionDistractSummary', {
                url: '/MstSAVerificationMakerInstitutionDistractSummary',
                title: 'MstSAVerificationMakerInstitutionDistractSummary',
                templateUrl: helper.basepath('ems.master/MstSAVerificationMakerInstitutionDistractSummary.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })
            .state('app.MstSAVerificationMakerIndividualDistractSummary', {
                url: '/MstSAVerificationMakerIndividualDistractSummary',
                title: 'MstSAVerificationMakerIndividualDistractSummary',
                templateUrl: helper.basepath('ems.master/MstSAVerificationMakerIndividualDistractSummary.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })
            .state('app.MstSAVerificationInstitutionCheckerDistractSummary', {
                url: '/MstSAVerificationInstitutionCheckerDistractSummary',
                title: 'MstSAVerificationInstitutionCheckerDistractSummary',
                templateUrl: helper.basepath('ems.master/MstSAVerificationInstitutionCheckerDistractSummary.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })
            .state('app.MstSAVerificationIndividualCheckerDistractSummary', {
                url: '/MstSAVerificationIndividualCheckerDistractSummary',
                title: 'MstSAVerificationIndividualCheckerDistractSummary',
                templateUrl: helper.basepath('ems.master/MstSAVerificationIndividualCheckerDistractSummary.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })
            .state('app.MstSAVerificationInstitutionMakerTrashView', {
                url: '/MstSAVerificationInstitutionMakerTrashView',
                title: 'MstSAVerificationInstitutionMakerTrashView',
                templateUrl: helper.basepath('ems.master/MstSAVerificationInstitutionMakerTrashView.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })
            .state('app.MstSAVerificationIndividualMakerTrashView', {
                url: '/MstSAVerificationIndividualMakerTrashView',
                title: 'MstSAVerificationIndividualMakerTrashView',
                templateUrl: helper.basepath('ems.master/MstSAVerificationIndividualMakerTrashView.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })
            .state('app.MstSAVerificationInstitutionCheckerTrashView', {
                url: '/MstSAVerificationInstitutionCheckerTrashView',
                title: 'MstSAVerificationInstitutionCheckerTrashView',
                templateUrl: helper.basepath('ems.master/MstSAVerificationInstitutionCheckerTrashView.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })
            .state('app.MstSAVerificationIndividualCheckerTrashView', {
                url: '/MstSAVerificationIndividualCheckerTrashView',
                title: 'MstSAVerificationIndividualCheckerTrashView',
                templateUrl: helper.basepath('ems.master/MstSAVerificationIndividualCheckerTrashView.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })
        .state('app.MstSAApprovalIndividualDeferredSummary', {
                url: '/MstSAApprovalIndividualDeferredSummary',
                title: 'MstSAApprovalIndividualDeferredSummary',
                templateUrl: helper.basepath('ems.master/MstSAApprovalIndividualDeferredSummary.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })

            .state('app.MstSAApprovalInstitutionDeferredSummary', {
                url: '/MstSAApprovalInstitutionDeferredSummary',
                title: 'MstSAApprovalInstitutionDeferredSummary',
                templateUrl: helper.basepath('ems.master/MstSAApprovalInstitutionDeferredSummary.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })
            .state('app.MstSABussDevtInstitutionDeferredSummary', {
            url: '/MstSABussDevtInstitutionDeferredSummary',
            title: 'MstSABussDevtInstitutionDeferredSummary',
            templateUrl: helper.basepath('ems.master/MstSABussDevtInstitutionDeferredSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        
        .state('app.MstSABussDevtIndividualDeferredSummary', {
            url: '/MstSABussDevtIndividualDeferredSummary',
            title: 'MstSABussDevtIndividualDeferredSummary',
            templateUrl: helper.basepath('ems.master/MstSABussDevtIndividualDeferredSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstSAVerificationIndividualDeferredSummary', {
            url: '/MstSAVerificationIndividualDeferredSummary',
            title: 'MstSAVerificationIndividualDeferredSummary',
            templateUrl: helper.basepath('ems.master/MstSAVerificationIndividualDeferredSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstSAVerificationInstitutionDeferredSummary', {
            url: '/MstSAVerificationInstitutionDeferredSummary',
            title: 'MstSAVerificationInstitutionDeferredSummary',
            templateUrl: helper.basepath('ems.master/MstSAVerificationInstitutionDeferredSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstSAVerificationInstitutionDeferredView', {
            url: '/MstSAVerificationInstitutionDeferredView',
            title: 'MstSAVerificationInstitutionDeferredView',
            templateUrl: helper.basepath('ems.master/MstSAVerificationInstitutionDeferredView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstSAVerificationIndividualDeferredView', {
            url: '/MstSAVerificationIndividualDeferredView',
            title: 'MstSAVerificationIndividualDeferredView',
            templateUrl: helper.basepath('ems.master/MstSAVerificationIndividualDeferredView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstSABussDevtInstitutionDeferredView', {
            url: '/MstSABussDevtInstitutionDeferredView',
            title: 'MstSABussDevtInstitutionDeferredView',
            templateUrl: helper.basepath('ems.master/MstSABussDevtInstitutionDeferredView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstSABussDevtIndividualDeferredView', {
            url: '/MstSABussDevtIndividualDeferredView',
            title: 'MstSABussDevtIndividualDeferredView',
            templateUrl: helper.basepath('ems.master/MstSABussDevtIndividualDeferredView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstSAApprovalInstitutionDeferredView', {
            url: '/MstSAApprovalInstitutionDeferredView',
            title: 'MstSAApprovalInstitutionDeferredView',
            templateUrl: helper.basepath('ems.master/MstSAApprovalInstitutionDeferredView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstSAApprovalIndividualDeferredView', {
            url: '/MstSAApprovalIndividualDeferredView',
            title: 'MstSAApprovalIndividualDeferredView',
            templateUrl: helper.basepath('ems.master/MstSAApprovalIndividualDeferredView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstSAMyApprovalInstitutionDeferredView', {
            url: '/MstSAMyApprovalInstitutionDeferredView',
            title: 'MstSAMyApprovalInstitutionDeferredView',
            templateUrl: helper.basepath('ems.master/MstSAMyApprovalInstitutionDeferredView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstSAMyApprovalIndividualDeferredView', {
            url: '/MstSAMyApprovalIndividualDeferredView',
            title: 'MstSAMyApprovalIndividualDeferredView',
            templateUrl: helper.basepath('ems.master/MstSAMyApprovalIndividualDeferredView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.brsTrnMyUnReconciliationClosedView', {
            url: '/brsTrnMyUnReconciliationClosedView',
            title: 'brsTrnMyUnReconciliationClosedView',
            templateUrl: helper.basepath('ems.brs/brsTrnMyUnReconciliationClosedView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
          .state('app.AgrMstContractSummary', {
                 url: '/AgrMstContractSummary',
                 title: 'AgrMstContractSummary',
                 templateUrl: helper.basepath('ems.mastersamagro/AgrMstContractSummary.html?ver=' + version + '"'),
                 resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
             })
            .state('app.AgrMstContractCheckerSummary', {
                 url: '/AgrMstContractCheckerSummary',
                 title: 'AgrMstContractCheckerSummary',
                 templateUrl: helper.basepath('ems.mastersamagro/AgrMstContractCheckerSummary.html?ver=' + version + '"'),
                 resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
             })
            .state('app.AgrMstContractApprovalSummary', {
                 url: '/AgrMstContractApprovalSummary',
                 title: 'AgrMstContractApprovalSummary',
                 templateUrl: helper.basepath('ems.mastersamagro/AgrMstContractApprovalSummary.html?ver=' + version + '"'),
                 resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
             })
            .state('app.AgrMstContractApprovalCompleted', {
                 url: '/AgrMstContractApprovalCompleted',
                 title: 'AgrMstContractApprovalCompleted',
                 templateUrl: helper.basepath('ems.mastersamagro/AgrMstContractApprovalCompleted.html?ver=' + version + '"'),
                 resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
             }) 
            .state('app.AgrMstContractAccepted', {
                 url: '/AgrMstContractAccepted',
                 title: 'AgrMstContractAccepted',
                 templateUrl: helper.basepath('ems.mastersamagro/AgrMstContractAccepted.html?ver=' + version + '"'),
                 resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
             }) 
           .state('app.AgrMstContractDtlSummary', {
                 url: '/AgrMstContractDtlSummary',
                 title: 'AgrMstContractDtlSummary',
                 templateUrl: helper.basepath('ems.mastersamagro/AgrMstContractDtlSummary.html?ver=' + version + '"'),
                 resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
             }) 
            .state('app.AgrMstCreateContract', {
                 url: '/AgrMstCreateContract',
                 title: 'AgrMstCreateContract',
                 templateUrl: helper.basepath('ems.mastersamagro/AgrMstCreateContract.html?ver=' + version + '"'),
                 resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            }) 
             .state('app.AgrMstContrateTemplateSummary', {
                 url: '/AgrMstContrateTemplateSummary',
                 title: 'AgrMstContrateTemplateSummary',
                 templateUrl: helper.basepath('ems.mastersamagro/AgrMstContrateTemplateSummary.html?ver=' + version + '"'),
                 resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
             }) 

             .state('app.AgrMstContractEdit', {
                 url: '/AgrMstContractEdit',
                 title: 'AgrMstContractEdit',
                 templateUrl: helper.basepath('ems.mastersamagro/AgrMstContractEdit.html?ver=' + version + '"'),
                 resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
             }) 

             .state('app.AgrMstContractDtlViewSummary', {
                 url: '/AgrMstContractDtlViewSummary',
                 title: 'AgrMstContractDtlViewSummary',
                 templateUrl: helper.basepath('ems.mastersamagro/AgrMstContractDtlViewSummary.html?ver=' + version + '"'),
                 resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
             }) 

             .state('app.AgrMstAppContractLetterWordView', {
                 url: '/AgrMstAppContractLetterWordView',
                 title: 'AgrMstAppContractLetterWordView',
                 templateUrl: helper.basepath('ems.mastersamagro/AgrMstAppContractLetterWordView.html?ver=' + version + '"'),
                 resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
             })

             .state('app.AgrSanctionEditTemplate', {
                 url: '/AgrSanctionEditTemplate',
                 title: 'AgrSanctionEditTemplate',
                 templateUrl: helper.basepath('ems.mastersamagro/AgrSanctionEditTemplate.html?ver=' + version + '"'),
                 resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
             })

             .state('app.AgrSanctionAddTemplate', {
                 url: '/AgrSanctionAddTemplate',
                 title: 'AgrSanctionAddTemplate',
                 templateUrl: helper.basepath('ems.mastersamagro/AgrSanctionAddTemplate.html?ver=' + version + '"'),
                 resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
             })
             .state('app.AgrSanctionTemplateSummary', {
                 url: '/AgrSanctionTemplateSummary',
                 title: 'AgrSanctionTemplateSummary',
                 templateUrl: helper.basepath('ems.mastersamagro/AgrSanctionTemplateSummary.html?ver=' + version + '"'),
                 resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
             })

             .state('app.AgrMstAppSanctionLetterGeneration', {
                 url: '/AgrMstAppSanctionLetterGeneration',
                 title: 'AgrMstAppSanctionLetterGeneration',
                 templateUrl: helper.basepath('ems.mastersamagro/AgrMstAppSanctionLetterGeneration.html?ver=' + version + '"'),
                 resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
             })

             .state('app.AgrMstSanctionDtlViewSummary', {
                 url: '/AgrMstSanctionDtlViewSummary',
                 title: 'AgrMstSanctionDtlViewSummary',
                 templateUrl: helper.basepath('ems.mastersamagro/AgrMstSanctionDtlViewSummary.html?ver=' + version + '"'),
                 resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
             })
             .state('app.AgrTrnCcMeetingSkipSummary', {
                 url: '/AgrTrnCcMeetingSkipSummary',
                 title: 'AgrTrnCcMeetingSkipSummary',
                 templateUrl: helper.basepath('ems.mastersamagro/AgrTrnCcMeetingSkipSummary.html?ver=' + version + '"'),
                 resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
             })
             .state('app.AgrTrnCcMeetingSkipHistory', {
                 url: '/AgrTrnCcMeetingSkipHistory',
                 title: 'AgrTrnCcMeetingSkipHistory',
                 templateUrl: helper.basepath('ems.mastersamagro/AgrTrnCcMeetingSkipHistory.html?ver=' + version + '"'),
                 resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
             })
          .state('app.MstEncoreProduct', {
            url: '/MstEncoreProduct',
            title: 'MstEncoreProduct',
            templateUrl: helper.basepath('ems.master/MstEncoreProduct.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
          .state('app.SysMstBranchSummary', {
                url: '/SysMstBranchSummary',
                title: 'SysMstBranchSummary',
                templateUrl: helper.basepath('ems.system/SysMstBranchSummary.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })
            .state('app.SysMstDepartmentSummary', {
                url: '/SysMstDepartmentSummary',
                title: 'SysMstDepartmentSummary',
                templateUrl: helper.basepath('ems.system/SysMstDepartmentSummary.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })
    .state('app.BrsTrnUnreconBankMatchedView', {
            url: '/BrsTrnUnreconBankMatchedView',
            title: 'BrsTrnUnreconBankMatchedView',
            templateUrl: helper.basepath('ems.brs/BrsTrnUnreconBankMatchedView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
       

        .state('app.BrsTrnUnreconRepaymentMatchedView', {
            url: '/BrsTrnUnreconRepaymentMatchedView',
            title: 'BrsTrnUnreconRepaymentMatchedView',
            templateUrl: helper.basepath('ems.brs/BrsTrnUnreconRepaymentMatchedView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')

        })
    .state('app.BrsTrnUnreconTransactionDetails', {
            url: '/BrsTrnUnreconTransactionDetails',
            title: 'BrsTrnUnreconTransactionDetails',
            templateUrl: helper.basepath('ems.brs/BrsTrnUnreconTransactionDetails.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.BrsTrnUnreconAssignedSummary', {
            url: '/BrsTrnUnreconAssignedSummary',
            title: 'BrsTrnUnreconAssignedSummary',
            templateUrl: helper.basepath('ems.brs/BrsTrnUnreconAssignedSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')

        })
       .state('app.AgrTrnPMGPhysicalDochecklist', {
                 url: '/AgrTrnPMGPhysicalDochecklist',
                 title: 'AgrTrnPMGPhysicalDochecklist',
                 templateUrl: helper.basepath('ems.mastersamagro/AgrTrnPMGPhysicalDochecklist.html?ver=' + version + '"'),
                 resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
             })

             .state('app.AgrTrnPMGPhysicalDocQuery', {
                 url: '/AgrTrnPMGPhysicalDocQuery',
                 title: 'AgrTrnPMGPhysicalDocQuery',
                 templateUrl: helper.basepath('ems.mastersamagro/AgrTrnPMGPhysicalDocQuery.html?ver=' + version + '"'),
                 resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
             })
         .state('app.MstOneApiDashboard', {
                url: '/MstOneApiDashboard',
                title: 'MstOneApiDashboard',
                templateUrl: helper.basepath('ems.master/MstOneApiDashboard.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })
        .state('app.SysMstApiUserRegistration', {
                 url: '/SysMstApiUserRegistration',
                 title: 'SysMstApiUserRegistration',
                 templateUrl: helper.basepath('ems.system/SysMstApiUserRegistration.html?ver=' + version + '"'),
                     resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
                 })
       .state('app.SysMstStateSummary', {
            url: '/SysMstStateSummary',
            title: 'SysMstStateSummary',
            templateUrl: helper.basepath('ems.system/SysMstStateSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'ngWig')
        })
    .state('app.MstSupplierSummary',
        {
            url: '/MstSupplierSummary',
            title: 'MstSupplierSummary',
            templateUrl: helper.basepath('ems.master/MstSupplierSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
 .state('app.MstCadConsolidatedSanctionReport', {
                 url: '/MstCadConsolidatedSanctionReport',
                 title: 'MstCadConsolidatedSanctionReport',
                 templateUrl: helper.basepath('ems.master/MstCadConsolidatedSanctionReport.html?ver=' + version + '"'),
                 resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
             })

             .state('app.MstCadConsolidatedLSAReport', {
                 url: '/MstCadConsolidatedLSAReport',
                 title: 'MstCadConsolidatedLSAReport',
                 templateUrl: helper.basepath('ems.master/MstCadConsolidatedLSAReport.html?ver=' + version + '"'),
                 resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
             })

             .state('app.MstCadConsolidatedDocumentChecklistReport', {
                 url: '/MstCadConsolidatedDocumentChecklistReport',
                 title: 'MstCadConsolidatedDocumentChecklistReport',
                 templateUrl: helper.basepath('ems.master/MstCadConsolidatedDocumentChecklistReport.html?ver=' + version + '"'),
                 resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
             })

             .state('app.MstCadConsolidatedSoftcopyVettingReport', {
                 url: '/MstCadConsolidatedSoftcopyVettingReport',
                 title: 'MstCadConsolidatedSoftcopyVettingReport',
                 templateUrl: helper.basepath('ems.master/MstCadConsolidatedSoftcopyVettingReport.html?ver=' + version + '"'),
                 resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
             })

             .state('app.MstCadConsolidatedOriginalCopyVettingReport', {
                 url: '/MstCadConsolidatedOriginalCopyVettingReport',
                 title: 'MstCadConsolidatedOriginalCopyVettingReport',
                 templateUrl: helper.basepath('ems.master/MstCadConsolidatedOriginalCopyVettingReport.html?ver=' + version + '"'),
                 resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
             })

           .state('app.TransUnionInstitutionReport', {
            url: '/TransUnionInstitutionReport',
            title: 'TransUnionInstitutionReport',
            templateUrl: helper.basepath('ems.master/TransUnionInstitutionReport.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
       .state('app.AgrTransUnionInstitutionReport', {
                url: '/AgrTransUnionInstitutionReport',
                title: 'AgrTransUnionInstitutionReport',
                templateUrl: helper.basepath('ems.mastersamagro/AgrTransUnionInstitutionReport.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')

            })
       .state('app.AgrCadConsolidatedSanctionReport', {
                 url: '/AgrCadConsolidatedSanctionReport',
                 title: 'AgrCadConsolidatedSanctionReport',
                 templateUrl: helper.basepath('ems.mastersamagro/AgrCadConsolidatedSanctionReport.html?ver=' + version + '"'),
                 resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
             })           

 

             .state('app.AgrCadConsolidatedDocumentChecklistReport', {
                 url: '/AgrCadConsolidatedDocumentChecklistReport',
                 title: 'AgrtCadConsolidatedDocumentChecklistReport',
                 templateUrl: helper.basepath('ems.mastersamagro/AgrCadConsolidatedDocumentChecklistReport.html?ver=' + version + '"'),
                 resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
             })

 

             .state('app.AgrCadConsolidatedSoftcopyVettingReport', {
                 url: '/AgrCadConsolidatedSoftcopyVettingReport',
                 title: 'AgrCadConsolidatedSoftcopyVettingReport',
                 templateUrl: helper.basepath('ems.mastersamagro/AgrCadConsolidatedSoftcopyVettingReport.html?ver=' + version + '"'),
                 resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
             })

 

             .state('app.AgrCadConsolidatedOriginalCopyVettingReport', {
                 url: '/AgrCadConsolidatedOriginalCopyVettingReport',
                 title: 'AgrCadConsolidatedOriginalCopyVettingReport',
                 templateUrl: helper.basepath('ems.mastersamagro/AgrCadConsolidatedOriginalCopyVettingReport.html?ver=' + version + '"'),
                 resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
             })
       .state('app.MstHardCodedType', {
                url: '/MstHardCodedType',
                title: 'MstHardCodedType',
                templateUrl: helper.basepath('ems.system/MstHardCodedType.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })
      .state('app.StdOneApiDashboard', {
                url: '/StdOneApiDashboard',
                title: 'StdOneApiDashboard',
                templateUrl: helper.basepath('ems.system/StdOneApiDashboard.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })
        .state('app.SysOneApiDashboard', {
                url: '/SysOneApiDashboard',
                title: 'SysOneApiDashboard',
                templateUrl: helper.basepath('ems.system/SysOneApiDashboard.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })
             .state('app.brsTrnRepaymentMatchedImport', {
                 url: '/brsTrnRepaymentMatchedImport',
                 title: 'brsTrnRepaymentMatchedImport',
                 templateUrl: helper.basepath('ems.brs/brsTrnRepaymentMatchedImport.html?ver=' + version + '"'),
                 resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
             })
         
            .state('app.brsTrnBankMatchedImport', {
                url: '/brsTrnBankMatchedImport',
                title: 'brsTrnBankMatchedImport',
                templateUrl: helper.basepath('ems.brs/brsTrnBankMatchedImport.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })
		 .state('app.MstLSAReport', {
                url: '/MstLSAReport',
                title: 'MstLSAReport',
                templateUrl: helper.basepath('ems.master/MstLSAReport.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })

       .state('app.AgrMstCreditManagerRejectRevokeSummary', {
                url: '/AgrMstCreditManagerRejectRevokeSummary',
                title: 'AgrMstCreditManagerRejectRevokeSummary',
                templateUrl: helper.basepath('ems.mastersamagro/AgrMstCreditManagerRejectRevokeSummary.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')

            }) 

            .state('app.AgrMstCreditManagerRejectRevoke', {
                url: '/AgrMstCreditManagerRejectRevoke',
                title: 'AgrMstCreditManagerRejectRevoke',
                templateUrl: helper.basepath('ems.mastersamagro/AgrMstCreditManagerRejectRevoke.html?ver=' + version + '"'),
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