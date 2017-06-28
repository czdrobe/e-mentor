'use strict';
module.exports = function(grunt){
	grunt.initConfig({
	    pkg: grunt.file.readJSON('package.less.json'),

		less:{
			development: {
				options:{
					compress: true,
					yuicompress: true,
					optimisation: 2,
					sourceMap: true
				},
				files: {
					"css/styles.css" : "css/styles.less"
				}
			}
		},

		watch:{
			less:{
				files: 'css/styles.less',
				tasks:['less']
			}
		}
	});

	grunt.registerTask('default', []);
	grunt.registerTask('hello', function(){
		console.log('Hello World!');
	});

	grunt.loadNpmTasks('grunt-contrib-watch');
	grunt.loadNpmTasks('grunt-contrib-less');
	grunt.registerTask('default', ['less', 'watch']);
}