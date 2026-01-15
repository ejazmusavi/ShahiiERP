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

    var items = [
        {id:1, text:'item 1 Number', ItemName:'Item 1'},
        {id:1, text:'item 2 Number', ItemName:'Item 2'},
        {id:1, text:'item 3 Number', ItemName:'Item 3'},
        {id:1, text:'item 4 Number', ItemName:'Item 4'},
    ];
    $('.select2').select2({ width:'100%', placeholder:'select an item' });
   var item_dropdown = $('#item-select2').select2({
        width: '320px',
        placeholder: "Select an Item",
        dropdownCssClass: 'bigdrop',
        data:items, //end ajax
        templateResult: formatOption
    });

    var item_dropdown2 = $('#item-select2-foot').select2({
        width: '320px',
        placeholder: "Select an Item",
        dropdownCssClass: 'bigdrop',
        data:items, //end ajax
        templateResult: formatOption
    });

    function formatOption(option) {
        if (!option.id) {
            return option.text;
        }
        const column1 = option.text;
        const column2 = option.ItemName;

        return $(
            `<div class="row gutter-0 align-items-center" style="margin:0 !important">
                <div class="col-6"><span class="truncate-multiline">${column1}</span></div>
                <div class="col-6"><span class="truncate-multiline">${column2}</span></div>
            </div>`
            
        );
    }
    item_dropdown2.on('select2:open', function () {
        // Check if headers already exist to prevent duplication
        if ($('.select2-dropdown .select2-search.select2-search--dropdown .select2-results__header').length == 0) {
            // Create the header
            var $header = $(
                '<div class="row gutter-0 select2-results__header" style="margin:0 !important; font-weight: bold; border-bottom: 1px solid var(--input-border);margin-left: -4px!important;margin-right: -4px!important; padding-top: 6px;padding-bottom: 10px;">' +
                '<div class="col-6 d-flex align-items-center">' +
                '<span>Item Number</span>' +
                '</div>' +
                '<div class="col-6 d-flex align-items-center">' +
                '<span>Item Name</span>' +
                '</div>' +
                '</div>'
            );

            // Prepend the header to the results list
            $('.select2-dropdown .select2-search.select2-search--dropdown').prepend($header);
        }
    });
    item_dropdown.on('select2:open', function () {
        // Check if headers already exist to prevent duplication
        if ($('.select2-dropdown .select2-search.select2-search--dropdown .select2-results__header').length == 0) {
            // Create the header
            var $header = $(
                '<div class="row gutter-0 select2-results__header" style="margin:0 !important; font-weight: bold; border-bottom: 1px solid var(--input-border);margin-left: -4px!important;margin-right: -4px!important; padding-top: 6px;padding-bottom: 10px;">' +
                '<div class="col-6 d-flex align-items-center">' +
                '<span>Item Number</span>' +
                '</div>' +
                '<div class="col-6 d-flex align-items-center">' +
                '<span>Item Name</span>' +
                '</div>' +
                '</div>'
            );

            // Prepend the header to the results list
            $('.select2-dropdown .select2-search.select2-search--dropdown').prepend($header);
        }
    });
 var ordertable =  $('#order-lines-table').DataTable({
            responsive: window.innerWidth <= 768,
            columnDefs: [
                { responsivePriority: 1, targets: 0 }, // Column 1
                { responsivePriority: 2, targets: 1 }, // Column 3
                { responsivePriority: 3, targets: 2 },  // Column 4
                { responsivePriority: 4, targets: 5 },  // Column 4
                { responsivePriority: 5, targets: 7 },  // Column 4
                { responsivePriority: 6, targets: 8 },  // Column 4
            ],
        });
    $(document).on('ready',function () {
      
        // if ($(window).width() <= 786) {
        //     $('#order-lines-table').DataTable({
        //         bSort:false,
        //     bPaginate:false,
        //     bFilter:false,
        //     bInfo:false,
        //     responsive: true
        //     });
        // } else {
        //     $('#order-lines-table').DataTable({
        //         bSort:false,
        //     bPaginate:false,
        //     bFilter:false,
        //     bInfo:false,
        //     responsive: false
        //     });
        // }
    });

    
    $(window).on('resize',function () {
        var isMobile = window.innerWidth <= 768;
        console.log(isMobile)
        ordertable.responsive = isMobile; // Enable responsive only on mobile
        ordertable.columns.adjust().responsive.recalc();
        ordertable.draw();
        // console.log('current width is',$(window).width())
        // if ($(window).width() <= 786) {
        //     if (!$.fn.DataTable.isDataTable('#order-lines-table')) {
        //         $('#order-lines-table').DataTable().destroy();
        //         $('#order-lines-table').DataTable({
        //             bSort:false,
        //         bPaginate:false,
        //         bFilter:false,
        //         bInfo:false,
        //         responsive: true
        //         });
        //     }
        // } else {
        //     if ($.fn.DataTable.isDataTable('#order-lines-table')) {
        //         $('#order-lines-table').DataTable().destroy();
        //         $('#order-lines-table').DataTable({
        //             bSort:false,
        //             bPaginate:false,
        //             bFilter:false,
        //             bInfo:false,
        //             responsive: false
        //         });
        //     }
        // }
    });



    const form = document.getElementById('salesOrderForm');
        const editBtn = document.getElementById('editBtn');
        const saveBtn = document.getElementById('saveBtn');
        const cancelBtn = document.getElementById('cancelBtn');

        const choicesInstances = [];

        // Initialize Choices.js for all select elements in the form
        const selectElements = form.querySelectorAll('select[data-trigger]');
        selectElements.forEach(select => {
            // if (select.classList.contains('choices__input')) {
            //     // Add to the list
            // }
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
                      //closestDiv.style.overflowX = 'hidden !important';
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
                      //closestDiv.style.overflowY = 'auto';
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
                if (element.tagName === 'BUTTON' && element.classList.contains('to-hide')) element.style.display =isReadonly? 'none':'inline-block'; // Skip buttons
                else 
                if (element.type === 'radio' || element.type === 'checkbox' || element.tagName === 'SELECT') {
                    element.disabled = isReadonly;
                }
                 else if(element.tagName !== 'BUTTON') {
                    element.disabled = isReadonly;
                }
            }

            // choicesInstances.forEach(choices => {
            //     if (isReadonly) {
            //         choices.disable(); // Disable all Choices instances
            //     } else {
            //         choices.enable(); // Enable all Choices instances
            //         choices.refresh();
            //     }
            // });
             // Toggle button visibility based on read-only state
             if (isReadonly) {
                editBtn.style.display = 'inline-block';
                saveBtn.style.display = 'none';
                cancelBtn.style.display = 'none';
                tfoot.style.display = 'none';
                for (let i = 0; i < rows.length; i++) {
                    //rows[i].cells[0].style.display = 'none';
                }
            } else {
                editBtn.style.display = 'none';
                saveBtn.style.display = 'inline-block';
                cancelBtn.style.display = 'inline-block';
                tfoot.style.display = 'table-footer-group';
                for (let i = 0; i < rows.length; i++) {
                    //rows[i].cells[0].style.display = 'table-cell';
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
})();