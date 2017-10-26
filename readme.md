# Sandbox Game Foundation for Unity

日本語の注意事項は下の方に書いてあります。


***caution***  
This project is a trial version before the alpha version.


### Preparation

- Unity (Supported 5.6.1+)
- Your favorite player character prefab.
- grounds texture.


### Usage:

1. Download this repository. (and extract)

2. Copy your favorite player character prefab (attach PlayerController.cs) to "Assets/Resources/Character/player/" directory.

3. Copy grounds texture to "Assets/Resources/Terrain/Grounds/" directory.

4. Edit player_fbx_filepath in "Congigs/player.json".

5. Edit texture_filepath in "Configs/terrain.json".

6. Open directory in unity and open bootStrap scene and Build&Run.

If you not found Configs directory or each under files, it will be created when you open the directory and Build&Run with Unity.  

And If you not found Assets/Plugins/ under each directory, please downloads each external library.  
[UniRx](https://github.com/neuecc/UniRx) / [Utf8Json](https://github.com/neuecc/Utf8Json/releases) / [OptimizedStringOperation in StringBuilderTemporary](https://github.com/wotakuro/StringBuilderTemporary/tree/master/Assets/Scripts)


### Player control

It moves front, left, back, right with WASD.  
Can't do anything else.

---

***注意事項***  
このプロジェクトはα版以前の試作版です。


### 用意するもの

- Unity (バージョン5.6.1以降)
- お気に入りのプレイヤーキャラのprefabファイル
- Terrainに貼る地面テクスチャ


### 使い方

1. このリポジトリをダウンロードしてください。（zipを落とした場合は解凍も）

2. 用意したプレイヤーキャラのファイルを "Assets/Resources/Character/player/" ディレクトリにコピーしてください。（PlayerController.csをアタッチしたprefabがベストです）

3. 用意した地面のテクスチャを "Assets/Resources/Terrain/Grounds/" ディレクトリにコピーしてください。

4. "Congigs/player.json" ファイルを開き、player_fbx_filepath を編集してください。

5. "Configs/terrain.json" ファイルを開き、texture_filepath を編集してください。

6. Unityでディレクトリを開き、bootStrapシーンを開き、Build & Run で動きます。

もしConfigsディレクトリやその中のファイルが見つからない場合、一旦UnityでBuild & Runすると生成されます。

また、Windowsではsubmoduleに設定しているAssets/Pluginsディレクトリ以下の外部ライブラリがgit cloneで取得出来ないようです。  
その場合は、それぞれダウンロードして導入してください。  
[UniRx](https://github.com/neuecc/UniRx) / [Utf8Json](https://github.com/neuecc/Utf8Json/releases) / [OptimizedStringOperation in StringBuilderTemporary](https://github.com/wotakuro/StringBuilderTemporary/tree/master/Assets/Scripts)


### 操作方法

WASDで前後左右に動きます。  
それ以外のことは出来ません。
