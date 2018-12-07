// function to create the cards
function createCards() {
	// declaring some variables
	var exampleBlockA = $('#exampleBlockA'), // cache the selector of the element, increases performance
		exampleBlockB = $('#exampleBlockB'), // cache the selector of the element, increases performance
		exampleBlockC = $('#exampleBlockC'), // cache the selector of the element, increases performance
		i,
		j = 1,
		tag,
		
		// the cards, in this example in an array
		imagename = ["Image1", "Image2", "Image3"],
		imagesource = ["./images/image1.jpg", "./images/image2.jpg", "./images/image3.jpg"],
		title = ["Title", "Title", "Title"],
		text = ["Lorem ipsum part 1...", "Lorem ipsum part 2...", "Lorem ipsum part 3..."];

	// emptying the current grid
	exampleBlockA.empty();
	exampleBlockB.empty();
	exampleBlockC.empty();

	// the loop to get the values from the arrays
	for (i = 0; i < 3; i = i + 1) {
		tag = '<div class="card" id="card' + i + '">' +
				'<div class="card-image"><img alt="' + imagename[i] + '" src="' + imagesource[i] + '" />' +
				'<h2>' + title[i] + '</h2></div>' +
				'<p>' +  text[i] + '</p>' +
				'</div>';
		
		/*	You will need to create cards in a special order.
			The first 1/3 of the cards are placed in block A.
			The second 1/3 of the cards are placed in block B.
			The last 1/3 of the cards are placed in block C.
			
			This will make sure that the cards will fill white spots
			when the screen is changing orientation and/or size.
			
			When you create new block for every card you would get
			an interface that is lined like a table.
		*/
		if (i < (result.rows.length / 3)) {
			exampleBlockA.append(tag);
		} else if (i < ((result.rows.length / 3) * 2)) {
			exampleBlockB.append(tag);
		} else if (i <= ((result.rows.length / 3) * 3)) {
			exampleBlockC.append(tag);
		}
		
		// add a press effect to the card
		pressEffectCard('card' + i);
	}
}

// press effect card ui
function pressEffectCard(x) {
	var id = $("#" + x); // cache the selector of the element, increases performance
	id.off('touchstart').on('touchstart', function () {
		id.addClass("holoPressEffectDiv");
	});
	id.off('touchend').on('touchend', function () {
		id.removeClass("holoPressEffectDiv");
	});
	id.off('touchmove').on('touchmove', function () {
		id.removeClass("holoPressEffectDiv"); // to remove the press effect when there is a scroll detected in stead of a tap
	});
}