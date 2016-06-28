/*jshint node:true*/
'use strict';
var express = require('express');

var app = express();
var port = process.env.PORT || 8001;

console.log('About to crank up node');
console.log('PORT=' + port);

console.log('** DEV **');
app.use(express.static('./'));

app.listen(port, function () {
    console.log('Express server listening on port ' + port);
    console.log('env = ' + app.get('env') +
        '\n__dirname = ' + __dirname +
        '\nprocess.cwd = ' + process.cwd());
});
