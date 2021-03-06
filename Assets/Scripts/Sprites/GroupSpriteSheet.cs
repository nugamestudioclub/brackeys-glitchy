using System.Collections.Generic;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

public abstract class GroupSpriteSheet : SpriteSheet {
	[SerializeField]
	[SerializeProperty(nameof(DefaultSprite))]
	private Sprite defaultSprite;
	public Sprite DefaultSprite {
		get => defaultSprite;
		set {
			defaultSprite = value;
			uniqueSpriteSheets.Clear();
			// Debug.Log($"{name} has legnth of: {Count}");
			for( int i = 0; i < Count; i++ ) {
				// Debug.Log($"Making {(TileType) i} spritesheet");
				var spriteSheet = MakeSpriteSheet(i, defaultSprite);

				this[i] = spriteSheet;
				if( !Contains(spriteSheet.Original) )
					uniqueSpriteSheets.Add(spriteSheet);
			}
#if UNITY_EDITOR
			EditorUtility.SetDirty(this);
#endif
		}
	}

	protected abstract IList<SpriteSheet> SpriteSheets { get; }

	[ReadOnly]
	[SerializeField]
	protected Palette<SpriteSheet> uniqueSpriteSheets = new Palette<SpriteSheet>();

	public int Count => SpriteSheets.Count;

	public override Sprite Original => OriginalAt(0);

	public override Sprite Grey => GreyAt(0);

	protected SpriteSheet this[int index] {
		get => SpriteSheets[IndexOrZero(index)];
		set => SpriteSheets[IndexOrZero(index)] = value;
	}

	public override Sprite OriginalAt(int index) => this[index].Original;

	public override Sprite GreyAt(int index) => this[index].Grey;

	public override Sprite FindUniqueOriginal(int start) {
		//Debug.Log($"size of {uniqueSpriteSheets}: {uniqueSpriteSheets.Count}");
		return uniqueSpriteSheets[start].Original;
	}

	public override Sprite FindUniqueGrey(int start) => uniqueSpriteSheets[start].Grey;

	private SingleSpriteSheet MakeSpriteSheet(int index, Sprite defaultSprite) {
		Sprite sprite = null;
		SingleSpriteSheet spriteSheet = CreateInstance<SingleSpriteSheet>();

#if UNITY_EDITOR
		string defaltSpritePath = AssetDatabase.GetAssetPath(defaultSprite);
		string directory = Path.GetDirectoryName(defaltSpritePath);
		string groupName = GroupName(defaltSpritePath);
		string typeName = TypeName(index);
		string ext = Path.GetExtension(defaltSpritePath);
		string spritePath = SpritePath(directory, groupName, typeName, ext);
		string spriteSheetPath = SpriteSheetPath(FolderName(), groupName, typeName);

		sprite = AssetDatabase.LoadAssetAtPath<Sprite>(spritePath);

		// Debug.Log($"{spriteSheetPath} is the path!");

		CreateAsset(ref spriteSheet, spriteSheetPath);
#endif
		spriteSheet.OriginalSprite = sprite == null ? defaultSprite : sprite;

		return spriteSheet;
	}

	private int IndexOrZero(int index) => 0 <= index && index < Count ? index : 0;

	private bool Contains(Sprite sprite) {
		foreach( var spriteSheet in uniqueSpriteSheets )
			if( spriteSheet.Original == sprite )
				return true;
		return false;
	}

	protected abstract string FolderName();

	private static string GroupName(string fileName) {
		return FileSystem.WithoutSuffix(Path.GetFileNameWithoutExtension(fileName), "_");
	}

	protected abstract string TypeName(int index);

	private static string SpritePath(string directory, string groupName, string typeName, string ext) {
		return $"{directory}/{groupName}_{typeName}{ext}";
	}

	private static string SpriteSheetPath(string folderName, string groupName, string typeName) {
		return $"{Paths.SPRITE_SHEETS}/{folderName}/{groupName}/{groupName}_{typeName}.asset";
	}


#if UNITY_EDITOR
	private static void CreateAsset(ref SingleSpriteSheet spriteSheet, string path) {
		string absolutePath = $"{Directory.GetCurrentDirectory()}/{path}";
		if( File.Exists(absolutePath) ) {
			spriteSheet = AssetDatabase.LoadAssetAtPath<SingleSpriteSheet>(path);
		}
		else {
			if( !Directory.Exists(absolutePath) )
				Directory.CreateDirectory(absolutePath);

			AssetDatabase.CreateAsset(spriteSheet, path);
		}

	}

#endif

}