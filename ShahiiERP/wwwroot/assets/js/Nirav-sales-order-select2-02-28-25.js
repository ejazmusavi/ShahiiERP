(function () {
    "use strict"

    // for publish date picker
    flatpickr("#tran-date", {
        defaultDate: ["2023-04-17"]
    });
    flatpickr("#ship-date", {
        defaultDate: ["2023-04-17"]
    });

    flatpickr("#shipto-date", {
        defaultDate: ["2023-04-17"]
    });

    $('.select2').select2({ width:'100%', placeholder:'select an item' });

    const form = document.getElementById('salesOrderForm');
        const editBtn = document.getElementById('editBtn');
        const saveBtn = document.getElementById('saveBtn');
        const cancelBtn = document.getElementById('cancelBtn');
        const showhideheader = document.getElementById('showhideHeaderBtn');
        const allDivs = document.getElementById('header').children;
        const firstTwoDivs = Array.from(allDivs).slice(0, 2);
        const choicesInstances = [];

        // Add click event listener to the button
        showhideheader.addEventListener('click', function() {
            // Check the display state of the first div (assuming both have the same visibility)
            const isHidden = firstTwoDivs[0].style.display === 'none';
            
            // Toggle the visibility of both divs
            firstTwoDivs.forEach(div => {
                div.style.display = isHidden ? 'block' : 'none';
            });
            
            // Update button text
            showhideheader.textContent = isHidden ? 'Hide Header' : 'Show Header';
        });


        // Initialize Choices.js for all select elements in the form
        const selectElements = form.querySelectorAll('select[data-trigger]');
        selectElements.forEach(select => {
            const choices = new Choices(select, {
                allowHTML: true,
                removeItemButton: select.multiple?true:false,
              });
              

              select.addEventListener(
                'showDropdown',
                function(event) {
                    var closestDiv = select.closest('div.table-responsive');

                    if (closestDiv) {
                      // Set the CSS property 'overflow' to 'visible'
                      closestDiv.style.overflow = 'visible';
                    }
                },
                false,
              );

              select.addEventListener(
                'hideDropdown',
                function(event) {
                    var closestDiv = select.closest('div.table-responsive');

                    if (closestDiv) {
                      // Set the CSS property 'overflow' to 'visible'
                      closestDiv.style.overflow = 'auto';
                    }
                },
                false,
              );
                choicesInstances.push(choices); 
        });

        const table = document.getElementById('order-lines-table');
        const rows = table.rows;
        const tfoot = document.querySelector('#order-lines-table tfoot');

        // Function to set the read-only state of the form
        function setFormReadonly(isReadonly) {
            const elements = form.elements;

            
            choicesInstances.forEach(choices => {
                if (isReadonly) {
                    choices.disable(); // Disable all Choices instances
                } else {
                    choices.enable(); // Enable all Choices instances
                    console.log(choices)
                }
            });

            for (let i = 0; i < elements.length; i++) {
                const element = elements[i];
                console.log(element);
                const isTableHead = $(element).closest('thead').attr('id') === 'tableHead';

                if (element.tagName === 'INPUT' && (element.type === 'search' || element.type === 'date')) {
                    $(element).prop('disabled', isTableHead ? false : isReadonly);
                    continue;
                }

                if (element.tagName === 'BUTTON') {
                    if (element.classList.contains('to-hide')) {
                        element.style.display = isReadonly ? 'none' : 'inline-block';
                    }
                    continue;
                }

                if (element.type === 'radio' || element.type === 'checkbox' || element.tagName === 'SELECT') {
                    if (element.tagName === 'SELECT' && !element.classList.contains('allowEdit')) {
                        $(element).prop('disabled', isTableHead ? false : isReadonly);
                    } else if (element.classList.contains('allowEdit')) {
                        $(element).prop('disabled', false);
                    } else {
                        element.disabled = isReadonly;
                    }
                    continue;
                }
            }

             // Toggle button visibility based on read-only state
             if (isReadonly) {
                editBtn.style.display = 'inline-block';
                saveBtn.style.display = 'none';
                cancelBtn.style.display = 'none';
                tfoot.style.display = 'none';
                for (let i = 0; i < rows.length; i++) {
                    rows[i].cells[1].style.display = 'none';
                }
            } else {
                editBtn.style.display = 'none';
                saveBtn.style.display = 'inline-block';
                cancelBtn.style.display = 'inline-block';
                tfoot.style.display = 'table-footer-group';
                for (let i = 0; i < rows.length; i++) {
                    rows[i].cells[1].style.display = 'table-cell';
                }
            }
        }

        // Initialize form as readonly
        setFormReadonly(true);

        // Add event listeners to buttons
        editBtn.addEventListener('click', () => setFormReadonly(false));
        //saveBtn.addEventListener('click', () => setFormReadonly(true));
        cancelBtn.addEventListener('click', () => setFormReadonly(true));

        document.addEventListener('keydown', (event) => {
            const activeElement = document.activeElement;
        
            // Check if the active element is inside the footer row
            if (activeElement && activeElement.closest('tfoot')) {
                // Check if the pressed key is "Enter"
                if (event.key === 'Enter') {
                    event.preventDefault(); // Prevent default Enter behavior
        
                    // Find the "+" button in the footer row
                    const footer = activeElement.closest('tfoot');
                    const addButton = footer.querySelector('.add-row'); // Assumes the "+" button has class "add-row"
        
                    if (addButton) {
                        addButton.click(); // Trigger the click event on the "+" button
                    }
                }
            }
        });

        
        const addRowBtn = document.getElementById('add-row');
        addRowBtn.addEventListener('click', (e)=>{
            alert('adding new ror.');            
        });

        const fulfillmentDropdownItems = document.querySelectorAll('.fulfillment-dropdown .dropdown-item');
        fulfillmentDropdownItems.forEach(item => {
            item.addEventListener('click', function(e) {
                e.preventDefault();
                const text = this.textContent;
                item.closest('.fulfillment-dropdown').querySelectorAll('.dropdown-item').forEach(el => el.classList.remove('records'));
                item.classList.add('records');
                
                // Update button text
                const dropdownButton = this.closest('.btn-group').querySelector('.dropdown-toggle');
                dropdownButton.textContent = text;
            });
        });
})();