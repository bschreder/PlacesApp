'use strict';

//  This List object contains all methods required for working with the comprehensive-item object
//  Allergy, Medication, and MedicalConditions - one list for all modals

let ComprehensiveList = function () {
    this.list = [];
};

//  Get the number on elements in the ComprehensiveList array
//  return: length of array
ComprehensiveList.prototype.length = function () {
    return this.list.length;
};

//  Clear entries from object
//  return: void
ComprehensiveList.prototype.clear = function () {
    this.list.length = 0;
};

//  Add a comprehensiveItem to the ComprehensiveList array
//  comprehensiveItem - item to be added to list
//  return:  updated ComprehensiveList
ComprehensiveList.prototype.add = function (comprehensiveItem) {
    if (comprehensiveItem == null) {
        return null;
    }

    this.list.push(comprehensiveItem);
    return this;
};

//  Add or update comprehensiveItem to the array
//  comprehensiveItem - item to be added to list
//  return:  updated ComprehensiveList
ComprehensiveList.prototype.addUpdate = function (comprehensiveItem) {
    if (!(comprehensiveItem instanceof ComprehensiveItem)) {
        return null;
    }

    let index = this.indexOf(comprehensiveItem.name)
    if (index >= 0) {
        this.list[index] = comprehensiveItem;
    } else {
        this.add(comprehensiveItem);
    }
    return this;
};

//  IndexOf an item from the ComprehensiveList array
//  name - name to match that will identify element to be found
//  return:  index of found item (-1 if not found)
ComprehensiveList.prototype.indexOf = function (name) {
    return $.inArray(name, this.list.map(i => i.name));
}

//  Get all items from the list
//  return: comprehensiveItem list
ComprehensiveList.prototype.getItems = function () {
    return this.list;
}

//  Get a specific comprehensiveItem based on index into array
//  index - array index or key name
//  return:  comprehensiveItem
ComprehensiveList.prototype.getItem = function (index) {
    if (typeof index === 'number' && index in this.list) {
        return this.list[index];
    } else if (typeof index === 'string') {
        return this.list[this.indexOf(index)];
    }
    return null;
}

//  Remove an item from the ComprehensiveList array
//  name - name to be removed
//  return:  updated ComprehensiveList array
ComprehensiveList.prototype.remove = function (name) {
    let index = this.indexOf(name);
    if (index < 0) {
        return null;
    }
    this.list.splice(index, 1);
    return this;
};



//  This is for testing only
/* istanbul ignore next */
ComprehensiveList.prototype.sArray = function () {
    let ov = [];
    for (let i = 0; i < this.list.length; i++) {
        let x = this.list[i];
        let v = 'i: ' + i +
            ', name: ' + x.name +
            ', id: ' + x.id +
            ', patientModalId: ' + x.patientModalId +
            ', comment: ' + x.comment +
            ', dosage: ' + x.dosage +
            ', frequencyUITag: ' + x.frequencyUITag +
            ', prescriber: ' + x.prescriber +
            ', severityId: ' + x.severityId +
            ', reactionType: ' + x.reactionType +
            ', reactionId: ' + x.reactionId +
            ', knownDate: ' + x.knownDate +
            ', age: ' + x.age +
            ', medicalConditionStatusId: ' + x.medicalConditionStatusId;
        ov.push(v);
    }
    return ov;
}