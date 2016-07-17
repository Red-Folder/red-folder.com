describe('repo.service', function () {

    beforeEach(function () {
        bard.appModule('app');
        bard.inject('$httpBackend', '$q', 'repoService', '$rootScope');
    });

    it('exists', function () {
        /* jshint -W030 */
        expect(repoService).to.exist;
    });

    it('getAll hits the correct url', function () {
        $httpBackend.when('GET', '/api/Repo').respond(200, [{}]);
        repoService.getAll().then(function (data) {
            /* jshint -W030 */
            expect(data).to.exist;
        });
        $httpBackend.flush();
    });

    it('getAll reports an error on server fail', function () {
        $httpBackend.when('GET', '/api/Repo').respond(500, {description: 'Server failed'});
        repoService.getAll().catch(function (error) {
            expect(error).to.match(/Server failed/);
        });
        $httpBackend.flush();
    });

});
