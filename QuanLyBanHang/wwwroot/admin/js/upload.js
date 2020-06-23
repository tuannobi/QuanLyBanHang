$(document).ready(function (e) {
	$("#uploadFile").change(function () {
		$('#image_preview').html("");
		var total_file = document.getElementById("uploadFile").files.length;
		for (var i = 0; i < total_file; i++) {
			$('#image_preview').append("<img src='" + URL.createObjectURL(event.target.files[i]) + "'>");
		}
	});
	$("#addproduct").on('submit', (function (e) {
		e.preventDefault();
		$.ajax({
			url: "upload.php",        // Url to which the request is send
			type: "POST",             // Type of request to be send, called as method
			data: new FormData(this), // Data sent to server, a set of key/value pairs (i.e. form fields and values)
			contentType: false,       // The content type used when sending data to the server.
			cache: false,             // To unable request pages to be cached
			processData: false,        // To send DOMDocument or non processed data file it is set to false
			success: function (response)   // A function to be called if request succeeds
			{
				alert(response);
				window.location.replace("listproducts.php");
			}
		});
	}
	));

});