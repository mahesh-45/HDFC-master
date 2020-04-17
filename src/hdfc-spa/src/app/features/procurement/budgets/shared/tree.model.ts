export class TreeNode {
  id: number;
  name: string;
  parentBudgetHeadId: number;
  childBudgetHead: TreeNode[];
}

export class TreeFlatNode {
  constructor(
    public expandable: boolean,
    public name: string,
    public level: number,
    public id: any
  ) { }
}