(function() {
    'use strict';

    angular
        .module('app')
        .service('repoService', function () {
            this.getAll = [
                            {
                                name: 'Repo 1',
                                description: 'Repo 1 xxx desciption',
                                stars: 5,
                                openIssues: 7,
                                tags: [
                                    'Cordova',
                                    'Java',
                                ],
                            },
                            {
                                name: 'Repo 2',
                                description: 'Repo 2 desciption',
                                stars: 3,
                                openIssues: 0,
                                tags: [
                                    'Cordova',
                                    'JavaScript',
                                    'GPS',
                                ],
                            },
                            {
                                name: 'Repo 3',
                                description: 'Repo 3 desciption',
                                stars: 10,
                                openIssues: 100,
                                tags: [
                                    'C#',
                                    'CSS',
                                    'JavaScript',
                                ],
                            },
                        ];
        });
})();